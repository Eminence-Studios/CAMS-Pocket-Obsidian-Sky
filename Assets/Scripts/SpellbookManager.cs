using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SpellbookManager : MonoBehaviour
{

    private int learnedSpells = 0;
    private int maxSpells = 25;
    private int numHotbarSpells = 0;

    [Header("Spellbook")]
    public GameObject actualSpellbook;
    public GameObject spellbookCanvas;
    public TextMeshProUGUI warningText;
    public GameObject hotbar;
    // public Canvas spellbookOverlay;

    [Header("Description Box")]
    public GameObject descriptionBox;
    public TextMeshProUGUI spellNameText;
    public TextMeshProUGUI type;
    public TextMeshProUGUI description;

    [Header("Map Canvas")]
    public GameObject mapCanvas;
    public Button mapIcon;
    public Image mapImage;

    [Header("Settings Canvas")]
    public GameObject settingsCanvas;

    [Header("Icon Canvas")]
    public GameObject icons;

    public static SpellbookManager instance;

    private SpellSlot[] spellbookSlots;
    private SpellSlot[] hotbarSlots;

    void Awake()
    {
        
        GameObject rootCanvas = gameObject;        
        DontDestroyOnLoad(rootCanvas);
        
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        // Time.timeScale = 0f;
        spellbookCanvas.SetActive(true);
        spellbookCanvas.SetActive(false);

    }

    private void OnDisable()
    {
        // Time.timeScale = 1f;
    }

    public void updateMap(Sprite newMap)
    {
        mapIcon.image.sprite = newMap;
        mapImage.sprite = newMap;
    }

    public void hideMapIcon()
    {
        mapIcon.gameObject.SetActive(false);
    }

    public void showMapIcon()
    {
        mapIcon.gameObject.SetActive(true);
    }

    /*
    public void learnSpell(Spell newSpell)
    {
        Debug.Log("learnedSpell");
        spellbookSlots[learnedSpells++].setSpell(newSpell);
    }
    */

    public void learnSpell(Spell newSpell)
    {
        spellbookSlots[learnedSpells].setSpell(newSpell);
        spellbookSlots[learnedSpells].GetComponent<Button>().interactable = true;
        learnedSpells++;

        if (!GameManager.Instance.currentData.learnedSpellNames.Contains(newSpell.spellName))
        {
            GameManager.Instance.currentData.learnedSpellNames.Add(newSpell.spellName);
            GameManager.Instance.SaveGame();
        }
    }

    public void addSpelltoHotbar(Spell spellData, Button slotButton)
    {
        if (numHotbarSpells >= hotbarSlots.Length)
        {
            warningText.gameObject.SetActive(true);
            Invoke("closeWarning", 2);
        }
        else
        {
            SpellSlot openSlot = nextOpen();
            openSlot.setSpell(spellData);
            openSlot.originalSpellSlot = slotButton;
            slotButton.interactable = false;
            numHotbarSpells++;
        }

    }

    public void closeCanvas(string whichCanvas)
    {
        if (whichCanvas.Equals("Map"))
        {
            mapCanvas.gameObject.SetActive(false);
        }
        else if (whichCanvas.Equals("Spellbook"))
        {
            spellbookCanvas.gameObject.SetActive(false);
        }
        else
        {
            settingsCanvas.gameObject.SetActive(false);
        }
        icons.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    public void openCanvas(string whichCanvas)
    {
        if (whichCanvas.Equals("Map"))
        {
            mapCanvas.gameObject.SetActive(true);
        }
        else if (whichCanvas.Equals("Spellbook"))
        {
            spellbookCanvas.gameObject.SetActive(true);
        }
        else
        {
            settingsCanvas.gameObject.SetActive(true);
        }
        icons.gameObject.SetActive(false);
        
        Time.timeScale = 0f;
    }

    private SpellSlot nextOpen()
    {
        foreach (SpellSlot slot in hotbarSlots)
        {
            if (!slot.hasSpell)
            {
                return slot;
            }
        }
        return null;
    }

    public void removeSpellfromHotbar()
    {
        hideDescription();
        numHotbarSpells--;
    }

    public void showDescription(Spell spellData, Vector2 position)
    {
        updateDescription(spellData);

        descriptionBox.transform.localPosition = position;
        descriptionBox.gameObject.SetActive(true);
    }

    public void hideDescription()
    {
        descriptionBox.gameObject.SetActive(false);
    }

    void closeWarning()
    {
        warningText.gameObject.SetActive(false);
    }

    private void updateDescription(Spell spellData)
    {
        spellNameText.text = spellData.spellName;
        description.text = spellData.spellDescription;
        if (spellData.chosenType == Spell.spellType.Attack)
        {
            type.text = $"Deals {spellData.damage} damage";
        }
        else if (spellData.chosenType == Spell.spellType.Defense)
        {
            type.text = $"Buffs by {spellData.damage}";
        }
        else if (spellData.chosenType == Spell.spellType.Healing)
        {
            type.text = $"Heals for {spellData.damage} HP";
        }
        else
        {
            type.text = $"Increases energy by {spellData.damage}";
        }

    }

    public void saveSpellbook()
    {
        var data = GameManager.Instance.currentData;

        // Save Hotbar
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (hotbarSlots[i].hasSpell)
                data.hotbarSpellNames[i] = hotbarSlots[i].spellData.spellName;
            else
                data.hotbarSpellNames[i] = "";
        }

        GameManager.Instance.SaveGame();
    }

    public void loadSpellbook()
    {
        spellbookSlots = GetComponentsInChildren<SpellSlot>(true);
        hotbarSlots = hotbar.GetComponentsInChildren<SpellSlot>(true);

        var data = GameManager.Instance.currentData;
        learnedSpells = 0;
        numHotbarSpells = 0;

        // Clear all slots first
        foreach (var slot in spellbookSlots) slot.clear();
        foreach (var slot in hotbarSlots) slot.clear();

        // Load Learned Spells
        foreach (string spellName in data.learnedSpellNames)
        {
            Spell spellAsset = Resources.Load<Spell>($"Spells/{spellName}");
            if (spellAsset != null)
            {
                learnSpell(spellAsset);
            }
        }

        // Load Hotbar
        for (int i = 0; i < data.hotbarSpellNames.Length; i++)
        {
            string spellName = data.hotbarSpellNames[i];
            if (!string.IsNullOrEmpty(spellName))
            {
                Spell spellAsset = Resources.Load<Spell>($"Spells/{spellName}");
                if (spellAsset != null)
                {
                    hotbarSlots[i].setSpell(spellAsset);
                    numHotbarSpells++;

                    // Re-link the original slot button so it remains non-interactable
                    linkHotbar(hotbarSlots[i], spellName);
                }
            }
        }
    }

    private void linkHotbar(SpellSlot hotbarSlot, string spellName)
    {
        foreach (var spellSlot in spellbookSlots)
        {
            if (spellSlot.hasSpell && spellSlot.spellData.spellName == spellName)
            {
                Button btn = spellSlot.GetComponent<Button>();
                hotbarSlot.originalSpellSlot = btn;
                btn.interactable = false;
                break;
            }
        }
    }
}
