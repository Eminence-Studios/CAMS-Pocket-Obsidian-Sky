using UnityEngine;

public class Obj : MonoBehaviour
{
    public static Obj instance;
    public float movespeed = 10;
    public float ydeadzone = -6;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        transform.position += (Vector3.down * movespeed) * Time.deltaTime;

        if (transform.position.y < ydeadzone)
        {
            CatchingObjUIManager.instance.loseGame();
            Time.timeScale = 0f;
        }
    }

    public void destroyObj()
    {
        Destroy(gameObject);
    }
}