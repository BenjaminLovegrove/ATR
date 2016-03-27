using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    public GameObject confirmExitUI;
    public GameObject confirmMainMenuUI;

    public RawImage backBackgroundTint;

    #region Main Menu UI Buttons
    // Resume
    public void ResumeButton()
    {
        backBackgroundTint.CrossFadeAlpha(0, 2, false);
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        EventManager.inst.gamePaused = false;
    }

    // Options
    public void OptionsButton()
    {
        pauseMenuUI.SetActive(false);
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

    #region Options Buttons
    // Apply Options
    public void OptionsApplyButton()
    {

    }

    // Accept Options
    public void OptionsAcceptButton()
    {
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

    void Update()
    {
        PauseMenuCheck();
    }

    // In game pause function
    void PauseMenuCheck()
    {
        if (Input.GetKey(KeyCode.Escape) && !EventManager.inst.gamePaused && !EventManager.inst.memoryPlaying)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            EventManager.inst.gamePaused = true;
            pauseMenuUI.SetActive(true);
            backBackgroundTint.CrossFadeAlpha(255, 2, false);
        }
    }
}
