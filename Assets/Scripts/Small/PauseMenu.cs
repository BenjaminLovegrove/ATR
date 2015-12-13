using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{   
    public void ResumeClicked()
    {
        print("Game resumed");
        Cursor.visible = false;
        Time.timeScale = 1;
        EventManager.inst.gamePaused = false;
        EventManager.inst.pauseMenuButtons.SetActive(false);
    }

    public void MainMenuClicked()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void ExitClicked()
    {
        Application.Quit();
    }
}
