using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    [SerializeField] public Sprite[] images;
    [SerializeField] public Image background;

    private int index = 0;

    public CanvasChanger canvasChangerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        background.sprite = images[index];

        canvasChangerScript = GetComponent<CanvasChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (index < images.Length - 1)
            {
                changeImage();
            }
            else
            {
                canvasChangerScript.changeCanvas();
            }
        }
    }
    void changeImage()
    {
        index++;
        background.sprite = images[index];
    }
}
