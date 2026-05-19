using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlots : MonoBehaviour
{

    public TextMeshProUGUI[] names;
    public GameObject[] wipeButtons;

    public TextMeshProUGUI wipeSlotName;
    private int slotNumber;

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
                    wipeButtons[i - 1].SetActive(true);
                }
                else
                {
                    SaveSystem.DeleteSave(i);
                    names[i - 1].text = "new game";
                }
            }
            else
            {
                names[i - 1].text = "new game";
                wipeButtons[i - 1].SetActive(false);
            }
        }
    }

    public void updateWipePopUp(int i)
    {
        wipeSlotName.text = "Are you sure you want to wipe \nSlot " + i + ": \"" + names[i - 1].text + "\"?";
        slotNumber = i;
    }

    public void wipeSlot()
    {
        SaveSystem.DeleteSave(slotNumber);
        updateSlotName();
    }
}
