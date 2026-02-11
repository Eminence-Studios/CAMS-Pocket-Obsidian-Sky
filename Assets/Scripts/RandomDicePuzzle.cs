using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int tempDiceNum;
    public GameObject[] diceFaces;

    public void clearRolls()
    {
        for (int i = 0; i < 6; i++)
        {
            diceFaces[i].GetComponent<RandomDiceHUD>().on = false;
            diceFaces[i].GetComponent<RandomDiceHUD>().displayDice();
        }
    }
    
    public void onRollButtonClick()
    {
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
        diceFaces[finalRoll - 1].GetComponent<RandomDiceHUD>().on = true;
        for (int i = 0; i < 6; i++)
        {
            diceFaces[i].GetComponent<RandomDiceHUD>().displayDice();
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
