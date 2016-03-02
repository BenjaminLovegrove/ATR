using UnityEngine;
using System.Collections;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public GameObject[] loadingScreenImages;

    IEnumerator LoadScene()
    {
        
        AsyncOperation async = Application.LoadLevelAsync("City");

        while (!async.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void PlayButton()
    {
        print("Pressed Play");
        Cursor.visible = false;
        for (int i = 0; i < loadingScreenImages.Length; i++)
        {
            loadingScreenImages[i].SetActive(true);
        }
        StartCoroutine("LoadScene");
    }

    public void CreditsButton()
    {
        print("Pressed Credits");
        Application.LoadLevel("Credits");
    }

    public void ExitButton()
    {
        print("Pressed Exit");
        Application.Quit();
    }

    void FixedUpdate()
    {
        // Dodgey
        Cursor.visible = true;
    }
}
