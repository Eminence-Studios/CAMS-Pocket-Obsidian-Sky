using UnityEngine.UI;
using UnityEngine;

public class RandomDiceHUD : MonoBehaviour
{

    public bool on;
    public Sprite newSprite;
    public Sprite baseSprite;

    public void displayDice()
    {
        if (on)
        {
            GetComponent<Image>().sprite = newSprite;
        }

        else
        {
            GetComponent<Image>().sprite = baseSprite;
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
