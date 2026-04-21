using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButtonPuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHold = false;
    private bool completed = false;

    private float heldTime;
    public float requiredTime;

    [SerializeField] Slider gauge;
    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] Image plant;
    [SerializeField] GameObject collider;
    [SerializeField] GameObject puzzle;
    [SerializeField] Sprite stem;
    [SerializeField] Sprite bud;
    [SerializeField] Sprite flower;
    [SerializeField] Button close;
    // [SerializeField] MultiPuzzleManager puzzleManager;

    public Movement playerMovement;

    public string notStartedText;
    public string inProgressText;
    public string completedText;

    public string teacherName;
    public int puzzleIndex;

    public AudioSource successSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progress.text = notStartedText;
        gauge.maxValue = requiredTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && !completed)
        {
            close.gameObject.SetActive(false);
            plant.sprite = bud;
            heldTime += Time.deltaTime;
            gauge.value = heldTime;
            progress.text = inProgressText;

            if (heldTime >= requiredTime)
            {
                completed = true;
                puzzleComplete();
            }
        }
        else if (!completed)
        {
            close.gameObject.SetActive(true);
            plant.sprite = stem;
            progress.text = notStartedText;
            gauge.value = 0;
        }
    }

    private void OnEnable()
    {
        playerMovement.enableMovement = false;
        SpellbookManager.instance.gameObject.SetActive(false);
    }

    private void puzzleComplete()
    {
        SetCompletedState();

        GameManager.Instance.setPuzzleCompeletion(teacherName, puzzleIndex);
        // puzzleManager.checkAllComplete();
        GameManager.Instance.SaveGame();

        successSound.Play();
        collider.SetActive(false);
        // puzzleManager.checkAllComplete();
        Invoke("closePuzzle", 2);
    }
    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
        SpellbookManager.instance.gameObject.SetActive(true);
        puzzle.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHold = true;
        heldTime = 0;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }

    private void SetCompletedState()
    {
        completed = true;
        plant.sprite = flower;
        progress.text = completedText;
        gauge.value = requiredTime;
    }


}
