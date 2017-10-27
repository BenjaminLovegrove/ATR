using UnityEngine;
using System.Collections;

public class DirtyDoubleLoadFix : MonoBehaviour {

    public GameObject loadScreenUI;

	void Start () {
        //PlayerPrefs.DeleteAll();
	    if (EventManager.inst.firstLoad)
        {
            EventManager.inst.firstLoad = false;
            print("Scene was loaded twice");
            EventManager.inst.keepAudioOff = true;
            AudioListener.volume = 0;
            StartCoroutine("LoadNextScene");
            PlayerPrefs.SetInt("FirstLoad", 1);
        } else
        {
            EventManager.inst.keepAudioOff = false;
        }
	}

    IEnumerator LoadNextScene()
    {
        loadScreenUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        AsyncOperation async = Application.LoadLevelAsync(Application.loadedLevel);
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
