using UnityEngine;

public class FireballSpawn : MonoBehaviour
{
    public float xoffset = 2;
    public float spawnrate = 2;
    public float time = 0;
    public GameObject fireball;
    void Update()
    {
        if (time < spawnrate)
        {
            time += Time.deltaTime;
        }
        else
        {
            spawnFB();
            time = 0;
        }
    }
    void spawnFB()
    {
        float lowestX = transform.position.x - xoffset;
        float highestX = transform.position.x + xoffset;
        Instantiate(fireball, new Vector3(Random.Range(lowestX, highestX), transform.position.y, 0), transform.rotation);
    }
}
