using TMPro;
using UnityEngine;

public class LoadSlots : MonoBehaviour
{

    public TextMeshProUGUI[] names; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSlotName()
    {
        for (int i = 1; i < 5; i++)
        {
            if (SaveSystem.hasSlotData(i))
            {
                GameData data = SaveSystem.Load(i);
                if (!string.IsNullOrWhiteSpace(data.playerName))
                {
                    names[i - 1].text = data.playerName;
                }
                else
                {
                    SaveSystem.DeleteSave(i);
                    names[i - 1].text = "new game";
                }
            }
        }
    }
}
