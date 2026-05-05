using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] Canvas dialogueScreen;
    
    public string[] lines;
    public bool hasCharName;

    [Header("User Input Parameters")]
    public bool isTutorial;
    [SerializeField] Canvas inputPopup;
    [SerializeField] TMP_InputField userInputField;

    [Header("Changing Scene Parameters")]
    public bool changeScene;
    public int sceneID;

    [Header("Teacher Dialogue Parameters")]
    public bool isTeacherDialogue;
    // public string teacherName;
    public TeacherInteraction teacherInteractionManager;

    [Header("Following Canvas Parameters")]
    public bool hasNextScene;
    [SerializeField] Canvas nextScreen;

    [Header("Player Movement Parameters")]
    public bool changePlayerMovement;
    public Movement playerMovement;


    // public bool hasSwitchSpeakers;
    // [SerializeField] Canvas otherSpeaker;
    // [SerializeField] Image self;

    // Self = 0
    // Other = 1
    // public int[] speakers;

    private int index;
    private string charName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textBox.text == lines[index])
            {
                if (index < lines.Length - 1 && lines[index + 1] == "userInput")
                {
                    StopAllCoroutines();
                    inputPopup.gameObject.SetActive(true);
                }
                else
                {
                    printNext();
                }
            }
            else
            {
                StopAllCoroutines();
                textBox.text = lines[index];
            }
        }
    }

    private void OnEnable()
    {
        if (SpellbookManager.instance != null)
        {
            SpellbookManager.instance.gameObject.SetActive(false);
        }
        if (changePlayerMovement)
        {
            playerMovement.movementEnabled = false;
        }
        startDialogue();
    }

    private void OnDisable()
    {
        if (changeScene)
        {
            SceneManager.LoadScene(sceneID);
        }
        else if (isTeacherDialogue)
        {
            teacherInteractionManager.loadNext();
        }
        else if (hasNextScene)
        {
            nextScreen.gameObject.SetActive(true);
        }
        else if (changePlayerMovement)
        {
            SpellbookManager.instance.gameObject.SetActive(true);
            playerMovement.movementEnabled = true;
        }
    }

    public void startDialogue()
    {
        if (hasCharName)
        {
            replaceCharName();
        }
        index = 0;
        textBox.text = string.Empty;
        StartCoroutine(printLine());
        // if (hasSwitchSpeakers) { alternateSpeaker(); };
    }

    void replaceCharName()
    {
        charName = GameManager.Instance.currentData.playerName ?? "Coyote";

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Replace("charName", charName);
            Debug.Log(lines[i]);
        }
    }

    IEnumerator printLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void printNext()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textBox.text = string.Empty;
            StartCoroutine(printLine());
                
        }
        else
        {
            if (isTutorial)
            {
                GameManager.Instance.currentData.tutorialComplete = true;
                GameManager.Instance.SaveGame();
                Debug.Log("changed");
            }
            dialogueScreen.gameObject.SetActive(false);
        }
        // if (hasSwitchSpeakers) { alternateSpeaker(); }
    }

    public void nameEntered()
    {
        if (!string.IsNullOrWhiteSpace(userInputField.text))
        {
            GameManager.Instance.currentData.playerName = userInputField.text;
            // replaceCharName();
            inputPopup.gameObject.SetActive(false);
            index++;
            lines[index + 1] = lines[index + 1].Replace("Coyote", userInputField.text);
            printNext();
        }
        
    }

       
    /*
    void alternateSpeaker()
    {
        if (speakers[index] == 0)
        {
            self.gameObject.SetActive(true);
            otherSpeaker.gameObject.SetActive(false);
        }
        else
        {
            self.gameObject.SetActive(false);
            otherSpeaker.gameObject.SetActive(true);
        }
    }
    */
    
    
}
