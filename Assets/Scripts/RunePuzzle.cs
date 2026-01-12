using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class RunePuzzle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numbers;
    [SerializeField] TextMeshProUGUI progress;

    private int length = 0;
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
        
        if (length == 3 && !isCheck)
        {
            isCheck = true;
            progress.text = "Attempting to cast spell...";
            Invoke("checkCode", 2);
            
        }
        
        
    }

    public void checkCode()
    {
        if (numbers.text == code)
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
        numbers.text = "";
        length = 0;
        Debug.Log("cleared");
    }

    public void addSymbol(int index)
    {
        placeholders[length++].sprite = options[index].sprite;
        numbers.text += index;
        Debug.Log("added symbol");
        Debug.Log(numbers.text);
    }
    
    

}
