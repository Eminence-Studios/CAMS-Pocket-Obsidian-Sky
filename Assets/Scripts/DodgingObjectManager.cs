using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DodgingObjectManager : MonoBehaviour
{
    [SerializeField] GameObject collider;
    [SerializeField] GameObject gameScreen;

    public Movement playerMovement;
    public string PlayerPrefVariable;

    public static DodgingObjectManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] Button retryButton;
    [SerializeField] Button closeButton;
    [SerializeField] TextMeshProUGUI buttonText;
    // [SerializeField] Camera camera;

    private int score = 0;
    public string startingText;

    public float ground;

    public Rigidbody2D body;
    public int speed;

    public Boolean isCatch;
    public Boolean isRising;
    public int catchAmt;
    public float dodgeTime;

    private float time;

    public float jumpForce;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded = true;
    private float rotationY = 0;

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
        // ground += camera.transform.position.y;
        transform.localPosition = new Vector3(transform.localPosition.x, ground + 10, 0);

        playerMovement.enableMovement = false;
        SpellbookManager.instance.transform.root.gameObject.SetActive(false);

        resultText.text = startingText;
        buttonText.text = "Start";
        scoreText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y, 0);
        transform.eulerAngles = newRotation;
        */

        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotationY = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotationY = 180f;
        }

        transform.rotation = Quaternion.Euler(0, rotationY, 0);


        
        // Debug.Log("y" + transform.position.y);
        // Debug.Log("x" + transform.position.x);
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
            isGrounded = false;
        }

        if (transform.localPosition.y < ground)
        {
            isGrounded = true;
            velocity.y = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, ground + 10, transform.localPosition.z);
        }

        if (!isGrounded)
        {
            velocity.y -= jumpForce * Time.deltaTime * 2;
        }
        else
        {
            velocity.y = 0;
        }

        
        velocity.x = Input.GetAxis("Horizontal") * speed;

        transform.localPosition += velocity * Time.deltaTime;
        Debug.Log(velocity.y);
        

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
        isGrounded = true;
        velocity = Vector3.zero;
        transform.localPosition = new Vector3(transform.localPosition.x, ground + 10, 0);
        
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
        if (isRising)
        {
            
            RisingObject[] leftovers = FindObjectsByType<RisingObject>(FindObjectsSortMode.None);
            foreach (RisingObject obj in leftovers)
            {
                Destroy(obj.gameObject);
            }
            return true;
        }
        else
        {
            FallingObject[] leftovers = FindObjectsByType<FallingObject>(FindObjectsSortMode.None);
            foreach (FallingObject obj in leftovers)
            {
                Destroy(obj.gameObject);
            }
            return true;
        }
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
        if ((isCatch && score != catchAmt) || (!isCatch && time > 0))
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
        SpellbookManager.instance.transform.root.gameObject.SetActive(true);
        gameScreen.SetActive(false);
    }

}