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

    private int screenResY;
    private int screenResX;
    private int screenResKey;
    private int screenResXtemp;
    private int screenResYtemp;

    private bool fullScreen;
    private bool fullScreenTemp;
    private int fullScreenKey;

    private float volumeLevel;
    private float mouseSensitivity;
    
    private bool invertY;
    private bool invertYTemp;
    private int invertYKey;

    AudioConfiguration speakerConfig;
    private int speakerKey;
    private int speakerTemp;
    private int speakerInit;
    public AudioSource[] resumeAudio;

    // Load game async coroutine
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = Application.LoadLevelAsync("City Outskirts");

        while (!async.isDone)
        {
            yield return null;
        }        
    }

    void Awake()
    {
        // Load current settings
        speakerConfig = AudioSettings.GetConfiguration();
        screenResKey = PlayerPrefs.GetInt("Screen Res");
        speakerKey = PlayerPrefs.GetInt("Speaker Config");
        fullScreenKey = PlayerPrefs.GetInt("Fullscreen");
        invertYKey = PlayerPrefs.GetInt("Invert Toggle");

        // Set default values if there are no settings yet
        if (!PlayerPrefs.HasKey("Speaker Config"))
        {
            speakerInit = 0;
        }

        if (!PlayerPrefs.HasKey("Mouse Sensitivity"))
        {
            EventManager.inst.mouseSensitivty = 5;
            PlayerPrefs.SetFloat("Mouse Sensitivity", 5);
            mouseSensitivity = 5;
        }

        if (!PlayerPrefs.HasKey("Master Volume"))
        {
            EventManager.inst.masterVolume = 1;
            PlayerPrefs.SetFloat("Master Volume", 1);
            volumeLevel = 1;
        }
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
        Application.LoadLevel("Credits");
    }

    // Options Button
    public void OptionsButton()
    {
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
        ShowMenuButtons();
        HideOptionsButtons();
    }

    // Exit Button
    public void ExitButton()
    {
        Application.Quit();
    }

    void FixedUpdate()
    {
        UpdateUIvalues();
        Cursor.visible = true; // Dodgey over ride?
    }

    void ApplySettings()
    {
        // Assign global variables
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.mouseSensitivty = mouseSensitivity;
        EventManager.inst.invertY = invertY;

        // Save values to player prefs
        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
        PlayerPrefs.SetFloat("Mouse Sensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("Screen Res", screenDrop.value);
        PlayerPrefs.SetInt("Speaker Config", speakerDrop.value);

        // Switch between windowed and full screen mode
        if (fullscreenToggle.isOn == false)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else PlayerPrefs.SetInt("Fullscreen", 1);

        // Apply invert Y toggle
        if (invertToggle.isOn == false)
        {
            EventManager.inst.invertY = false;
            PlayerPrefs.SetInt("Invert Toggle", 0);
        }

        // Remove invert Y toggle
        if (invertToggle.isOn == true)
        {
            EventManager.inst.invertY = true;
            PlayerPrefs.SetInt("Invert Toggle", 1);
        }
        
        // Adjust screen res if there are changes
        if (screenResX != screenResXtemp || screenResY != screenResYtemp || fullScreen != fullScreenTemp)
        {
            Screen.SetResolution(screenResXtemp, screenResYtemp, fullScreenTemp);
        }

        // Reset audio config if there are any changes
        if (speakerKey != speakerTemp)
        {
            AudioSettings.Reset(speakerConfig);
            speakerKey = speakerTemp;
            // Set all audio sources in the scene to play after restarting
            for (int i = 0; i < resumeAudio.Length; i++)
            {
                resumeAudio[i].Play();
            }
        }
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

        // Invert Y val
        if (invertYKey == 0)
        {
            invertToggle.isOn = false;
        }
        else invertToggle.isOn = true;
    }

    void UpdateUIvalues()
    {
        if (fullscreenToggle.isOn)
        {
            fullScreenTemp = true;
        }
        else fullScreenTemp = false;

        if (invertToggle.isOn)
        {
            invertYTemp = true;
        }
        else invertYTemp = false;

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
            speakerTemp = 0;
            speakerConfig.speakerMode = AudioSpeakerMode.Stereo;
        }

        if (speakerDrop.value == 1)
        {
            speakerTemp = 1;
            speakerConfig.speakerMode = AudioSpeakerMode.Stereo;
        }

        if (speakerDrop.value == 2)
        {
            speakerTemp = 2;
            speakerConfig.speakerMode = AudioSpeakerMode.Mode5point1;
        }

        if (speakerDrop.value == 3)
        {
            speakerTemp = 3;
            speakerConfig.speakerMode = AudioSpeakerMode.Mode7point1;
        }

        volumeLevel = volSlider.value;
        mouseSensitivity = sensSlider.value;
        invertY = invertToggle;
    }

    // Functions to show/hude UI groups
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
