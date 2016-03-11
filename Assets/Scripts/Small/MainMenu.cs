using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public GameObject[] loadingScreenUI;
    public GameObject[] mainMenuUI;
    public GameObject[] optionMenuUI;

    public Slider volSlider;
    public Slider sensSlider;

    public Toggle invertToggle;
    public Toggle fullscreenToggle;

    public Dropdown screenDrop;
    public Dropdown speakerDrop;

    public int screenResY;
    public int screenResX;
    public int screenResKey;
    public int screenResXtemp;
    public int screenResYtemp;
    public bool fullScreen;
    public bool fullScreenTemp;
    public int fullScreenKey;

    public float volumeLevel;
    public float mouseSensitivity;
    public bool invertY;

    AudioConfiguration speakerConfig;
    public int speakerKey;
    public int speakerTemp;
    public int speakerInit;

    // Load game async coroutine
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = Application.LoadLevelAsync("City");

        while (!async.isDone)
        {
            yield return null;
        }        
    }

    void Awake()
    {
        speakerConfig = AudioSettings.GetConfiguration();
        screenResKey = PlayerPrefs.GetInt("Screen Res");
        speakerKey = PlayerPrefs.GetInt("Speaker Config");
        fullScreenKey = PlayerPrefs.GetInt("Fullscreen");
    }

    void Start()
    {
        InitialiseValues();
    }
    
    // Play Button
    public void PlayButton()
    {
        //print("Pressed Play");
        Cursor.visible = false;
        HideMenuButtons();
        for (int i = 0; i < loadingScreenUI.Length; i++)
        {
            loadingScreenUI[i].SetActive(true);
        }
        StartCoroutine("LoadScene");
    }

    // Credits Button
    public void CreditsButton()
    {
        //print("Pressed Credits");
        Application.LoadLevel("Credits");
    }

    // Options Button
    public void OptionsButton()
    {
        //print("Pressed Options");
        // Set initial values
        volSlider.value = EventManager.inst.masterVolume;
        sensSlider.value = EventManager.inst.mouseSensitivty;
        invertY = EventManager.inst.invertY;
        HideMenuButtons();
        ShowOptionsButtons();
    }

    // Apply Button
    public void ApplyButton()
    {
        ApplySettings();      
    }

    // Accept Button
    public void AcceptButton()
    {
        ApplySettings();
        ShowMenuButtons();
        HideOptionsButtons();
    }

    // Cancel Button
    public void CancelButton()
    {
        //print("Pressed Cancel");
        ShowMenuButtons();
        HideOptionsButtons();
    }

    // Exit Button
    public void ExitButton()
    {
        //print("Pressed Exit");
        Application.Quit();
    }

    void FixedUpdate()
    {
        UpdateUIvalues();
        Cursor.visible = true; // Dodgey over ride?
    }

    void ApplySettings()
    {
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.mouseSensitivty = mouseSensitivity;
        EventManager.inst.invertY = invertY;
        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
        PlayerPrefs.SetFloat("Mouse Sensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("Screen Res", screenDrop.value);
        PlayerPrefs.SetInt("Speaker Config", speakerDrop.value);

        if (fullscreenToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else PlayerPrefs.SetInt("Fullscreen", 1);

        if (screenResX != screenResXtemp || screenResY != screenResYtemp || fullScreen != fullScreenTemp)
        {
            Screen.SetResolution(screenResXtemp, screenResYtemp, fullScreenTemp);
        }

        AudioSettings.Reset(speakerConfig);
        print(AudioSettings.speakerMode);
    }

    void InitialiseValues()
    {
        screenDrop.value = screenResKey;
        speakerDrop.value = speakerKey;

        // Screen res vals
        if (screenResKey == 0)
        {
            screenResX = 1024;
            screenResY = 768;
        }

        if (screenResKey == 1)
        {
            screenResX = 1280;
            screenResY = 720;
        }

        if (screenResKey == 2)
        {
            screenResX = 1280;
            screenResY = 768;
        }

        if (screenResKey == 3)
        {
            screenResX = 1280;
            screenResY = 800;
        }

        if (screenResKey == 4)
        {
            screenResX = 1280;
            screenResY = 1024;
        }

        if (screenResKey == 5)
        {
            screenResX = 1360;
            screenResY = 768;
        }

        if (screenResKey == 6)
        {
            screenResX = 1366;
            screenResY = 768;
        }

        if (screenResKey == 7)
        {
            screenResX = 1680;
            screenResY = 1050;
        }

        if (screenResKey == 8)
        {
            screenResX = 1920;
            screenResY = 1080;
        }

        if (screenResKey == 9)
        {
            screenResX = 2175;
            screenResY = 1527;
        }

        // Speaker vals
        if (speakerKey == 0)
        {
            speakerInit = 0;
        }

        if (speakerKey == 1)
        {
            speakerInit = 1;
        }

        if (speakerKey == 2)
        {
            speakerInit = 2;
        }

        if (speakerKey == 3)
        {
            speakerInit = 3;
        }

        // Fullscreen val
        if (fullScreenKey == 0)
        {
            fullscreenToggle.isOn = false;
        }
        else fullscreenToggle.isOn = true;
    }

    void UpdateUIvalues()
    {
        if (fullscreenToggle.isOn)
        {
            fullScreenTemp = true;
        }
        else fullScreenTemp = false;

        // TODO: these should be states
        if (screenDrop.value == 0)
        {
            screenResXtemp = 1024;
            screenResYtemp = 768;
        }

        if (screenDrop.value == 1)
        {
            screenResXtemp = 1280;
            screenResYtemp = 720;
        }

        if (screenDrop.value == 2)
        {
            screenResXtemp = 1280;
            screenResYtemp = 768;
        }

        if (screenDrop.value == 3)
        {
            screenResXtemp = 1280;
            screenResYtemp = 800;
        }

        if (screenDrop.value == 4)
        {
            screenResXtemp = 1280;
            screenResYtemp = 1024;
        }

        if (screenDrop.value == 5)
        {
            screenResXtemp = 1360;
            screenResYtemp = 768;
        }

        if (screenDrop.value == 6)
        {
            screenResXtemp = 1366;
            screenResYtemp = 768;
        }

        if (screenDrop.value == 7)
        {
            screenResXtemp = 1680;
            screenResYtemp = 1050;
        }

        if (screenDrop.value == 8)
        {
            screenResXtemp = 1920;
            screenResYtemp = 1080;
        }

        if (screenDrop.value == 9)
        {
            screenResXtemp = 2175;
            screenResYtemp = 1527;
        }
        
        // Speaker vals
        if (speakerDrop.value == 0)
        {
            speakerConfig.speakerMode = AudioSpeakerMode.Stereo;
        }

        if (speakerDrop.value == 1)
        {
            speakerConfig.speakerMode = AudioSpeakerMode.Stereo;
        }

        if (speakerDrop.value == 2)
        {
            speakerConfig.speakerMode = AudioSpeakerMode.Mode5point1;
        }

        if (speakerDrop.value == 3)
        {
            speakerConfig.speakerMode = AudioSpeakerMode.Mode7point1;
        }

        volumeLevel = volSlider.value;
        mouseSensitivity = sensSlider.value;
        invertY = invertToggle;
    }

    #region simple functions
    void ShowOptionsButtons()
    {
        for (int i = 0; i < optionMenuUI.Length; i++)
        {
            optionMenuUI[i].SetActive(true);
        }
    }

    void HideOptionsButtons()
    {
        for (int i = 0; i < optionMenuUI.Length; i++)
        {
            optionMenuUI[i].SetActive(false);
        }
    }

    void ShowMenuButtons()
    {
        for (int i = 0; i < mainMenuUI.Length; i++)
        {
            mainMenuUI[i].SetActive(true);
        }
    }

    void HideMenuButtons()
    {
        for (int i = 0; i < mainMenuUI.Length; i++)
        {
            mainMenuUI[i].SetActive(false);
        }
    }
    #endregion
}
