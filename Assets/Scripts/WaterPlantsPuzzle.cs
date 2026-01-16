using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaterPlantsPuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    public Movement playerMovement;

    public string notStartedText;
    public string inProgressText;
    public string completedText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        progress.text = notStartedText;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && !completed)
        {
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
            plant.sprite = stem;
            progress.text = notStartedText;
            gauge.value = 0;
        }
    }

    private void puzzleComplete()
    {
        plant.sprite = flower;
        progress.text = completedText;
        gauge.value = requiredTime;
        collider.SetActive(false);
        Invoke("closePuzzle", 2);
    }
    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
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


}
