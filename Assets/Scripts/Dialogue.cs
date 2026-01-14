using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] Canvas dialogueScreen;
    [SerializeField] Canvas nextScreen;
    public string[] lines;
    public bool hasCharName;

    public bool changeScene;
    public int sceneID;

    [SerializeField] Canvas otherSpeaker;
    [SerializeField] Image self;
    // Self = 0
    // Other = 1
    public int[] speakers;

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
                printNext();
            }
            else
            {
                StopAllCoroutines();
                textBox.text = lines[index];
            }
        }
    }

    public void startDialogue()
    {
        if (hasCharName)
        {
            charName = PlayerPrefs.GetString("charName", "Coyote");

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("charName", charName);
                Debug.Log(lines[i]);
            }
        }
        index = 0;
        textBox.gameObject.SetActive(true);
        textBox.text = string.Empty;
        StartCoroutine(printLine());
        alternateSpeaker();
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
        else if (changeScene)
        {
            textBox.gameObject.SetActive(false);
            dialogueScreen.gameObject.SetActive(false);
            SceneManager.LoadScene(sceneID);
        }
        else
        {
            dialogueScreen.gameObject.SetActive(false);
            textBox.gameObject.SetActive(false);
            nextScreen.gameObject.SetActive(true);
        }
        alternateSpeaker();
    }

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
    
}
