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

    private string melody = "123456";

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
                Debug.Log("success"); ;

            }
            else
            {
                input.text = input.text.Substring(1);
                Debug.Log("removed note");
                Debug.Log(input.text);
            }
            Debug.Log("checked code");

        }
        
        
    }

    public void playNote (int index)
    {
        input.text += index;
        // play note
        Debug.Log("played note");
        Debug.Log(input.text);
    }
    
    

}
