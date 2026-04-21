using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject basic;
    public GameObject spells;

    void Awake()
    {
        spells.SetActive(false);
    }

    public void spellB()
    {
        basic.SetActive(false);
        spells.SetActive(true);
    }
    public void backB()
    {
        spells.SetActive(false);
        basic.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
