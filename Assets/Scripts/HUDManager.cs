using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] public TMP_Text _text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateText(int CurrentItem1)
    {
        _text.text = CurrentItem1.ToString();
    }
}
