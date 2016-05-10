using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

// Script for the UI buttons and options/settings used in the main menu

public class MainMenu : MonoBehaviour
{
    public enum MenuToggle
    {
        MAIN, OPTIONS, PLAY, CANCEL, NONE
    }

    Resolution[] resolutions;

    private AudioListener AL;
    public MenuToggle menuToggle;

    private List<AudioSpeakerMode> speakerConfigList = new List<AudioSpeakerMode>();

    public GameObject loadingScreenUI;
    public GameObject[] mainMenuUI;
    public GameObject[] optionMenuUI;
    public GameObject[] playMenuUI;
    public GameObject confirmQuitUI;
    private AudioSource audio;
    public AudioClip playSFX;

    public Transform currentCamPos;
    public Transform mainMenuPos;
    public Transform optionMenuPos;
    public Transform playPos;
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
    private bool fullScreenVal;

    private float volumeLevel;
    private float lookSensitivity;
    
    private bool invertY;
    private bool invertYTemp;
    private int invertYKey;

    private AudioConfiguration speakerConfig;
    public AudioSource[] resumeAudio; // Place any objects with audio sources into this array
    public AudioSource menuMusic;
    private float menuMusicMax;
    private bool pressedPlay;
    private float musicLerp;
    private int speakerKey;
    private int speakerTemp;
    private int speakerInit;    

    public GameObject Level1Highlight;
    public GameObject Level2Highlight;
    public GameObject Level3Highlight;
    public Text levelText;
    private string levelSelect;

    void Awake()
    {
        InitialiseSettings();
        LoadSettings();
    }

    void Update()
    {
        UpdateUIvalues();
    }

    void FixedUpdate()
    {
        MenuTransitioning();
    }

    void MenuTransitioning()
    {
        if (pressedPlay)
        {
            musicLerp += Time.deltaTime;
            menuMusic.volume = Mathf.Lerp(menuMusicMax, 0f, musicLerp);
        }

        // Increment camera transition value
        cameraPanIncrement += 0.020f;

        // Move the camera between target locations
        switch (menuToggle)
        {
            case MenuToggle.MAIN:
                currentCamPos.position = Vector3.Lerp(optionMenuPos.position, mainMenuPos.position, cameraPanIncrement);
                break;

            case MenuToggle.OPTIONS:
                currentCamPos.position = Vector3.Lerp(mainMenuPos.position, optionMenuPos.position, cameraPanIncrement);
                break;

            case MenuToggle.PLAY:
                currentCamPos.position = Vector3.Lerp(mainMenuPos.position, playPos.position, cameraPanIncrement);
                break;

            case MenuToggle.CANCEL:
                currentCamPos.position = Vector3.Lerp(playPos.position, mainMenuPos.position, cameraPanIncrement);
                break;
        } 
    }

    void LoadSettings()
    {
        // Set default values if there are no settings yet
        if (!PlayerPrefs.HasKey("Speaker Config"))
        {
            speakerInit = 0;
        }

        if (!PlayerPrefs.HasKey("Mouse Sensitivity"))
        {
            EventManager.inst.lookSensitivity = 5;
            PlayerPrefs.SetFloat("Mouse Sensitivity", 5);
            lookSensitivity = 5;
        }

        if (!PlayerPrefs.HasKey("Master Volume"))
        {
            EventManager.inst.masterVolume = 0.8f;
            PlayerPrefs.SetFloat("Master Volume", 0.8f);
            volumeLevel = 0.8f;
        }

        if (!PlayerPrefs.HasKey("Fullscreen"))
        {
            fullscreenToggle.isOn = true;
        }

        // Load saved settings
        speakerConfig = AudioSettings.GetConfiguration();
        screenResKey = PlayerPrefs.GetInt("Screen Res");
        speakerKey = PlayerPrefs.GetInt("Speaker Config");
        fullScreenKey = PlayerPrefs.GetInt("Fullscreen");
        invertYKey = PlayerPrefs.GetInt("Invert Toggle");
        volSlider.value = PlayerPrefs.GetFloat("Master Volume");
        sensSlider.value = PlayerPrefs.GetInt("Look Sensitivity");

        Invoke("FullScreenCheck", 0.01f);
    }

    void ApplySettings()
    {
        EventManager.inst.invertY = invertY;

        // Save values to player prefs
        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
        PlayerPrefs.SetFloat("Mouse Sensitivity", lookSensitivity);
        PlayerPrefs.SetInt("Screen Res", screenDropdown.value);
        PlayerPrefs.SetInt("Speaker Config", speakerDropdown.value);
        PlayerPrefs.SetFloat("Master Volume", volSlider.value);
        PlayerPrefs.SetInt("Look Sensitivity", Mathf.FloorToInt(sensSlider.value));

        // Switch between windowed and full screen mode
        if (!fullscreenToggle.isOn)
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else PlayerPrefs.SetInt("Fullscreen", 1);

        // Apply invert Y toggle
        if (!invertToggle.isOn)
        {
            EventManager.inst.invertY = false;
            PlayerPrefs.SetInt("Invert Toggle", 0);
        }

        // Remove invert Y toggle
        if (invertToggle.isOn)
        {
            EventManager.inst.invertY = true;
            PlayerPrefs.SetInt("Invert Toggle", 1);
        }
        
        // Adjust screen res if there are changes
        if (screenResX != screenResXtemp || screenResY != screenResYtemp || fullScreen != fullScreenTemp)
        {
            Screen.SetResolution(screenResXtemp, screenResYtemp, fullScreenTemp);
            Cursor.lockState = CursorLockMode.None;
            PlayerPrefs.SetInt("ScreenResX", resolutions[screenDropdown.value].width);
            PlayerPrefs.SetInt("ScreenResY", resolutions[screenDropdown.value].height);
            Invoke("CursorConfine", 0.05f);
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
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            screenDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            screenDropdown.value = i;
        }

        audio = GetComponent<AudioSource>();
        GameObject.Find("EventManager").GetComponent<AudioSource>().Stop();

        EventManager.inst.firstEncounterPlaying = false;
        EventManager.inst.firstPlay = true;
        EventManager.inst.currentMemory = 1;
        EventManager.inst.currentLevel = "MainMenu";
        EventManager.inst.credits = false;
        EventManager.inst.atEndTerrain = false;
        EventManager.inst.memoryPlaying = false;
        EventManager.inst.currentCheckPoint = 0;
        EventManager.inst.memoryLookScalar = 1;
        EventManager.inst.memoryMoveScalar = 1;

        levelSelect = "City Outskirts";

        // Assign listeners for drop down UI menus
        screenDropdown.onValueChanged.AddListener(delegate { ScreenResListener(screenDropdown); });
        speakerDropdown.onValueChanged.AddListener(delegate { SpeakerConfigListener(speakerDropdown); });

        // Populate speaker config modes
        speakerConfigList.Add(AudioSpeakerMode.Stereo);
        speakerConfigList.Add(AudioSpeakerMode.Stereo);
        speakerConfigList.Add(AudioSpeakerMode.Mode5point1);
        speakerConfigList.Add(AudioSpeakerMode.Mode7point1);

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
        Cursor.visible = false;
        screenDropdown.value = screenResKey;
        speakerDropdown.value = speakerKey;
        levelSelect = "City Outskirts";
        levelText.text = levelSelect;
        Cursor.visible = true;
        menuMusicMax = menuMusic.volume; 
    }

    void UpdateUIvalues()
    {
        volumeLevel = volSlider.value;
        lookSensitivity = sensSlider.value;
        invertY = invertToggle;

        //Real time update sens and volume
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.lookSensitivity = lookSensitivity;

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

    #region UI Buttons
    // Level 1 Select
    public void LevelOneButton()
    {
        Level1Highlight.SetActive(true);
        Level2Highlight.SetActive(false);
        Level3Highlight.SetActive(false);
        levelSelect = "City Outskirts";
        levelText.text = levelSelect;
        EventManager.inst.currentMemory = 1;
    }

    // Level 2 Select
    public void LevelTwoButton()
    {
        Level1Highlight.SetActive(false);
        Level2Highlight.SetActive(true);
        Level3Highlight.SetActive(false);
        levelSelect = "City";
        levelText.text = levelSelect;
        EventManager.inst.currentMemory = 6;
    }

    // Level 3 Select
    public void LevelThreeButton()
    {
        Level1Highlight.SetActive(false);
        Level2Highlight.SetActive(false);
        Level3Highlight.SetActive(true);
        levelSelect = "Coast";
        levelText.text = levelSelect;
        EventManager.inst.currentMemory = 8;
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
        pressedPlay = true;
        audio.PlayOneShot(playSFX, 1f);
        EventManager.inst.currentLevel = levelSelect;
        EventManager.inst.currentCheckPoint = 0;
        EventManager.inst.firstPlay = true;

        StartCoroutine("LoadScreen");
    }

    // Credits Button
    public void CreditsButton()
    {
        Application.LoadLevel("Credits");
    }

    // Options Button
    public void OptionsButton()
    {
        cameraPanIncrement = 0;
        menuToggle = MenuToggle.OPTIONS;

        // Set initial values
        volSlider.value = PlayerPrefs.GetFloat("Master Volume");
        sensSlider.value = PlayerPrefs.GetInt("Look Sensitivity");
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
        PlayerPrefs.SetFloat("Master Volume", volSlider.value);
        PlayerPrefs.SetInt("Look Sensitivity", Mathf.FloorToInt(sensSlider.value));
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
        HideMenuButtons();
        confirmQuitUI.SetActive(true);
    }

    // Confirm Quit Yes
    public void QuitYes()
    {
        Application.Quit();
    }

    // Confirm Quit No
    public void QuitNo()
    {
        confirmQuitUI.SetActive(false);
        ShowMenuButtons();
    }
    #endregion

    #region simple functions
    void FullScreenCheck()
    {
        if (!Screen.fullScreen)
        {
            if (PlayerPrefs.GetInt("Fullscreen") == 1)
            {
                Screen.SetResolution(Screen.width, Screen.height, true);
            }
            else Screen.SetResolution(Screen.width, Screen.height, false);
        }
    }

    IEnumerator LoadScreen()
    {
        EventManager.inst.firstPlay = true;
        EventManager.inst.firstEncounter = true;
        EventManager.inst.memoryPlaying = false;
        loadingScreenUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync(levelSelect);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    IEnumerator OptionsCoRoutine()
    {
        HideMenuButtons();
        yield return new WaitForSeconds(1.1f);
        ShowOptionsButtons();
    }

    IEnumerator MenuCoRoutine()
    {
        HideOptionsButtons();
        yield return new WaitForSeconds(1.1f);
        ShowMenuButtons();
    }

    IEnumerator PlayCoRoutine()
    {
        HideMenuButtons();
        yield return new WaitForSeconds(1.1f);
        ShowPlayButtons();
    }

    IEnumerator CancelCoRoutine()
    {
        HidePlayButtons();
        yield return new WaitForSeconds(1.1f);
        ShowMenuButtons();
    }

    void ScreenResListener(Dropdown val)
    {
        screenResKey = val.value;
        screenResXtemp = resolutions[screenDropdown.value].width;
        screenResYtemp = resolutions[screenDropdown.value].height;
    }

    void SpeakerConfigListener(Dropdown val)
    {
        speakerTemp = val.value;
        speakerConfig.speakerMode = speakerConfigList[val.value];
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

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

    void OnDestroy()
    {
        screenDropdown.onValueChanged.RemoveAllListeners();
        speakerDropdown.onValueChanged.RemoveAllListeners();
    }
    #endregion
}
