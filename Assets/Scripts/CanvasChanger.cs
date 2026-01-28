using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] Canvas oldCanvas;
    [SerializeField] Canvas newCanvas;

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

    void OnCollisionEnter2D(Collision2D col)
    {
        newCanvas.gameObject.SetActive(true);

    }

}
