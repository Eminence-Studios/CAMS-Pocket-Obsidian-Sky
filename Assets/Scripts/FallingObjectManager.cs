using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FallingObjectManager : MonoBehaviour
{
    [SerializeField] GameObject collider;
    [SerializeField] GameObject gameScreen;

    public Movement playerMovement;
    public string PlayerPrefVariable;

    public static FallingObjectManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Button retryButton;
    [SerializeField] Button closeButton;
    [SerializeField] TextMeshProUGUI buttonText;

    private int score = 0;
    public string startingText;

    public Rigidbody2D body;
    public int speed;

    public Boolean isCatch;
    private float time = 21;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        playerMovement.enableMovement = false;
        resultText.text = startingText;
        buttonText.text = "Start";
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

    }

    void Update()
    {
        if (isCatch)
        {
            scoreText.text = "Caught Items: " + score.ToString();
        }
        else
        {
            time -= Time.deltaTime;
            scoreText.text = "Time Left: " + ((int) time).ToString();
            if (time <= 0)
            {
                endGame();
            }
        }
    }

    public void start()
    {
        Time.timeScale = 1f;
        resultText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Debug.Log("restarted");
        
        score = 0;
        time = 21f;

        FallingObject[] leftovers = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);
        foreach (FallingObject obj in leftovers)
        {
            Destroy(obj.gameObject);
        }

    }

    public void endGame()
    {
        Time.timeScale = 0f;
       
        resultText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        if (score != 5 && time > 0)
        {
            resultText.text = "Fail";
            buttonText.text = "Retry";
            closeButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        else
        {
            ObjectSpawner.instance.gameObject.SetActive(false);
            collider.SetActive(false);
            resultText.text = "Success!";
            PlayerPrefs.SetInt(PlayerPrefVariable, 1);
            Time.timeScale = 1f;
            Invoke("closePuzzle", 2);
        }
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (isCatch)
        {
            score++;
            if (score == 5)
            {
                endGame();
            }
        }
    }

    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
        gameScreen.SetActive(false);
    }
}
