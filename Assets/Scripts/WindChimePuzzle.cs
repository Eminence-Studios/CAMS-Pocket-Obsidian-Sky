using System.Collections;using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WindChimePuzzle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] Canvas puzzle;
    [SerializeField] Collider2D collider;
    [SerializeField] Button close;

    private string melody = "254312";
    public Movement playerMovement;

    public AudioSource successSound;

    private bool solved = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        playerMovement.movementEnabled = false;
        SpellbookManager.instance.gameObject.SetActive(false);
        MusicManager.instance.pauseBackgroundTrack();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.text.Length == 6)
        {
            if (input.text == melody)
            {
                close.gameObject.SetActive(false);
                GameManager.Instance.setPuzzleCompeletion("Johnson", 0);
                GameManager.Instance.SaveGame();
                progress.text = "Beautiful tune!";
                successSound.Play();
                solved = true;
                collider.gameObject.SetActive(false);
                MusicManager.instance.wait(37);
                Invoke("closePuzzle", 2);

            }
            else
            {
                input.text = input.text.Substring(1);
            }
        }
        
        
    }

    public void playNote (int index)
    {
        input.text += index;
    }

    public void closePuzzle()
    {
        playerMovement.movementEnabled = true;
        SpellbookManager.instance.gameObject.SetActive(true);
        puzzle.gameObject.SetActive(false);
        if (!solved)
        {
            MusicManager.instance.resumeBackgroundTrack();
        }
    }
}
