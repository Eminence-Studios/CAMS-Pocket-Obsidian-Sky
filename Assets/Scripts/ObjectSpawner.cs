using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float xoffset;
    public float spawnrate;
    private float time = 0;
    public GameObject [] spawnedObject;
    public static ObjectSpawner instance;

    public bool doSpawn;

    private int index = 0;


    void Awake()
    {
        instance = this;

    }

    void Update()
    {
        if (doSpawn)
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
    }

    void spawnObject()
    {
        /*
        float lowestX = transform.position.x - xoffset;
        float highestX = transform.position.x + xoffset;
        Instantiate(spawnedObject[index++], new Vector3(Random.Range(lowestX, highestX), transform.position.y, 0), transform.rotation, transform.parent);
        if (index > spawnedObject.Length - 1)
        {
            index = 0;
        }
        */

        float lowestX = -xoffset;
        float highestX = xoffset;
        float randomX = UnityEngine.Random.Range(lowestX, highestX);

        GameObject newObj = Instantiate(spawnedObject[index++], transform.parent);

        newObj.transform.localPosition = new Vector3(transform.localPosition.x + randomX, transform.localPosition.y, 0);
        newObj.transform.localRotation = transform.localRotation;

        if (index > spawnedObject.Length - 1)
        {
            index = 0;
        }
    }
}
