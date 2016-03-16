using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public enum MenuToggle
    {
        MAIN, OPTIONS, PLAY, CANCEL, NONE
    }

    public MenuToggle menuToggle;

    public List<int> screenResXlist = new List<int>();
    public List<int> screenResYlist = new List<int>();
    public List<AudioSpeakerMode> speakerConfigList = new List<AudioSpeakerMode>();

    public GameObject loadingScreenUI;
    public GameObject[] mainMenuUI;
    public GameObject[] optionMenuUI;
    public GameObject[] playMenuUI;

    public Transform currentCamPos;
    public Transform mainMenuPos;
    public Transform optionMenuPos;
    public Transform playPos;
    public float cameraPanSpeed;
    private float cameraPanIncrement;

    public Slider volSlider;
    public Slider sensSlider;

    public Toggle invertToggle;
    public Toggle fullscreenToggle;

    public Dropdown screenDropdown;
    public Dropdown speakerDropdown;

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

    private AudioConfiguration speakerConfig;
    private int speakerKey;
    private int speakerTemp;
    private int speakerInit;
    public AudioSource[] resumeAudio; // Place any objects with audio sources (within the scene) into this array

    public GameObject Level1Highlight;
    public GameObject Level2Highlight;
    public GameObject Level3Highlight;
    public Text levelText;
    private string levelSelect;

    IEnumerator OptionsCoRoutine()
    {
        HideMenuButtons();
        yield return new WaitForSeconds(cameraPanSpeed / 100);        
        ShowOptionsButtons();
    }

    IEnumerator MenuCoRoutine()
    {
        HideOptionsButtons();
        yield return new WaitForSeconds(cameraPanSpeed / 100);
        ShowMenuButtons();
    }

    IEnumerator PlayCoRoutine()
    {
        HideMenuButtons();
        yield return new WaitForSeconds(cameraPanSpeed / 100);
        ShowPlayButtons();
    }

    IEnumerator CancelCoRoutine()
    {
        HidePlayButtons();
        yield return new WaitForSeconds(cameraPanSpeed / 100);
        ShowMenuButtons();
    }

    IEnumerator LoadScreen()
    {
        loadingScreenUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync(levelSelect);
        while (!async.isDone)
        {
            yield return null;
        } 
    }

    void Awake()
    {
        InitialiseSettings();

        // Assign listeners for drop down UI menus
        screenDropdown.onValueChanged.AddListener(delegate { ScreenResListener(screenDropdown); });
        speakerDropdown.onValueChanged.AddListener(delegate { SpeakerConfigListener(speakerDropdown); });
        LoadSettings();
        levelSelect = "City Outskirts";
    }

    void FixedUpdate()
    {        
        // Function Calls
        UpdateUIvalues();
        MenuTransitioning();
    }

    #region UI Buttons

    // Level 1 Select
    public void LevelOneButton()
    {
        Level1Highlight.SetActive(true);
        Level2Highlight.SetActive(false);
        Level3Highlight.SetActive(false);
        levelSelect = "City Outskirts";
        levelText.text = levelSelect;
    }

    // Level 2 Select
    public void LevelTwoButton()
    {
        Level1Highlight.SetActive(false);
        Level2Highlight.SetActive(true);
        Level3Highlight.SetActive(false);
        levelSelect = "City";
        levelText.text = levelSelect;
    }

    // Level 1 Select
    public void LevelThreeButton()
    {
        Level1Highlight.SetActive(false);
        Level2Highlight.SetActive(false);
        Level3Highlight.SetActive(true);
        levelSelect = "Coast Ending";
        levelText.text = levelSelect;
    }

    // Play Main Menu Button
    public void PlayMainButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.PLAY;
        StartCoroutine("PlayCoRoutine");        
    }

    // Play Button
    public void PlayButton()
    {
        Cursor.visible = false;
        StartCoroutine("LoadScreen");
        screenDropdown.onValueChanged.RemoveAllListeners();
        speakerDropdown.onValueChanged.RemoveAllListeners();
    }

    // Credits Button
    public void CreditsButton()
    {
        screenDropdown.onValueChanged.RemoveAllListeners();
        speakerDropdown.onValueChanged.RemoveAllListeners();
        Application.LoadLevel("Credits");
    }

    // Options Button
    public void OptionsButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.OPTIONS;

        // Set initial values
        volSlider.value = EventManager.inst.masterVolume;
        sensSlider.value = EventManager.inst.mouseSensitivty;
        invertY = EventManager.inst.invertY;

        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            fullscreenToggle.isOn = false;
        }
        else fullscreenToggle.isOn = true;

        speakerDropdown.value = PlayerPrefs.GetInt("Speaker Config");
        screenDropdown.value = PlayerPrefs.GetInt("Screen Res");
        StartCoroutine("OptionsCoRoutine");
    }

    // Apply Button
    public void ApplyButton()
    {
        ApplySettings();      
    }

    // Accept Button
    public void AcceptButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.MAIN;
        ApplySettings();
        StartCoroutine("MenuCoRoutine");
    }

    // Cancel Options Button
    public void CancelOptionsButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.MAIN;        
        StartCoroutine("MenuCoRoutine");
    }

    // Cancel Play Button
    public void CancelPlayButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.CANCEL;
        StartCoroutine("CancelCoRoutine");
    }

    // Exit Button
    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion

    void MenuTransitioning()
    {
        // Increment camera movement between target positions
        cameraPanIncrement += Time.deltaTime * cameraPanSpeed;

        // Move the camera between target locations
        switch (menuToggle)
        {
            case MenuToggle.MAIN:
                currentCamPos.position = Vector3.MoveTowards(optionMenuPos.position, mainMenuPos.position, cameraPanIncrement);
                break;

            case MenuToggle.OPTIONS:
                currentCamPos.position = Vector3.MoveTowards(mainMenuPos.position, optionMenuPos.position, cameraPanIncrement);
                break;

            case MenuToggle.PLAY:
                currentCamPos.position = Vector3.MoveTowards(mainMenuPos.position, playPos.position, cameraPanIncrement);
                break;

            case MenuToggle.CANCEL:
                currentCamPos.position = Vector3.MoveTowards(playPos.position, mainMenuPos.position, cameraPanIncrement);
                break;
        } 
    }

    void LoadSettings()
    {
        // Load saved settings
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

    void ApplySettings()
    {
        // Assign global variables
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.mouseSensitivty = mouseSensitivity;
        EventManager.inst.invertY = invertY;

        // Save values to player prefs
        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
        PlayerPrefs.SetFloat("Mouse Sensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("Screen Res", screenDropdown.value);
        PlayerPrefs.SetInt("Speaker Config", speakerDropdown.value);

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

    void InitialiseSettings()
    {
        // Populate screen X resolutions list
        screenResXlist.Add(1024);
        screenResXlist.Add(1280);
        screenResXlist.Add(1280);
        screenResXlist.Add(1280);
        screenResXlist.Add(1280);
        screenResXlist.Add(1360);
        screenResXlist.Add(1366);
        screenResXlist.Add(1680);
        screenResXlist.Add(1920);
        screenResXlist.Add(2175);
        //screenResXlist.Add(PlayerPrefs.GetInt("Default X"));

        // Populate screen Y resolutions list
        screenResYlist.Add(768);
        screenResYlist.Add(720);
        screenResYlist.Add(768);
        screenResYlist.Add(800);
        screenResYlist.Add(1024);
        screenResYlist.Add(768);
        screenResYlist.Add(768);
        screenResYlist.Add(1050);
        screenResYlist.Add(1080);
        screenResYlist.Add(1527);
        //screenResYlist.Add(PlayerPrefs.GetInt("Default Y"));

        // Populate speaker config modes
        speakerConfigList.Add(AudioSpeakerMode.Stereo);
        speakerConfigList.Add(AudioSpeakerMode.Stereo);
        speakerConfigList.Add(AudioSpeakerMode.Mode5point1);
        speakerConfigList.Add(AudioSpeakerMode.Mode7point1);

        //screenDrop.options.Add(new Dropdown.OptionData((PlayerPrefs.GetInt("Default X") +  (PlayerPrefs.GetInt("Default Y")"));

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

        menuToggle = MenuToggle.NONE;
        Cursor.visible = true;
        screenDropdown.value = screenResKey;
        speakerDropdown.value = speakerKey;
        levelSelect = "Coast Ending";
        levelText.text = levelSelect;
    }

    void UpdateUIvalues()
    {
        volumeLevel = volSlider.value;
        mouseSensitivity = sensSlider.value;
        invertY = invertToggle;

        // Full screen value
        if (fullscreenToggle.isOn)
        {
            fullScreenTemp = true;
        }
        else fullScreenTemp = false;

        // Invert Y value
        if (invertToggle.isOn)
        {
            invertYTemp = true;
        }
        else invertYTemp = false;
    }

    void ScreenResListener(Dropdown val)
    {
        screenResKey = val.value;
        screenResXtemp = screenResXlist[val.value];
        screenResYtemp = screenResYlist[val.value];
    }

    void SpeakerConfigListener(Dropdown val)
    {
        speakerTemp = val.value;
        speakerConfig.speakerMode = speakerConfigList[val.value];
    }

    // Functions to show/hude UI groups
    #region simple UI functions
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

    void ShowPlayButtons()
    {
        for (int i = 0; i < playMenuUI.Length; i++)
        {
            playMenuUI[i].SetActive(true);
        }
    }

    void HidePlayButtons()
    {
        for (int i = 0; i < playMenuUI.Length; i++)
        {
            playMenuUI[i].SetActive(false);
        }
    }
    #endregion
}
