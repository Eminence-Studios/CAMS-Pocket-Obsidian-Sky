using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LoadScene : MonoBehaviour
{

    // private int location;

    [Header("Location Parameters")]
    public bool loadLocation;
    public int locationIndex;
    [SerializeField] Transform player;

    [Header("Trigger Parameters")]
    public bool loadTrigger;
    public int totalPuzzleCount;
    public string teacherName;
    public GameObject[] triggers;

    [Header("Campus Map Parameters")]
    public GameObject agulto;
    public GameObject[] hallways;

    [Header("Corner Map Parameters")]
    public Sprite sceneMap;

    public SpellbookManager spellbookManager;

    private Vector3[] locationsCampus;
    private Vector3[] locations1000;
    private Vector3[] locations2000;
    private Vector3[] locations3000;
    private Vector3[] locations4000;
    private Vector3[] locations6000;

    private int currentTrigger;
    private int numOfElements;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        if (!PlayerPrefs.HasKey("NishiyamaNotReady"))
        {
            PlayerPrefs.SetInt("NishiyamaNotReady", 1);
            // PlayerPrefs.SetFloat("SFXVolume", 1f);
        }
        */

        if (loadLocation)
        {

            createLocations();
            numOfElements = GameManager.Instance.currentData.numOfMasteredElements;
            // location = PlayerPrefs.GetInt(playerPrefLocation, 0);

            SpellbookManager.instance.gameObject.SetActive(true);

            if (locationIndex == 0)
            {
                player.position = locationsCampus[GameManager.Instance.currentData.currentLocations[0]];

                /*
                GameObject rootCanvas = spellbookManager.transform.root.gameObject;
                
                if (SpellbookManager.instance != null && SpellbookManager.instance != spellbookManager)
                {
                    updateOverlay();

                    Destroy(rootCanvas);
                    return;
                }
                

                SpellbookManager.instance = spellbookManager;
                DontDestroyOnLoad(rootCanvas);
                */
                updateOverlay();
            }
            else if (locationIndex == 1)
            {
                player.position = locations1000[GameManager.Instance.currentData.currentLocations[1]];
                SpellbookManager.instance.showMapIcon();
                SpellbookManager.instance.updateMap(sceneMap);
                load1000Hallway();
            }
            else if (locationIndex == 2)
            {
                player.position = locations2000[GameManager.Instance.currentData.currentLocations[2]];
                SpellbookManager.instance.showMapIcon();
                SpellbookManager.instance.updateMap(sceneMap);
                load2000Hallway();
            }
            else if (locationIndex == 3)
            {
                player.position = locations3000[GameManager.Instance.currentData.currentLocations[3]];
                SpellbookManager.instance.showMapIcon();
                SpellbookManager.instance.updateMap(sceneMap);
                load3000Hallway();
            }
            else if (locationIndex == 4)
            {
                player.position = locations4000[GameManager.Instance.currentData.currentLocations[4]];
                SpellbookManager.instance.showMapIcon();
                SpellbookManager.instance.updateMap(sceneMap);
                load4000Hallway();
            }
            else if (locationIndex == 5)
            {
                player.position = locations6000[GameManager.Instance.currentData.currentLocations[5]];
                SpellbookManager.instance.showMapIcon();
                SpellbookManager.instance.updateMap(sceneMap);
                load6000Hallway();
            }
        }
        if (loadTrigger)
        {
            SpellbookManager.instance.hideMapIcon();

            GameManager.Instance.createTeacher(teacherName, totalPuzzleCount);
            TeacherProgress data = GameManager.Instance.getTeacher(teacherName);

            int startSearchIndex = 0;
            if (teacherName.Equals("Nishiyama"))
            {
                if (data.subPuzzles[0] == true)
                {
                    triggers[0].SetActive(false);
                    startSearchIndex = 1;
                }
                else
                {
                    foreach(var trigger in triggers)
                    {
                        trigger.SetActive(false);
                    }
                }
            }
            for (int i = startSearchIndex; i < startSearchIndex + data.subPuzzles.Length; i++)
            {
                if (data.subPuzzles[i])
                {
                    triggers[i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void load1000Hallway()
    {
        // triggers: Brodeur, Davis, Gonzales
        // Freshman Year
        if (numOfElements < 1)
        {
            // hide sophomore classes
            triggers[0].SetActive(false);
            triggers[2].SetActive(false);
        }
        // Sophomore Year
        if (numOfElements < 2)
        {
            // hide junior classes
            triggers[1].SetActive(false);
        }
        // no senior classes in 1000 hallway
    }

    void load2000Hallway()
    {
        // triggers: Sarno, Almeida, Brown, Luu
        // Freshman Year
        if (numOfElements < 1)
        {
            // hide sophomore classes
            triggers[0].SetActive(false);
            triggers[1].SetActive(false);
            triggers[3].SetActive(false);
        }
        // Sophomore Year
        if (numOfElements < 2)
        {
            // hide junior classes
            triggers[2].SetActive(false);
        }
        // no senior classes in 2000 hallway
    }

    void load3000Hallway()
    {
        // triggers: Nishiyama

        // no sophomore or junior classes in 3000 hallway

        // Junior Year
        if (numOfElements < 3)
        {
            // hide senior classes
            triggers[0].SetActive(false);
        }
    }

    void load4000Hallway()
    {
        // triggers: Dreyfus, Johns, Virak, Bucko
        // Freshman Year
        if (numOfElements < 1)
        {
            // hide sophomore classes
            triggers[2].SetActive(false);
        }
        // Sophomore Year
        if (numOfElements < 2)
        {
            // hide junior classes
            triggers[3].SetActive(false);
        }
        // Junior Year
        if (numOfElements < 3)
        {
            // hide senior classes
            triggers[0].SetActive(false);
            triggers[1].SetActive(false);
        }
    }

    void load6000Hallway()
    {
        // triggers: Fuentes, Gallardo, Brito, Avalos

        // no sophomore classes in 6000 hallway
        
        // Sophomore Year
        if (numOfElements < 2)
        {
            // hide junior classes
            triggers[1].SetActive(false);
            triggers[2].SetActive(false);
            triggers[3].SetActive(false);
        }
        // Junior Year
        if (numOfElements < 3)
        {
            // hide senior classes
            triggers[0].SetActive(false);
        }
    }

    void hideHallways()
    {
        foreach (var trigger in hallways)
        {
            trigger.gameObject.SetActive(false);
        }
    }

    void updateOverlay()
    {
        if (SpellbookManager.instance == null)
        {
            return;
        }

        GameObject root = SpellbookManager.instance.gameObject;

        if (!GameManager.Instance.currentData.tutorialComplete)
        {
            agulto.SetActive(true);
            root.SetActive(false);
            hideHallways();
        }
        else
        {
            agulto.SetActive(false); // Make sure Agulto is gone
            root.SetActive(true);
            SpellbookManager.instance.showMapIcon();
            SpellbookManager.instance.updateMap(sceneMap);
        }
    }


    void createLocations()
    {
        // default, 1000. 2000, 3000, 4000, 6000
        locationsCampus = new Vector3[] { new Vector3(300, -2000, 0), new Vector3(963, -2268, 0), new Vector3(1465, -1166, 0), new Vector3(1462, 317, 0), new Vector3(1454, 1539, 0), new Vector3(-1215, -436, 0) };

        // campus, Brodeur, Davis, Imatomi, Gonzales
        locations1000 = new Vector3[] { new Vector3(-1400, 80, 0), new Vector3(-1000, -50, 0), new Vector3(-600, -50, 0), new Vector3(700, -50, 0), new Vector3(1100, -50, 0)};

        // campus, Sarno, Almeida, Brown, Luu
        locations2000 = new Vector3[] { new Vector3(-1200, 0, 0), new Vector3(-950, 100, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0) };

        // campus, Maestas, Lee, Nishiyama
        locations3000 = new Vector3[] { new Vector3(-1200, 50, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0)};

        // campus, Dreyfus, Johns, Virak, Bucko
        locations4000 = new Vector3[] { new Vector3(-1200, 0, 0), new Vector3(-950, 100, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0) };

        // campus, King, Fuentes, Johnson, Carpenter, Marquez, Gallardo, Brito, Avalos, idLuna
        locations6000 = new Vector3[] {new Vector3(0, 2300, 0), new Vector3(-600, 2000, 0), new Vector3(-600, 500, 0), new Vector3(-600, 0, 0)
        , new Vector3(600, 250, 0), new Vector3(600, -800, 0), new Vector3(600, -1250, 0), new Vector3(-600, -1500, 0)
        , new Vector3(600, -2000, 0), new Vector3(-600, -2000, 0) };
    }
}
