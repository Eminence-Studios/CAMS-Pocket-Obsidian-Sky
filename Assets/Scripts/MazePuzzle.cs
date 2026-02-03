using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.FilePathAttribute;

public class MazePuzzle : MonoBehaviour
{
    [SerializeField] Movement playerMovement;
    [SerializeField] GameObject puzzle;
    [SerializeField] Rigidbody2D body;

    [SerializeField] TextMeshProUGUI progress;
    [SerializeField] Button closeButton;
    [SerializeField] Collider2D collider;

    public string puzzleName;

    public int speed;

    private bool enableMovement = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        playerMovement.enableMovement = false;
        enableMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableMovement)
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        }
        else
        {
            body.linearVelocity = Vector2.zero;
        }
    }

    public void close()
    {
        playerMovement.enableMovement = true;
        enableMovement = false;
        puzzle.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "End")
        {
            PlayerPrefs.SetInt(puzzleName, 1);
            closeButton.gameObject.SetActive(false);
            progress.text = "You reached the end!";
            collider.gameObject.SetActive(false);
            Invoke("close", 2);

        }
    }
}
