using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio Sources")]
    public AudioSource introSource;
    public AudioSource loopSource;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void PlayFullTrack(AudioClip introClip, AudioClip loopClip)
    {
        // 1. If we are already playing this loop, don't restart it
        if (loopSource.clip == loopClip && loopSource.isPlaying) return;

        // 2. Stop everything current
        introSource.Stop();
        loopSource.Stop();

        // 3. Assign the new clips
        introSource.clip = introClip;
        loopSource.clip = loopClip;

        // 4. Set the loop source to actually loop
        introSource.loop = false;
        loopSource.loop = true;

        // 5. Calculate the timing
        // We get the exact duration of the intro clip
        double introLength = (double)introClip.samples / introClip.frequency;

        // Pick a start time slightly in the future to allow for buffering
        double startTime = AudioSettings.dspTime + 0.1;
        double loopStartTime = startTime + introLength;

        // 6. Schedule them
        introSource.PlayScheduled(startTime);
        loopSource.PlayScheduled(loopStartTime);
    }
}