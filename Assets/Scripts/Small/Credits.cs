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

    void Start()
    {
        Cursor.visible = false;
    }

    void FixedUpdate()
    {        
        creditsText.transform.position += Vector3.up * Time.deltaTime * textScrollSpeed;
        timer += Time.deltaTime;

        if (timer > autoExit)
        {
            Application.LoadLevel("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
