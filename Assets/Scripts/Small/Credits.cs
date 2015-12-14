using UnityEngine;
using System.Collections;

// Script for the UI used in credits scene

public class Credits : MonoBehaviour
{
    public GameObject creditsText;
    private float timer;
    public float autoExit;
    public float textScrollSpeed;

    public void MainMenuButton()
    {
        print("Main Menu");
        Application.LoadLevel("MainMenu");
    }

    void FixedUpdate()
    {
        Cursor.visible = true;
        creditsText.transform.position += Vector3.up * Time.deltaTime * textScrollSpeed;
        timer += Time.deltaTime;

        if (timer > autoExit)
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
