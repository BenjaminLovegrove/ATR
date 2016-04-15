using UnityEngine;
using System.Collections;

public class LedgeFix : MonoBehaviour {

    public GameObject ledge;
	
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            ledge.SetActive(false);
        }
	}
}
