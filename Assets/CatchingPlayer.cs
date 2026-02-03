using UnityEngine;

public class CatchingPlayer : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }
    public Rigidbody2D rb;
    public float speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.linearVelocity.y);
    }

    
}