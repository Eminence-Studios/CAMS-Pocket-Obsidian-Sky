using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ObjCollision : MonoBehaviour
{
    public static ObjCollision instance;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CatchingObjUIManager.instance.score++;
        CatchingObjUIManager.instance.updateText();
        Debug.Log("hit");
        Obj.instance.destroyObj();
        

    }
}
