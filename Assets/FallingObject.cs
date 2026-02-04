using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public static FallingObject instance;
    private float movespeed;
    public float ydeadzone;

    void Awake()
    {
        instance = this;
        movespeed = Random.Range(5, 10);

    }
    void Update()
    {
        transform.position += (Vector3.down * movespeed) * Time.deltaTime;

        if (transform.position.y < ydeadzone)
        {
            CatchObjectManager.instance.endGame();
        }
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}