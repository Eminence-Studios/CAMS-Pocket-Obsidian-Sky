using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float xoffset;
    public float spawnrate;
    private float time = 0;
    public GameObject spawnedObject;
    public static ObjectSpawner instance;


    void Awake()
    {
        instance = this;

    }
    void Update()
    {
        if (time < spawnrate)
        {
            time += Time.deltaTime;
        }
        else
        {
            spawnObject();
            time = 0;
        }
    }
    void spawnObject()
    {
        float lowestX = transform.position.x - xoffset;
        float highestX = transform.position.x + xoffset;
        Instantiate(spawnedObject, new Vector3(Random.Range(lowestX, highestX), transform.position.y, 0), transform.rotation);
    }
}
