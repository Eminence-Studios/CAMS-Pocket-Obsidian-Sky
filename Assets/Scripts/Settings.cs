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
    public Toggle vsyncToggle;

    private Resolution[] resolutions;
    public bool startingScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setUpResolutions();
        LoadEffectiveSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

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

        if (!startingScreen)
        {
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
        }
        else
        {
            resolutionDropdown.value = fallback1080Index;
        }

        resolutionDropdown.RefreshShownValue();
    }

    

    /*
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
    */

    private void LoadEffectiveSettings()
    {
        if (startingScreen)
        {
            // Load from Global PlayerPrefs (the last settings used on this PC)
            volumeSlider.value = PlayerPrefs.GetFloat("GlobalVolume", 0.8f);
            vsyncToggle.isOn = PlayerPrefs.GetInt("GlobalVSync", 1) == 1;

            // For Resolution, we use the saved Global Index
            int globalRes = PlayerPrefs.GetInt("GlobalResIndex", -1);
            if (globalRes != -1) resolutionDropdown.value = globalRes;
        }
        else
        {
            // Inside a game: Load from the specific slot data
            var data = GameManager.Instance.currentData;
            volumeSlider.value = data.masterVolume;
            vsyncToggle.isOn = (data.vsyncCount == 1);
            resolutionDropdown.value = data.resolutionIndex;
        }

        // Apply them visually and to the engine
        ApplyAllSettings();
    }


    public void ApplyAllSettings()
    {
        setVolume();
        setVSync();
        setResolution();
    }

    public void setVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("GlobalVolume", volumeSlider.value); // Always update global

        if (!startingScreen && GameManager.Instance.currentData != null)
        {
            GameManager.Instance.currentData.masterVolume = volumeSlider.value;
        }
    }

    public void setVSync()
    {
        int index = vsyncToggle.isOn ? 1 : 0;
        QualitySettings.vSyncCount = index;
        PlayerPrefs.SetInt("GlobalVSync", index); // Always update global

        if (!startingScreen && GameManager.Instance.currentData != null)
        {
            GameManager.Instance.currentData.vsyncCount = index;
        }
    }

    public void setResolution()
    {
        int resIndex = resolutionDropdown.value;
        if (resolutions == null || resIndex >= resolutions.Length) return;

        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);

        PlayerPrefs.SetInt("GlobalResIndex", resIndex); // Always update global

        if (!startingScreen && GameManager.Instance.currentData != null)
        {
            GameManager.Instance.currentData.resolutionIndex = resIndex;
        }
    }
}
