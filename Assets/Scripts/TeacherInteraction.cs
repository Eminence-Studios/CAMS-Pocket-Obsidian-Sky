using UnityEngine;

public class TeacherInteraction : MonoBehaviour
{
    public Movement playerMovement;

    public string teacherName;
    [SerializeField] GameObject dialoguePopUp;
    [SerializeField] Dialogue dialogueManager;

    [Header("Teacher Lines")]
    public string[] initalLines;
    public string[] afterPuzzleLines;
    public string[] afterBattleLines;
    public string[] afterSpellLines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        playerMovement.enableMovement = false;

        if (PlayerPrefs.GetInt(teacherName + "Spell", 0) == 1)
        {
            dialogueManager.lines = afterSpellLines;
        }
        else if (PlayerPrefs.GetInt(teacherName + "Battle", 0) == 1)
        {
            dialogueManager.lines = afterBattleLines;
        }
        else if (PlayerPrefs.GetInt(teacherName + "Puzzle", 0) == 1)
        {
            dialogueManager.lines = afterPuzzleLines;
        }
        else
        {
            dialogueManager.lines = initalLines;
        }
        SpellbookManager.instance.transform.root.gameObject.SetActive(false);
        dialoguePopUp.SetActive(true);

        // dialogueManager.startDialogue();
        // Debug.Log("SetActive");
    }

    public void loadNext()
    {
        // if spell learned
        if (PlayerPrefs.GetInt(teacherName + "Spell", 0) == 1)
        {
            SpellbookManager.instance.transform.root.gameObject.SetActive(true);
            playerMovement.enableMovement = true;
        }
        // if finished battle
        else if (PlayerPrefs.GetInt(teacherName + "Battle", 0) == 1)
        {
            // open spell learning cavas
            Debug.Log("Open Spell");
        }
        // if puzzle completed
        else if (PlayerPrefs.GetInt(teacherName + "Puzzle", 0) == 1)
        {
            // open battle canvas
            Debug.Log("Open Battle");

        }
        else
        {
            playerMovement.enableMovement = true;
        }
    }
}
