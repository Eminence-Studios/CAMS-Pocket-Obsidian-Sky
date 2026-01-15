using UnityEngine;
using UnityEngine.EventSystems;

public class WaterPlantsPuzzle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isHold = false;
    public bool completed = false;

    private float heldTime;
    public float requiredTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && !completed)
        {
            heldTime += Time.deltaTime;

            if (heldTime >= requiredTime)
            {
                completed = true;
                // gameObject.SetActive(false);
                Debug.Log("Completed");
            }
        }
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
