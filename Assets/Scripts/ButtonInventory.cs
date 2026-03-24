using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInventory : MonoBehaviour
{

    public List<GameObject> buttons = new List<GameObject>();

    public void changeButton(int index)
    {
        GameObject buttonChanged = buttons[index];
        string buttonText = buttonChanged.GetComponentInChildren<TextMeshProUGUI>().text;
        buttonChanged.GetComponentInChildren<TextMeshProUGUI>().text = "Changed";
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
