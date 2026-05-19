using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameData currentData;
    public int activeSlot = 1;

    public GameObject uiOverlay;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        ApplyGlobalHardwareSettings();
        // CleanupIncompleteSlots();
    }

    public void SaveGame() => SaveSystem.Save(currentData, activeSlot);
    public void LoadGame(int slot) { activeSlot = slot; currentData = SaveSystem.Load(slot); }


    private void ApplyGlobalHardwareSettings()
    {
        float vol = PlayerPrefs.GetFloat("GlobalVolume", 0.8f);
        int vsync = PlayerPrefs.GetInt("GlobalVSync", 1);
        AudioListener.volume = vol;
        QualitySettings.vSyncCount = vsync;
    }

    /*
    private void CleanupIncompleteSlots()
    {
        for (int i = 1; i <= 4; i++) // Assuming you have 3 slots
        {
            GameData data = SaveSystem.Load(i);
            if (data != null)
            {
                if (string.IsNullOrEmpty(data.playerName))
                {
                    SaveSystem.DeleteSave(i); // Make sure SaveSystem has a Delete method!
                    Debug.Log($"Slot {i} was incomplete and has been erased.");
                }
            }
        }
    }
    */

    public TeacherProgress getTeacher(string name)
    {
        // Find teacher
        var teacher = currentData.teacherProgress.FirstOrDefault(t => t.teacherName == name);

        return teacher;
    }

    public void createTeacher(string name, int numOfPuzzles)
    {
        var teacher = currentData.teacherProgress.FirstOrDefault(t => t.teacherName == name);

        if (teacher == null)
        {
            teacher = new TeacherProgress(name, numOfPuzzles);
            currentData.teacherProgress.Add(teacher);
        }
    }

    public void setBattleWon(string teacher)
    {
        var data = getTeacher(teacher);
        data.battle = true;
        SaveGame();
    }

    public void setPuzzleCompeletion(string teacher, int index)
    {
        var data = getTeacher(teacher);
        data.subPuzzles[index] = true;
        bool allMatch = Array.TrueForAll(data.subPuzzles, x => x == true);
        if (allMatch) data.puzzle = true;
        SaveGame();
    }

    /*
    public bool isPuzzleComplete(string teacher, int index)
    {
        var data = getTeacher(teacher);
        return index < data.subPuzzles.Length && data.subPuzzles[index];
    }
    */

    public void startSlotGame(int slot)
    {
        activeSlot = slot;

        if (SpellbookManager.instance == null)
        {
            Instantiate(uiOverlay);
            SpellbookManager.instance.gameObject.SetActive(false);
        }
        LoadGame(slot);

        // New Game
        if (currentData.lastLocation == 0)
        {
            currentData.masterVolume = PlayerPrefs.GetFloat("GlobalVolume", 0.8f);
            currentData.vsyncCount = PlayerPrefs.GetInt("GlobalVSync", 1);
            currentData.resolutionIndex = PlayerPrefs.GetInt("GlobalResIndex", -1);
            SaveGame();
            SceneManager.LoadScene(1);
        }
        else
        {
            // Load the scene the player was last in
            SceneManager.LoadScene(currentData.lastLocation);
        }
        SpellbookManager.instance.loadSpellbook();

    }
    public void addSpelltoElement(string element, int totalSpellsInElement)
    {
        // Find list of all spells for element
        var elementData = currentData.elementSpellsLearned.FirstOrDefault(e => e.elementName == element);

        // Create element and spell count if it does not exist
        if (elementData == null)
        {
            elementData = new ElementCount { elementName = element, count = 0 };
            currentData.elementSpellsLearned.Add(elementData);
        }

        elementData.count++;

        SaveGame();
    }

    public bool elementJustCompleted(string element, int totalSpellsInElement)
    {
        var elementData = currentData.elementSpellsLearned.FirstOrDefault(e => e.elementName == element);

        if (elementData.count >= totalSpellsInElement && !currentData.masteredElements.Contains(element))
        {
            currentData.numOfMasteredElements++;
            currentData.masteredElements.Add(element);
            return true;
        }
        return false;
    }
}