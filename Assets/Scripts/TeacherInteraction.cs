using UnityEngine;
using UnityEngine.EventSystems;

public class TeacherInteraction : MonoBehaviour
{
    public Movement playerMovement;

    public string teacherName;
    [SerializeField] GameObject dialoguePopUp;
    [SerializeField] Dialogue dialogueManager;
    [SerializeField] GameObject spellSelectionScreen;

    public string element;
    public int totalNumOfElementSpells;
    public Canvas battleCanvas;
    public EventSystem eventSystem;

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

        TeacherProgress data = GameManager.Instance.getTeacher(teacherName);

        if (data.spell)
        {
            dialogueManager.lines = afterSpellLines;
        }
        else if (data.battle)
        {
            dialogueManager.lines = afterBattleLines;
        }
        else if (data.puzzle)
        {
            dialogueManager.lines = afterPuzzleLines;
        }
        else
        {
            dialogueManager.lines = initalLines;
        }
        SpellbookManager.instance.gameObject.SetActive(false);
        dialoguePopUp.SetActive(true);
    }

    public void loadNext()
    {
        TeacherProgress data = GameManager.Instance.getTeacher(teacherName);

        if (data.battle)
        {
            spellSelectionScreen.SetActive(true);
            Debug.Log("Open Spell");
        }
        // if puzzle completed
        else if (data.puzzle)
        {
            // open battle canvas
            Debug.Log("Open Battle");
            GetComponent<SpriteRenderer>().enabled = false;
            battleCanvas.gameObject.SetActive(true);
            playerMovement.enableMovement = true;
        }
        else
        {
            playerMovement.enableMovement = true;
            SpellbookManager.instance.gameObject.SetActive(true);
        }
    }

    public void selectSpell(Spell spell)
    {
        SpellbookManager.instance.learnSpell(spell);

        // Update teacher progress
        TeacherProgress data = GameManager.Instance.getTeacher(teacherName);
        data.spell = true;
        GameManager.Instance.addSpelltoElement(element, totalNumOfElementSpells);

        spellSelectionScreen.SetActive(false);
        dialogueManager.lines = afterSpellLines;

        SpellbookManager.instance.gameObject.SetActive(false);
        dialoguePopUp.SetActive(true);
    }

}
