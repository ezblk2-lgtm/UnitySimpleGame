using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SettingsPanel : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public MainMenuPanel mainMenuPanel;

    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    void Start()
    {
        SetResolutionDropdown();
        LoadSettings();


        if (fullscreenToggle != null )
        {
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }

        if (resolutionDropdown != null )
        {
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        if (volumeSlider != null )
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void SetResolutionDropdown()
    {
        if (resolutionDropdown == null) return;

        Resolution[] allResolutions = Screen.resolutions;

        List<Resolution> uniqueResolutions = new List<Resolution>();
        HashSet<string> resolutionString = new HashSet<string>();

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string key = allResolutions[i].width + " X " + allResolutions[i].height;

            if (!resolutionString.Contains(key))
            {
                resolutionString.Add(key);
                uniqueResolutions.Add(allResolutions[i]);
            }
        }

        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutionIndex >=  resolutions.Length) return;

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            float normalizedVolume = volume / 100f;
            float db = Mathf.Log10(Mathf.Max(normalizedVolume, 0.0001f)) * 20f;
            audioMixer.SetFloat("MasterVolume", db);
        }
    }

    public void SaveSettings()
    {
        if (fullscreenToggle != null)
        {
            PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ?  1 : 0);
        }

        if (resolutionDropdown != null)
        {
            PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        }

        if (volumeSlider != null)
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        }

        PlayerPrefs.Save();
    }

    void LoadSettings()
    {

        if (fullscreenToggle  != null)
        {
            bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            fullscreenToggle.isOn = savedFullscreen;
            Screen.fullScreen = savedFullscreen;
        }

        int savedResolution = PlayerPrefs.GetInt("Resolution", -1);
        if (savedResolution >= 0 && resolutionDropdown != null &&
            savedResolution < resolutionDropdown.options.Count)
        {
            resolutionDropdown.value = savedResolution;
            SetResolution(savedResolution);
        }

        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 75f);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }

    }

    public void OnSettingClosed()
    {
        SaveSettings();

        mainMenuPanel.ShowMainMenu();
    }
}
