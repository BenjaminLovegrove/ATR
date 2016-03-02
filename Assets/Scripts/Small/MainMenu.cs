using UnityEngine;
using System.Collections;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public GameObject[] loadingScreenImages;

    IEnumerator LoadScene()
    {
        //yield return new WaitForSeconds(0.1f);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync("City");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
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
