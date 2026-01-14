using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] public Canvas oldCanvas;
    [SerializeField] public Canvas newCanvas;

    // 0 = old
    // 1 = new
    public int setActive;
    public int setNOTActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeCanvas()
    {
        oldCanvas.gameObject.SetActive(false);
        newCanvas.gameObject.SetActive(true);
    }

    
    public void setActiveCanvas()
    {
        if (setActive == 0)
        {
            oldCanvas.gameObject.SetActive(true);

        }
        else
        {
            newCanvas.gameObject.SetActive(true);
        }
        
    }

    public void setNOTActiveCanvas()
    {
        if (setActive == 0)
        {
            oldCanvas.gameObject.SetActive(false);

        }
        else
        {
            newCanvas.gameObject.SetActive(false);
        }
    }
    
}
