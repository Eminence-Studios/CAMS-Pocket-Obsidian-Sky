using System;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float maxMoveSpeed;
    public float minMoveSpeed;
    private float moveSpeed;
    public float ydeadzone;

    public Boolean isCatch;

    void Awake()
    {
        moveSpeed = UnityEngine.Random.Range(maxMoveSpeed, minMoveSpeed);

    }
    void Update()
    {
        transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;

        if (transform.position.y < ydeadzone)
        {
            if (isCatch)
            {
                DodgingObjectManager.instance.endGame();

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        
        if (collision.gameObject.name == "DodgingPlayer")
        {
            Destroy(gameObject);
            DodgingObjectManager.instance.endGame();
        }
        else if (collision.gameObject.name == "CatchingPlayer")
        {
            Destroy(gameObject);
        }
    }

    
}