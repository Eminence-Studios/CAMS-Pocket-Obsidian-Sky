using System;
using Unity.VisualScripting;
using UnityEngine;

public class MultiPuzzleManager : MonoBehaviour
{
    public string mainPuzzleName;
    public int numOfPuzzles;
    private string[] componentPuzzles;

    public Boolean isTwoStep;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkAllComplete()
    {
        int sum = 0;
        for (int i = 1; i <= numOfPuzzles; i++)
        {
            sum += PlayerPrefs.GetInt(mainPuzzleName + i, 0);
        }
        if (sum == numOfPuzzles)
        {
            PlayerPrefs.SetInt(mainPuzzleName, 1);
            // Debug.Log("all puzzles solved");
        }
    }
}
