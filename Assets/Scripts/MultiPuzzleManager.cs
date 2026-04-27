using System;
using Unity.VisualScripting;
using UnityEngine;

public class MultiPuzzleManager : MonoBehaviour
{
    public string teacherName;
    public int numOfPuzzles;
    private string[] componentPuzzles;

    // public Boolean isTwoStep;
    public int startSearchIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void checkAllComplete()
    {
        int completedCount = 0;
        // Set startSearchIndex to 1 for Nishiyama, 0 for others
        for (int i = startSearchIndex; i < (startSearchIndex + numOfPuzzles); i++)
        {
            if (GameManager.Instance.isPuzzleComplete(teacherName, i))
            {
                completedCount++;
            }
        }

        if (completedCount >= numOfPuzzles)
        {
            TeacherProgress data = GameManager.Instance.getTeacher(teacherName);
            data.puzzle = true;
            GameManager.Instance.SaveGame();
        }
    
    }
    */
}
