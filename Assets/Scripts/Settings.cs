using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveAndExit()
    {
        GameManager.Instance.currentData.lastLocation = SceneManager.GetActiveScene().buildIndex;
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene(0);
    }
}
