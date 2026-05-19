using UnityEngine;

public class RisingObject : MonoBehaviour
{
    public float upmovespeed;
    public float downmovespeed;
    public float ydeadzone;
    public float maxHeight;
    private bool movingup = true;
    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingup)
        {
            transform.localPosition += (Vector3.up * upmovespeed) * Time.deltaTime;
            if (transform.localPosition.y >= maxHeight)
            {
                movingup = false;
            }
        }
        else
        {
            transform.localPosition += (Vector3.down * downmovespeed) * Time.deltaTime;
        }
        if (transform.localPosition.y < ydeadzone)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");

        Destroy(gameObject);
        DodgingObjectManager.instance.endGame();
    }
}
