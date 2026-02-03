using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderPuzzle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] Canvas puzzle;
    [SerializeField] Collider2D collider;
    [SerializeField] Button closeButton;
    [SerializeField] Button clearButton;

    public string code;
    public string playerPrefVariable;

    // instructions, attemping, failed, success
    public string[] progressTexts;

    public Image[] placeholders;
    public Sprite[] originalPlaceholders;
    public GameObject[] options;

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
            progress.text = progressTexts[1];
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
            progress.text = progressTexts[3];

            PlayerPrefs.SetInt(playerPrefVariable, 1);
            collider.gameObject.SetActive(false);
            Invoke("closePuzzle", 2);

        }
        else
        {
            progress.text = progressTexts[2];
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
        progress.text = progressTexts[0];

    }

    public void clear()
    {
        for (int i = 0; i < placeholders.Length; i++)
        {
            placeholders[i].sprite = originalPlaceholders[i];
        }
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = true;
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
        placeholders[input.text.Length - 1].sprite = options[index].GetComponent<Image>().sprite;
        options[index].GetComponent<Button>().interactable = false;
        // Debug.Log("added symbol");
        // Debug.Log(input.text);
    }
    
    

}
