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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (loadLocation)
        { 
            createLocations();

            location = PlayerPrefs.GetInt(playerPrefLocation, 0);

            if (playerPrefLocation.Equals("CampusLocation"))
            {
                player.position = locationsCampus[location];
            }
            else if (playerPrefLocation.Equals("1000Location"))
            {
                player.position = locations1000[location];
            }
            else if (playerPrefLocation.Equals("2000Location"))
            {
                player.position = locations2000[location];
            }
            else if (playerPrefLocation.Equals("3000Location"))
            {
                player.position = locations3000[location];
            }
            else if (playerPrefLocation.Equals("4000Location"))
            {
                player.position = locations4000[location];
            }
            else if (playerPrefLocation.Equals("6000Location"))
            {
                player.position = locations6000[location];
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

    void createLocations()
    {
        // 1000. 2000, 3000, 4000, 6000
        locationsCampus = new Vector3[] { new Vector3(970, -1450, 0), new Vector3(700, -850, 0), new Vector3(700, 500, 0), new Vector3(700, 1850, 0), new Vector3(-800, -100, 0) };

        // campus, Bodeur, Davis, Imatomi, Gonzales
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
