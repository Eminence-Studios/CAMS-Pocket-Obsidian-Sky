using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    public int speed;

    public Sprite Front;
    public Sprite Back;
    public Sprite Side;

    private int currentX;
    private int currentY;

    private SpriteRenderer spriteRenderer;
    public bool movementEnabled = true;

    private float xScale;
    private float yScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        currentX = PlayerPrefs.GetInt("xDirection", 1);
        currentY = PlayerPrefs.GetInt("yDirection", 1);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementEnabled)
        {
            body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);


            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                spriteRenderer.sprite = Back;
                transform.localScale = new Vector2(xScale, yScale);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                spriteRenderer.sprite = Front;
                transform.localScale = new Vector2(xScale, yScale);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                spriteRenderer.sprite = Side;
                transform.localScale = new Vector2(-xScale, yScale);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                spriteRenderer.sprite = Side;
                transform.localScale = new Vector2(xScale, yScale);
            }
        }
        else
        {
            body.linearVelocity = Vector2.zero;
        }
    }

    /*
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
           spriteRenderer.sprite = Side;
           if (currentX == 1)
           {
               transform.localScale = transform.localScale * new Vector2(-1, 1);
           }
           else
           {
               transform.localScale = transform.localScale * new Vector2(1, 1);
           }
           currentX *= -1;
           PlayerPrefs.SetInt("xDirection", currentX);
       }
       else
       {
           if (currentY == 1)
           {
               spriteRenderer.sprite = Front;
           }
           else
           {
               spriteRenderer.sprite = Back;
           }
           currentY *= -1;
           PlayerPrefs.SetInt("yDirection", currentY);
       }
   }
   */
}
