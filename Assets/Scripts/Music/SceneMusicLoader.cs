using UnityEngine;

public class SceneMusicLoader : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;

    public bool changeMusic;
    public AudioClip afterSong;

    void Start()
    {
        if (MusicManager.instance != null)
        {
            if (changeMusic && GameManager.Instance.currentData.numOfMasteredElements >= 4)
            {
                MusicManager.instance.PlayFullTrack(afterSong, afterSong);
            }
            MusicManager.instance.PlayFullTrack(intro, loop);
        }
    }
}