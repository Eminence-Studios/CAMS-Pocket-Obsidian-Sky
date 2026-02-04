using System;
using System.Collections;
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
    public int catchAmt;
    public float dodgeTime;

    private float time;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = dodgeTime + 1;
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

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y, 0);
        transform.eulerAngles = newRotation;

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
        StartCoroutine(waitUntilScreenCleared());
        Time.timeScale = 1f;
        resultText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        Debug.Log("restarted");
        
        score = 0;
        time = dodgeTime + 1;

        

    }

    public bool clearScreen()
    {
        FallingObject[] leftovers = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);
        foreach (FallingObject obj in leftovers)
        {
            Destroy(obj.gameObject);
        }
        return true;
    }

    private IEnumerator waitUntilScreenCleared()
    {
        yield return new WaitUntil(clearScreen);
    }
    public void endGame()
    {
        Time.timeScale = 0f;
        StartCoroutine(waitUntilScreenCleared());
        
        resultText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        if (score != catchAmt && time > 0)
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
            if (score == catchAmt)
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
