using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private int speed;

    private int currentX;
    private int currentY;

    private SpriteRenderer spriteRenderer;


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
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

        turnCheck();
    }

    // Check if player turns
    private void turnCheck()
    {
        if ((Input.GetAxis("Horizontal") > 0 && currentX == -1)
            || (Input.GetAxis("Horizontal") < 0 && currentX == 1))
        {
            turn(0);
        }
        else if ((Input.GetAxis("Vertical") > 0 && currentY == -1)
            || (Input.GetAxis("Vertical") < 0 && currentY == 1))
        {
            turn(1);
        }
    }

    // direction = 0: x-axis
    // direction = 1: y-axis
    private void turn(int direction)
    {
        if (direction == 0)
        {
            if (currentX == 1)
            {
                // change sprite to look left
            }
            else
            {
                // change sprite to look right
            }
            currentX *= -1;
            PlayerPrefs.SetInt("xDirection", currentX);
        }
        else
        {
            if (currentY == 1)
            {
                // change sprite to look down
            }
            else
            {
                // change sprite to look up
            }
            currentY *= -1;
            PlayerPrefs.SetInt("yDirection", currentY);
        }
    }
}
