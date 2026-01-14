using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int sceneID;

    // 1 = North
    // 0 = South
    [SerializeField] public int direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToScene()
    {
        SceneManager.LoadScene(sceneID);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // 5000 Building
        if (sceneID == 7)
        {
            PlayerPrefs.SetInt("5000Side", direction);
        }
        // 6000 Building
        else if (sceneID == 8)
        {
            PlayerPrefs.SetInt("6000Side", direction);
        }
        MoveToScene();
    }
}
