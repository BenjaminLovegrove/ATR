using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Script for the UI buttons used in the main menu

public class MainMenu : MonoBehaviour
{
    public GameObject[] loadingScreenUI;
    public GameObject[] mainMenuUI;
    public GameObject[] optionMenuUI;

    public Slider volSlider;
    public Slider sensSlider;
    public Toggle invertToggle;
    public Toggle fullscreenToggle;

    public float volumeLevel;
    public float mouseSensitivity;
    public bool invertY;

    // Load game async coroutine
    IEnumerator LoadScene()
    {        
        AsyncOperation async = Application.LoadLevelAsync("City");

        while (!async.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
    }
    
    // Play Button
    public void PlayButton()
    {
        print("Pressed Play");
        Cursor.visible = false;
        for (int i = 0; i < loadingScreenUI.Length; i++)
        {
            loadingScreenUI[i].SetActive(true);
        }
        StartCoroutine("LoadScene");
    }

    // Credits Button
    public void CreditsButton()
    {
        print("Pressed Credits");
        Application.LoadLevel("Credits");
    }

    // Options Button
    public void OptionsButton()
    {
        print("Pressed Options");
        // Set initial values
        volSlider.value = EventManager.inst.masterVolume;
        sensSlider.value = EventManager.inst.mouseSensitivty;
        invertY = EventManager.inst.invertY;
        HideMenuButtons();
        ShowOptionsButtons();
    }

    // Apply Button
    public void ApplyButton()
    {
        print("Pressed Apply");
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.mouseSensitivty = mouseSensitivity;
        EventManager.inst.invertY = invertY;
    }

    // Accept Button
    public void AcceptButton()
    {
        print("Pressed Accept");
        EventManager.inst.masterVolume = volumeLevel;
        EventManager.inst.mouseSensitivty = mouseSensitivity;
        EventManager.inst.invertY = invertY;
        ShowMenuButtons();
        HideOptionsButtons();
    }

    // Cancel Button
    public void CancelButton()
    {
        print("Pressed Cancel");
        ShowMenuButtons();
        HideOptionsButtons();
    }

    // Exit Button
    public void ExitButton()
    {
        print("Pressed Exit");
        Application.Quit();
    }

    void FixedUpdate()
    {
        //AudioSettings.Reset(AudioSpeakerMode.Mono);
        volumeLevel = volSlider.value;
        mouseSensitivity = sensSlider.value;
        invertY = invertToggle;

        // Dodgey
        Cursor.visible = true;
    }

    #region simple functions
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
    #endregion
}
