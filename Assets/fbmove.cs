using UnityEngine;

public class fbmove : MonoBehaviour
{
    public float movespeed = 10;
    public float ydeadzone = -6;

    void Update()
    {
        transform.position += (Vector3.down * movespeed) * Time.deltaTime;

        if (transform.position.y < ydeadzone)
        {
            Destroy(gameObject);
        }
    }
}
