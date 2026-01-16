using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
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

    private string melody = "012345";
    public Movement playerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.text.Length == 6)
        {
            if (input.text == melody)
            {
                progress.text = "Beautiful tune!";
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
        playerMovement.enableMovement = true;
        puzzle.gameObject.SetActive(false);
    }
    
    

}
