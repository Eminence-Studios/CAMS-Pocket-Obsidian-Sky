using UnityEngine;

public class Container : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
        if (polygon == null)
        {
            polygon = gameObject.AddComponent<PolygonCollider2D>();
        }
        Vector2 [] points = polygon.points;
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(polygon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
