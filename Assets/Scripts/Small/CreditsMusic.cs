using UnityEngine;
using System.Collections;

public class CreditsMusic : MonoBehaviour {

	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	void OnLevelWasLoaded(int level) {
	    if (level == 0)
        {
            Destroy(this.gameObject);
        }
	}
}
