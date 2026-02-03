using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class CatchingObjUIManager : MonoBehaviour
{
    public static CatchingObjUIManager instance;
    [SerializeField] private TMP_Text screenText;
    public GameObject endText;
    public GameObject endButton;
    public GameObject winText;
    void Awake()
    {
        instance = this;
        endText.SetActive(false);
        endButton.SetActive(false);
        winText.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 5)
        {
            winGame();
        }
    }
    public int score = 0;
    public void updateText()
    {
        screenText.text = score.ToString();
    }

    public void loseGame()
    {
        endText.SetActive(true);
        endButton.SetActive(true);
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void winGame()
    {
        winText.SetActive(true);
        Time.timeScale = 0f;
    }
}
