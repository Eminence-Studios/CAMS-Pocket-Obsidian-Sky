using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float xoffset;
    public float spawnrate;
    private float time = 0;
    public GameObject [] spawnedObject;
    public static ObjectSpawner instance;

    private int index = 0;


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
            Debug.Log(index);
            time = 0;
        }
    }
    void spawnObject()
    {
        float lowestX = transform.position.x - xoffset;
        float highestX = transform.position.x + xoffset;
        Instantiate(spawnedObject[index++], new Vector3(Random.Range(lowestX, highestX), transform.localPosition.y, 0), transform.rotation);
        if (index > spawnedObject.Length - 1)
        {
            index = 0;
        }
    }
}
