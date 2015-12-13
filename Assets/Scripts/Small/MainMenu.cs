using UnityEngine;
using System.Collections;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        print("Pressed Play");
        Application.LoadLevel("City");
        Cursor.visible = false;
    }

    public void CreditsButton()
    {
        print("Pressed Credits");
    }

    public void ExitButton()
    {
        print("Pressed Exit");
        Application.Quit();
    }

    void FixedUpdate()
    {
        Cursor.visible = true;
    }
}
