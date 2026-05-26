using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Button closeMap;
    public Button mapIcon;
    public Image mapImage;

    [Header("Paused Canvas")]
    public GameObject pausedCanvas;

    [Header("Congratulations Canvas")]
    public GameObject congratulationsCanvas;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI elementText;
    public TextMeshProUGUI classesText;
    public AudioSource graduationSong;

    [Header("Icon Canvas")]
    public GameObject icons;

    public static SpellbookManager instance;

    private SpellSlot[] spellbookSlots;
    public SpellSlot[] hotbarSlots;

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

    public void hallwayMap()
    {
        RectTransform mapIconSize = mapIcon.GetComponent<RectTransform>();
        mapIconSize.sizeDelta = new Vector2(400, 280);
        mapIconSize.anchoredPosition = new Vector2(-230, -120);

        RectTransform mapSize = mapImage.GetComponent<RectTransform>();
        mapSize.sizeDelta = new Vector2(1500, 900);

        closeMap.transform.localPosition = new Vector2(800, 300);
    }

    public void hallway6000Map()
    {
        RectTransform mapIconSize = mapIcon.GetComponent<RectTransform>();
        mapIconSize.sizeDelta = new Vector2(150, 300);
        mapIconSize.anchoredPosition = new Vector2(-125, -190);

        RectTransform mapSize = mapImage.GetComponent<RectTransform>();
        mapSize.sizeDelta = new Vector2(500, 950);

        closeMap.transform.localPosition = new Vector2(275, 450);
    }

    public void campusMap()
    {
        RectTransform mapIconSize = mapIcon.GetComponent<RectTransform>();
        mapIconSize.sizeDelta = new Vector2(245, 280);
        mapIconSize.anchoredPosition = new Vector2(-150, -170);

        RectTransform mapSize = mapImage.GetComponent<RectTransform>();
        mapSize.sizeDelta = new Vector2(900, 900);

        closeMap.transform.localPosition = new Vector2(450, 450);
    }

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

    public void updateCongratulations()
    {
        int index = GameManager.Instance.currentData.numOfMasteredElements - 1;
        elementText.text = "You've successfully mastered " + GameManager.Instance.currentData.masteredElements[index] + "!";


        if (index < 3)
        {
            string[] years = { "sophomore", "junior", "senior" };
            classesText.text = "We've unlocked the " + years[index] + " classes for you!\nKeep up the great work and best of luck!";
        }
        else
        {
            classesText.text = "You've completed 4 years at CAMS! Happy Graduation!";
        }

    }

    public void endGameCongratulations()
    {
        MusicManager.instance.pauseBackgroundTrack();
        graduationSong.Play();
        titleText.text = "Happy Graduation!";
        elementText.text = "Elements Mastered: " + GameManager.Instance.currentData.masteredElements.ToString();
        classesText.text = "You can continue exploring campus, and once you're ready to leave, wipe this save slot in the main menu to let another student experience CAMS.";
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
        else if (whichCanvas.Equals("Congratulations"))
        {
            if (GameManager.Instance.currentData.numOfMasteredElements == 4)
            {
                endGameCongratulations();
            }
            else
            {
                congratulationsCanvas.gameObject.SetActive(false);
                MusicManager.instance.resumeBackgroundTrack();
            }
        }
        else
        {
            pausedCanvas.gameObject.SetActive(false);
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
        else if (whichCanvas.Equals("Congratulations"))
        {
            updateCongratulations();
            congratulationsCanvas.gameObject.SetActive(true);
        }
        else
        {
            pausedCanvas.gameObject.SetActive(true);
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
        type.text = spellData.statDescription;

        /*
        if (spellData.chosenType == Spell.spellType.Attack)
        {
            type.text = $"Chance of {spellData.damage} attack";
        }
        else if (spellData.chosenType == Spell.spellType.Defense)
        {
            type.text = $"Chance of {spellData.damage} defense";
        }
        else if (spellData.chosenType == Spell.spellType.Healing)
        {
            type.text = $"Heals for {spellData.damage} HP";
        }
        else
        {
            type.text = $"Increases energy by {spellData.damage}";
        }
        */

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

    public void saveAndExit()
    {
        SpellbookManager.instance.closeCanvas("Paused");

        GameManager.Instance.currentData.lastLocation = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.SaveGame();

        Destroy(SpellbookManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
