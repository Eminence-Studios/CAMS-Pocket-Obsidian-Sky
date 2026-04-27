using UnityEngine;

public class UIOverlay : MonoBehaviour
{
    public static UIOverlay Instance;

    void Awake()
    {
        // If an instance already exists, kill this new one immediately
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, make this the permanent instance
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}