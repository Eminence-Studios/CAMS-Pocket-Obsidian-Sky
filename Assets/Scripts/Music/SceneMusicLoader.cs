using UnityEngine;

public class SceneMusicLoader : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;

    void Start()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayFullTrack(intro, loop);
        }
    }
}