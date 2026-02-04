using System;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private float movespeed;
    public float ydeadzone;

    public Boolean isCatch;

    void Awake()
    {
        movespeed = UnityEngine.Random.Range(300, 400);

    }
    void Update()
    {
        transform.position += (Vector3.down * movespeed) * Time.deltaTime;

        if (transform.position.y < ydeadzone)
        {
            if (isCatch)
            {
                FallingObjectManager.instance.endGame();

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.name == "DodgingPlayer")
        {
            FallingObjectManager.instance.endGame();
        }
    }

    
}