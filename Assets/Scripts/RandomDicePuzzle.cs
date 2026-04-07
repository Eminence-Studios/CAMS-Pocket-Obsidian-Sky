using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomDicePuzzle : MonoBehaviour
{
    public int tempDiceNum;
    public GameObject[] diceFaces;

    [SerializeField] Canvas puzzle;
    [SerializeField] Collider2D collider;
    [SerializeField] Button closeButton;
    [SerializeField] Button rollButton;
    [SerializeField] TextMeshProUGUI progress;

    public bool isManager;

    private int rollCount = 0;

    public string playerPrefVariable;

    public Movement playerMovement;
    public RandomDicePuzzle other;

    public AudioSource successSound;
    public AudioSource failureSound;

    void OnEnable()
    {
        if (isManager)
        {
            playerMovement.enableMovement = false;
            SpellbookManager.instance.transform.root.gameObject.SetActive(false);
        }
    }

    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
        SpellbookManager.instance.transform.root.gameObject.SetActive(true);
        puzzle.gameObject.SetActive(false);

    }

    private void success()
    {
        progress.text = "Success!";
        successSound.Play();
        PlayerPrefs.SetInt(playerPrefVariable, 1);
        collider.gameObject.SetActive(false);
        Invoke("closePuzzle", 2);
    }

    public void clearRolls()
    {
        for (int i = 0; i < 6; i++)
        {
            diceFaces[i].GetComponent<RandomDiceHUD>().on = false;
            diceFaces[i].GetComponent<RandomDiceHUD>().displayDice();
        }
    }

    private void checkDice()
    {
        if((tempDiceNum == 2 && other.tempDiceNum == 6) || (tempDiceNum == 6 && other.tempDiceNum == 2))
        {
            success();
        }
        else
        {
            failureSound.Play();
            closeButton.gameObject.SetActive(true);
            rollButton.interactable = true;
        }    
    }
    
    public void onRollButtonClick()
    {
        if (isManager)
        {
            closeButton.gameObject.SetActive(false);
            rollButton.interactable = false;
        }
        rollCount++;
        displayNeutralDice();
        clearRolls();
        StartCoroutine(rollDice());

        
    }
    IEnumerator rollDice()
    {
        int finalRoll = 0;
        for (int i = 0; i < 10; i++)
        {
            tempDiceNum = Random.Range(1, 7); // rolls the number between 1 and 6
            diceFaces[tempDiceNum - 1].GetComponent<RandomDiceHUD>().on = true; // sets the "on" setting true for gameobject
            diceFaces[tempDiceNum - 1].GetComponent<RandomDiceHUD>().displayDice(); //calls the method
            yield return new WaitForSeconds(0.15f);
            diceFaces[tempDiceNum - 1].GetComponent<RandomDiceHUD>().on = false; // turns it off after a little just to display it
            finalRoll = tempDiceNum; // won't be final until the last iteration
            for (int j = 0; j < 6; j++)
            {
                diceFaces[j].GetComponent<RandomDiceHUD>().displayDice();
            }
        }
        if (rollCount >= 15)
        {
            int chance = Random.Range(1, 3);
            if (chance == 1)
            {
                finalRoll = 2;
                tempDiceNum = 2;
            }
            else
            {
                finalRoll = 6;
                tempDiceNum = 6;
            }
        }
        diceFaces[finalRoll - 1].GetComponent<RandomDiceHUD>().on = true;
        for (int i = 0; i < 6; i++)
        {
            diceFaces[i].GetComponent<RandomDiceHUD>().displayDice();
        }
        if (isManager)
        {
            checkDice();
        }
    }

    public void displayNeutralDice()
    {
        GetComponent<Image>().enabled = false;
        for (int i = 0; i < 6; i++)
        {
            diceFaces[i].GetComponent<Image>().enabled = true;
            diceFaces[i].GetComponent<RandomDiceHUD>().displayDice();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
