using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject fbButton;
    [SerializeField] private GameObject fbText;

    private void Awake()
    {
        Instance = this;
        fbButton.SetActive(false);
        fbText.SetActive(false);
    }

    public void ShowFireballUI()
    {
        fbButton.SetActive(true);
        fbText.SetActive(true);
    }
}
