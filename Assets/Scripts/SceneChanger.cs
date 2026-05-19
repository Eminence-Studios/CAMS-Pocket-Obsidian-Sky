using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int sceneID;
 
    // Scene for which location is being saved. Corresponds with currentLocations list in GameData
    public int locationIndex;
    
    // Actual location in scene. Index of location from LoadScene
    public int locationValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame(int slot)
    {
        GameManager.Instance.startSlotGame(slot);
    }

    public void MoveToScene()
    {
        if (locationIndex != -1)
        {
            GameManager.Instance.currentData.currentLocations[locationIndex] = locationValue;
            GameManager.Instance.currentData.lastLocation = sceneID;
            GameManager.Instance.SaveGame();
        }
        SceneManager.LoadScene(sceneID);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        MoveToScene();
    }
}
