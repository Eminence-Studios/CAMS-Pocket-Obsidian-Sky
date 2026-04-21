using NUnit.Framework;
using System;
using System.Linq;
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
    }

    public void SaveGame() => SaveSystem.Save(currentData, activeSlot);
    public void LoadGame(int slot) { activeSlot = slot; currentData = SaveSystem.Load(slot); }

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

    public void SelectSlotAndProceed(int slot)
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
            currentData.lastLocation = 1; // Default start scene
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

        // Check element mastery
        if (elementData.count >= totalSpellsInElement && !currentData.masteredElements.Contains(element))
        {
            currentData.numOfMasteredElements++;
            currentData.masteredElements.Add(element);
        }

        SaveGame();
    }
}