using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Sources")]
    public AudioSource introSource;
    public AudioSource loopSource;

    private bool isPaused = false;
    private double pauseDspTime;
    private double scheduledLoopStartTime;
    private double introDuration;


    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void PlayFullTrack(AudioClip introClip, AudioClip loopClip)
    {
        if (loopSource.clip == loopClip && (loopSource.isPlaying || isPaused)) return;

        isPaused = false;
        introSource.Stop();
        loopSource.Stop();

        introSource.clip = introClip;
        loopSource.clip = loopClip;

        introSource.loop = false;
        loopSource.loop = true;

        introDuration = (double)introClip.samples / introClip.frequency;

        double startTime = AudioSettings.dspTime + 0.1;
        scheduledLoopStartTime = startTime + introDuration;

        introSource.PlayScheduled(startTime);
        loopSource.PlayScheduled(scheduledLoopStartTime);
    }

    public void pauseBackgroundTrack()
    {
        if (isPaused) return;

        isPaused = true;
        pauseDspTime = AudioSettings.dspTime;

        introSource.Pause();
        loopSource.Pause();
    }

    public void wait(int time)
    {
        Invoke("resumeBackgroundTrack", time);
    }

    public void resumeBackgroundTrack()
    {
        if (!isPaused) return;
        isPaused = false;

        // If the intro was STILL playing when we paused, we must reschedule the loop
        if (introSource.clip != null && introSource.time < introDuration)
        {
            double elapsedBeforePause = pauseDspTime - (scheduledLoopStartTime - introDuration);
            double remainingIntroTime = introDuration - elapsedBeforePause;

            if (remainingIntroTime > 0)
            {
                double currentDspTime = AudioSettings.dspTime;
                scheduledLoopStartTime = currentDspTime + remainingIntroTime;

                introSource.UnPause();
                loopSource.Stop(); // Reset the old schedule link
                loopSource.PlayScheduled(scheduledLoopStartTime);
                return;
            }
        }

        // Standard unpause if they were already seamlessly looping on the main track
        introSource.UnPause();
        loopSource.UnPause();
    }
}