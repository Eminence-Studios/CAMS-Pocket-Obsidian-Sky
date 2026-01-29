using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MazePuzzle : MonoBehaviour
{
    public Movement playerMovement;

    private Rigidbody2D body;
    private int speed;

    private int currentX;
    private int currentY;

    private SpriteRenderer spriteRenderer;
    public bool enableMovement = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        currentX = PlayerPrefs.GetInt("xDirection", 1);
        currentY = PlayerPrefs.GetInt("yDirection", 1);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        speed = 400;
    }

    private void OnEnable()
    {
        playerMovement.enableMovement = false;
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
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D hitCollider = col.collider;
        if (hitCollider.name == "End")
        {
            // success
        }
        else if (hitCollider.name == "Edge")
        {
            restart();
        }
    }

    private void restart()
    {
        body.position = new Vector3 (0, 0, 0);
    }
}
