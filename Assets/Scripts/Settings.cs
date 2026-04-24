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
    public Toggle fullscreenToggle;
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

    private void setUpResolutions()
    {
        Resolution[] allResolutions = Screen.resolutions;
        List<Resolution> uniqueResolutions = new List<Resolution>();
        List<string> options = new List<string>();

        for (int i = 0; i < allResolutions.Length; i++)
        {
            if (!uniqueResolutions.Exists(r => r.width == allResolutions[i].width && r.height == allResolutions[i].height))
            {
                uniqueResolutions.Add(allResolutions[i]);
                string option = allResolutions[i].width + " x " + allResolutions[i].height;
                options.Add(option);
            }
        }

        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = GameManager.Instance.currentData.resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void saveAndExit()
    {
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
        fullscreenToggle.isOn = data.isFullscreen;
        vsyncToggle.isOn = (data.vsyncCount == 1);
    }

    public void setResolution()
    {
        int resIndex = resolutionDropdown.value;
        if (resIndex >= resolutions.Length) return;

        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        GameManager.Instance.currentData.resolutionIndex = resIndex;
    }
    public void setFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        GameManager.Instance.currentData.isFullscreen = fullscreenToggle.isOn;
    }
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
