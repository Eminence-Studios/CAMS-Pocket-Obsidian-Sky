using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    // Settings
    public float masterVolume;
    public int resolutionIndex;
    // public bool isFullscreen;
    public int vsyncCount;

    // Player
    public string playerName;

    // Save Data
    public int lastLocation;

    // Teacher Progress
    public List<TeacherProgress> teacherProgress = new List<TeacherProgress>();

    // Tracks spells learned for each element
    public List<ElementCount> elementSpellsLearned = new List<ElementCount>();


    // Track all spells
    public List<string> learnedSpellNames = new List<string>();
    public string[] hotbarSpellNames = new string[5];

    // List of mastered elements (in order)
    public int numOfMasteredElements;
    public List<string> masteredElements = new List<string>();

    // List of location in campus, 1000, 2000, 3000, 4000, 6000
    public List<int> currentLocations = new List<int>();

    public bool tutorialComplete;

    public GameData()
    {
        masterVolume = 1f;
        resolutionIndex = -1;
        // isFullscreen = false;
        vsyncCount = 1;
        numOfMasteredElements = 4;
        teacherProgress = new List<TeacherProgress>();
        elementSpellsLearned = new List<ElementCount>();
        masteredElements = new List<string>();
        currentLocations = new() {0,0,0,0,0,0};
        tutorialComplete = false;
    }
}

[System.Serializable]
public class TeacherProgress
{
    public string teacherName;
    public bool puzzle;
    public bool [] subPuzzles; // Index 0=Order, 1-5=Holding (for Nishiyama)
    public bool battle;
    public bool spell;
    
    public TeacherProgress(string teacherName, int numOfPuzzles)
    {
        this.teacherName = teacherName;
        subPuzzles = new bool[numOfPuzzles];
    }
}

[System.Serializable]
public class ElementCount
{
    public string elementName;
    public int count;
}