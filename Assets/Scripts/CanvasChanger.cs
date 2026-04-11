using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] Canvas oldCanvas;
    [SerializeField] Canvas newCanvas;

    [SerializeField] Movement player;

    public bool disablePlayer;

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
        if (disablePlayer)
        {
            player.enableMovement = false;
        }
        
    }

    public void openPopUp()
    {
        player.enableMovement = false;
        newCanvas.gameObject.SetActive(true);
    }

    public void closePopUp()
    {
        player.enableMovement = true;
        oldCanvas.gameObject.SetActive(false);
    }
}
