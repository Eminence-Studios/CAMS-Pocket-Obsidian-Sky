using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textBox;
    [SerializeField] Canvas oldScreen;
    [SerializeField] Canvas dialogueScreen;
    public string[] lines;

    private int index;

    [SerializeField] Canvas otherSpeaker;
    [SerializeField] Image self;

    // Self = 0
    // Other = 1
    public int[] speakers;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox.text = string.Empty;
        dialogueScreen.gameObject.SetActive(false);
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
        index = 0;
        oldScreen.gameObject.SetActive(false);
        dialogueScreen.gameObject.SetActive(true);
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
            yield return new WaitForSeconds(0.05f);
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
            dialogueScreen.gameObject.SetActive(false);
            textBox.gameObject.SetActive(false);
            oldScreen.gameObject.SetActive(true);
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
