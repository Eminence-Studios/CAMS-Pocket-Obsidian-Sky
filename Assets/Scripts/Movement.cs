using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = 100;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 200;
        }
        Debug.Log(speed);
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
    }
}
