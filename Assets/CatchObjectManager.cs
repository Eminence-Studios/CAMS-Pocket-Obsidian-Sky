using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchObjectManager : MonoBehaviour
{
    [SerializeField] GameObject collider;
    [SerializeField] GameObject gameScreen;

    public Movement playerMovement;
    public string PlayerPrefVariable;

    public static CatchObjectManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Button retryButton;
    [SerializeField] Button closeButton;
    [SerializeField] TextMeshProUGUI buttonText;

    public int score = 0;
    public string startingText;

    public Rigidbody2D body;
    public int speed;

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
        resultText.text = startingText;
        buttonText.text = "Start";
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

    }

    public void start()
    {
        Time.timeScale = 1f;
        resultText.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        Debug.Log("restarted");
        
        score = 0;
        updateText();

        FallingObject[] leftovers = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);
        foreach (FallingObject obj in leftovers)
        {
            Destroy(obj.gameObject);
        }

    }

    public void updateText()
    {
        scoreText.text = "Caught Items: " + score.ToString();
    }


    public void endGame()
    {
        Time.timeScale = 0f;
       
        resultText.gameObject.SetActive(true);
        if (score != 5)
        {
            resultText.text = "Fail";
            buttonText.text = "Retry";
            closeButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
        else
        {
            collider.SetActive(false);
            resultText.text = "Success!";
            PlayerPrefs.SetInt(PlayerPrefVariable, 1);
            Invoke("closePuzzle", 2);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        updateText();
        if (score == 5)
        {
            endGame();
        }
        FallingObject.instance.destroyObject();
    }

    public void closePuzzle()
    {
        playerMovement.enableMovement = true;
        gameScreen.SetActive(false);
    }
}
