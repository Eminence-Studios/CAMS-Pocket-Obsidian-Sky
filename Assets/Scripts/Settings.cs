using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    // public Toggle fullscreenToggle;
    public Toggle vsyncToggle;

    private Resolution[] resolutions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setUpResolutions();
        loadSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    private void setUpResolutions()
    {
        Resolution[] allResolutions = Screen.resolutions;
        List<Resolution> uniqueResolutions = new List<Resolution>();
        List<string> options = new List<string>();

        // We define the target aspect ratio (16:9 = 1.77777...)
        float targetAspect = 16f / 9f;
        float marginOfError = 0.01f; // To catch small rounding differences

        for (int i = 0; i < allResolutions.Length; i++)
        {
            float currentAspect = (float)allResolutions[i].width / allResolutions[i].height;

            // 1. Check if it matches 16:9
            if (Mathf.Abs(currentAspect - targetAspect) <= marginOfError)
            {
                // 2. Check if it's already in our unique list (to avoid refresh rate dupes)
                if (!uniqueResolutions.Exists(r => r.width == allResolutions[i].width && r.height == allResolutions[i].height))
                {
                    uniqueResolutions.Add(allResolutions[i]);
                    string option = allResolutions[i].width + " x " + allResolutions[i].height;
                    options.Add(option);
                }
            }
        }

        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Ensure the saved index is still valid for this new filtered list
        int savedIndex = GameManager.Instance.currentData.resolutionIndex;
        resolutionDropdown.value = (savedIndex < resolutions.Length) ? savedIndex : resolutions.Length - 1;

        resolutionDropdown.RefreshShownValue();
        setResolution();
    }
    */

    private void setUpResolutions()
    {
        Resolution[] allResolutions = Screen.resolutions;
        List<Resolution> uniqueResolutions = new List<Resolution>();
        List<string> options = new List<string>();

        float targetAspect = 16f / 9f;
        float marginOfError = 0.01f;

        int fallback1080Index = -1; // We'll store the index of 1080p here if we find it

        for (int i = 0; i < allResolutions.Length; i++)
        {
            float currentAspect = (float)allResolutions[i].width / allResolutions[i].height;

            if (Mathf.Abs(currentAspect - targetAspect) <= marginOfError)
            {
                if (!uniqueResolutions.Exists(r => r.width == allResolutions[i].width && r.height == allResolutions[i].height))
                {
                    uniqueResolutions.Add(allResolutions[i]);
                    string option = allResolutions[i].width + " x " + allResolutions[i].height;
                    options.Add(option);

                    // Look for 1920x1080 specifically
                    if (allResolutions[i].width == 1920 && allResolutions[i].height == 1080)
                    {
                        fallback1080Index = uniqueResolutions.Count - 1;
                    }
                }
            }
        }

        resolutions = uniqueResolutions.ToArray();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // --- THE "NEW VS OLD" CHECK ---
        int savedIndex = GameManager.Instance.currentData.resolutionIndex;

        // Logic: If it's a new save (index is -1) and we found 1080p, use 1080p.
        // Otherwise, use the savedIndex.
        if (savedIndex == -1 && fallback1080Index != -1)
        {
            resolutionDropdown.value = fallback1080Index;
            // Update the data immediately so it's "set"
            GameManager.Instance.currentData.resolutionIndex = fallback1080Index;
        }
        else
        {
            // Use the previous choice (ensuring it's not out of bounds)
            resolutionDropdown.value = Mathf.Clamp(savedIndex, 0, resolutions.Length - 1);
        }

        resolutionDropdown.RefreshShownValue();
    }

    public void saveAndExit()
    {
        SpellbookManager.instance.closeCanvas("Settings");
        GameManager.Instance.currentData.lastLocation = SceneManager.GetActiveScene().buildIndex;
        Destroy(SpellbookManager.instance.gameObject);
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene(0);
    }

    public void setVolume()
    {
        AudioListener.volume = volumeSlider.value;

        GameManager.Instance.currentData.masterVolume = volumeSlider.value;
    }

    private void loadSettings()
    {
        var data = GameManager.Instance.currentData;

        // Sync UI elements to match the current save data
        volumeSlider.value = data.masterVolume;
        setVolume();

        // fullscreenToggle.isOn = data.isFullscreen;
        // setFullscreen();
        
        vsyncToggle.isOn = (data.vsyncCount == 1);
        setVSync();

        setResolution();
    }

    /*
    public void setResolution()
    {
        int resIndex = resolutionDropdown.value;
        if (resIndex >= resolutions.Length) return;

        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        GameManager.Instance.currentData.resolutionIndex = resIndex;
    }
    */
    public void setResolution()
    {
        int resIndex = resolutionDropdown.value;
        if (resIndex >= resolutions.Length) return;

        Resolution res = resolutions[resIndex];

        // This forces the game to stay in a maximized, borderless state
        // but renders the game at the specific resolution chosen.
        Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);

        GameManager.Instance.currentData.resolutionIndex = resIndex;
    }

    /*
    public void setFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        GameManager.Instance.currentData.isFullscreen = fullscreenToggle.isOn;
    }
    */
    public void setVSync()
    {
        // 0 = Off, 1 = On (Standard)
        int index = 0;
        if (vsyncToggle.isOn)
        {
            index = 1;   
        }
        QualitySettings.vSyncCount = index;
        GameManager.Instance.currentData.vsyncCount = index;
    }
}
