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
    [SerializeField] Canvas puzzle;
    [SerializeField] Collider2D collider;
    [SerializeField] Button closeButton;
    [SerializeField] Button clearButton;

    public string code;
    public string playerPrefVariable;

    public Image[] placeholders;
    public Sprite[] originalPlaceholders;
    public Image[] options;

    private bool isCheck = false;
    public Movement playerMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (input.text.Length == originalPlaceholders.Length && !isCheck)
        {
            closeButton.gameObject.SetActive(false);
            clearButton.gameObject.SetActive(false);
            isCheck = true;
            progress.text = "Attempting to cast spell...";
            Invoke("checkCode", 2);
            
        }
        
        
    }
    private void OnEnable()
    {
        playerMovement.enableMovement = false;
    }
    private void checkCode()
    {
        if (input.text == code)
        {
            progress.text = "Success! Spell casted!";

            PlayerPrefs.SetInt(playerPrefVariable, 1);
            collider.gameObject.SetActive(false);
            Invoke("closePuzzle", 2);

        }
        else
        {
            progress.text = "Spell failed";
            clear();
            Invoke("clearProgress", 1);
            isCheck = false;
            closeButton.gameObject.SetActive(true);
            clearButton.gameObject.SetActive(true);
        }
        // Debug.Log("checked code");
    }

    private void clearProgress()
    {
        progress.text = "Enter the runes in the correct order to cast the spell";

    }

    public void clear()
    {
        for (int i = 0; i < placeholders.Length; i++)
        {
            placeholders[i].sprite = originalPlaceholders[i];
        }
        input.text = "";
        // Debug.Log("cleared");
    }

    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
        puzzle.gameObject.SetActive(false);
    }

    public void addSymbol(int index)
    {
        input.text += index;
        placeholders[input.text.Length - 1].sprite = options[index].sprite;
        // Debug.Log("added symbol");
        // Debug.Log(input.text);
    }
    
    

}
