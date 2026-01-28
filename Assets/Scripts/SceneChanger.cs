using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int sceneID;
    public string playerPrefVariable;
    public int playerPrefValue;

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
        PlayerPrefs.SetInt(playerPrefVariable, playerPrefValue);
        SceneManager.LoadScene(sceneID);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        MoveToScene();
    }
}
