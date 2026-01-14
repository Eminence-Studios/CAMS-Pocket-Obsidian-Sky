using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class RunePuzzle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] TextMeshProUGUI progress;

    private string code = "521";

    public Image[] placeholders;
    public Image[] options;

    private bool isCheck = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.text.Length == 3 && !isCheck)
        {
            isCheck = true;
            progress.text = "Attempting to cast spell...";
            Invoke("checkCode", 2);
            
        }
        
        
    }

    public void checkCode()
    {
        if (input.text == code)
        {
            progress.text = "Success! Spell casted!";
            
        }
        else
        {
            progress.text = "Spell failed";
            clear();
            Invoke("clearProgress", 1);
            isCheck = false;
        }
        Debug.Log("checked code");
    }

    public void clearProgress()
    {
        progress.text = "";
    }

    public void clear()
    {
        foreach (Image i in placeholders)
        {
            i.sprite = null;
        }
        input.text = "";
        Debug.Log("cleared");
    }

    public void addSymbol(int index)
    {
        input.text += index;
        placeholders[input.text.Length - 1].sprite = options[index].sprite;
        Debug.Log("added symbol");
        Debug.Log(input.text);
    }
    
    

}
