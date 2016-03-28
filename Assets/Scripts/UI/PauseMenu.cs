using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    private List<int> screenResXlist = new List<int>();
    private List<int> screenResYlist = new List<int>();

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject confirmExitUI;
    public GameObject confirmMainMenuUI;

    public Slider volSlider;
    public Slider sensSlider;

    public Toggle invertToggle;
    public Toggle fullscreenToggle;

    public Dropdown screenDropdown;
    public RawImage pauseBackGround;

    private int screenResY;
    private int screenResX;
    private int screenResKey;
    private int screenResXtemp;
    private int screenResYtemp;

    private bool fullScreen;
    private bool fullScreenTemp;
    private int fullScreenKey;

    private float volumeLevel;
    private float lookSensitivity;

    private bool invertY;
    private bool invertYTemp;
    private int invertYKey;

    #region Main Menu
    // Resume
    public void ResumeButton()
    {
        pauseBackGround.CrossFadeAlpha(0, 0.3f, false);
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        EventManager.inst.gamePaused = false;
    }

    // Options
    public void OptionsButton()
    {
        pauseMenuUI.SetActive(false);
        
        // Set initial values
        volSlider.value = PlayerPrefs.GetFloat("Master Volume");
        sensSlider.value = PlayerPrefs.GetInt("Look Sensitivity");
        invertY = EventManager.inst.invertY;

        if (PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            fullscreenToggle.isOn = false;
        }
        else fullscreenToggle.isOn = true;

        screenDropdown.value = PlayerPrefs.GetInt("Screen Res");
        optionsMenuUI.SetActive(true);
    }

    // Main Menu
    public void MainMenuButton()
    {
        pauseMenuUI.SetActive(false);
        confirmMainMenuUI.SetActive(true);
    }

    // Exit
    public void ExitButton()
    {
        pauseMenuUI.SetActive(false);
        confirmExitUI.SetActive(true);
    }
    #endregion

    #region Confirm Exit
    // Confirm Exit Yes
    public void ExitYesButton()
    {
        Application.Quit();
    }
    
    // Confirm Exit No
    public void ExitNoButton()
    {
        confirmExitUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    #endregion

    #region Confirm Main Menu
    // Confirm Main Menu Yes
    public void MainMenuYesButton()
    {
        Time.timeScale = 1;
        EventManager.inst.gamePaused = false;
        Application.LoadLevel("MainMenu");
    }

    // Confirm Main Menu No
    public void MainMenuNoButton()
    {
        confirmMainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    #endregion

    #region Options
    // Apply Options
    public void OptionsApplyButton()
    {
        ApplySettings();
    }

    // Accept Options
    public void OptionsAcceptButton()
    {
        ApplySettings();
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    // Cancel Options
    public void OptionsCancelButton()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    #endregion

    void Awake()
    {
        pauseBackGround.CrossFadeAlpha(0, 0, false);
        InitialiseSettings();
        LoadSettings();
    }

    void Update()
    {
        PauseMenuCheck();
        UpdateUIvalues();
    }

    void LoadSettings()
    {
        // Load saved settings
        screenResKey = PlayerPrefs.GetInt("Screen Res");
        fullScreenKey = PlayerPrefs.GetInt("Fullscreen");
        invertYKey = PlayerPrefs.GetInt("Invert Toggle");
        volSlider.value = PlayerPrefs.GetFloat("Master Volume");
        sensSlider.value = PlayerPrefs.GetInt("Look Sensitivity");

        // Set default values if there are no settings yet
        if (!PlayerPrefs.HasKey("Mouse Sensitivity"))
        {
            EventManager.inst.lookSensitivity = 5;
            PlayerPrefs.SetFloat("Mouse Sensitivity", 5);
            lookSensitivity = 5;
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
        EventManager.inst.invertY = invertY;

        // Save values to player prefs
        PlayerPrefs.SetFloat("Master Volume", volumeLevel);
        PlayerPrefs.SetFloat("Mouse Sensitivity", lookSensitivity);
        PlayerPrefs.SetInt("Screen Res", screenDropdown.value);
        PlayerPrefs.SetFloat("Master Volume", volSlider.value);
        PlayerPrefs.SetInt("Look Sensitivity", Mathf.FloorToInt(sensSlider.value));

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
            Cursor.lockState = CursorLockMode.None;
            Invoke("CursorConfine", 0.05f);
        }
    }

    void InitialiseSettings()
    {
        // Assign listeners for drop down UI menus
        screenDropdown.onValueChanged.AddListener(delegate { ScreenResListener(screenDropdown); });

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

        Cursor.visible = true;
        screenDropdown.value = screenResKey;
    }

    void UpdateUIvalues()
    {
        volumeLevel = volSlider.value;
        lookSensitivity = sensSlider.value;
        invertY = invertToggle;

        //Real time update sens and volume
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.lookSensitivity = sensSlider.value;

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

    void CursorConfine()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Check for Escape key press
    void PauseMenuCheck()
    {
        if (Input.GetKey(KeyCode.Escape) && !EventManager.inst.gamePaused && !EventManager.inst.memoryPlaying)
        {
            if (!EventManager.inst.gamePaused && !EventManager.inst.memoryPlaying)
            {
                pauseBackGround.CrossFadeAlpha(100, 1, false);
                Cursor.visible = true;
                Time.timeScale = 0;
                EventManager.inst.gamePaused = true;
                pauseMenuUI.SetActive(true);
            }
        }
    }

    void OnDestroy()
    {
        // Remove listeners
        screenDropdown.onValueChanged.RemoveAllListeners();
    }
}
