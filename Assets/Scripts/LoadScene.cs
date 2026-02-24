using UnityEngine;

public class LoadScene : MonoBehaviour
{

    private int location;

    public bool loadLocation;
    public string playerPrefLocation;
    [SerializeField] Transform player;

    public bool loadTrigger;
    public string[] playerPrefTrigger;
    public GameObject[] triggers;

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
        if (loadLocation)
        { 
            createLocations();
            numOfElements = PlayerPrefs.GetInt("numOfMasteredElements", 0);
            location = PlayerPrefs.GetInt(playerPrefLocation, 0);

            if (playerPrefLocation.Equals("CampusLocation"))
            {
                player.position = locationsCampus[location];
            }
            else if (playerPrefLocation.Equals("1000Location"))
            {
                player.position = locations1000[location];
                load1000Hallway();
            }
            else if (playerPrefLocation.Equals("2000Location"))
            {
                player.position = locations2000[location];
                load2000Hallway();
            }
            else if (playerPrefLocation.Equals("3000Location"))
            {
                player.position = locations3000[location];
                load3000Hallway();
            }
            else if (playerPrefLocation.Equals("4000Location"))
            {
                player.position = locations4000[location];
                load4000Hallway();
            }
            else if (playerPrefLocation.Equals("6000Location"))
            {
                player.position = locations6000[location];
                load6000Hallway();
            }
        }
        if (loadTrigger)
        {
            for (int i = 0; i < playerPrefTrigger.Length; i++)
            {
                if (PlayerPrefs.GetInt(playerPrefTrigger[i], 0) == 1)
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


    void createLocations()
    {
        // 1000. 2000, 3000, 4000, 6000
        locationsCampus = new Vector3[] { new Vector3(970, -1450, 0), new Vector3(700, -850, 0), new Vector3(700, 500, 0), new Vector3(700, 1850, 0), new Vector3(-800, -100, 0) };

        // campus, Brodeur, Davis, Imatomi, Gonzales
        locations1000 = new Vector3[] { new Vector3(-1400, 80, 0), new Vector3(-1000, -50, 0), new Vector3(-600, -50, 0), new Vector3(700, -50, 0), new Vector3(1100, -50, 0)};

        // campus, Sarno, Almeida, Brown, Luu
        locations2000 = new Vector3[] { new Vector3(-1200, 0, 0), new Vector3(-950, 100, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0) };

        // campus, Maestas, Lee, Nishiyama
        locations3000 = new Vector3[] { new Vector3(-1200, 50, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0)};

        // campus, Dreyfus, Johns, Virak, Bucko
        locations4000 = new Vector3[] { new Vector3(-1200, 0, 0), new Vector3(-950, 100, 0), new Vector3(-600, 100, 0), new Vector3(750, 100, 0), new Vector3(1100, 100, 0) };

        // campus, King, Fuentes, Johnson, Carpenter, Marquez, Gallardo, Brito, Avalos, Luna
        locations6000 = new Vector3[] {new Vector3(0, 2300, 0), new Vector3(-600, 2000, 0), new Vector3(-600, 500, 0), new Vector3(-600, 0, 0)
        , new Vector3(600, 250, 0), new Vector3(600, -800, 0), new Vector3(600, -1250, 0), new Vector3(-600, -1500, 0)
        , new Vector3(600, -2000, 0), new Vector3(-600, -2000, 0) };
    }
}
