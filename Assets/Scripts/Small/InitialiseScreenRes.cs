using UnityEngine;
using System.Collections;

public class InitialiseScreenRes : MonoBehaviour
{
    private bool fullScreenVal;

    void Awake()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Confined;


        if (PlayerPrefs.GetInt("FullScreen") == 1)
        {
            fullScreenVal = true;
        }
        else fullScreenVal = false;

        if (PlayerPrefs.HasKey("ScreenResX") && PlayerPrefs.HasKey("ScreenResY"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("ScreenResX"), PlayerPrefs.GetInt("ScreenResY"), fullScreenVal);
        }
        else Screen.fullScreen = fullScreenVal;

        Application.LoadLevel("MainMenu");
    }
}
