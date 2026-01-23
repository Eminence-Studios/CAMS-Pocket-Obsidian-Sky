using UnityEngine;

public class MazePuzzle : MonoBehaviour
{
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
        // success
    }
}
