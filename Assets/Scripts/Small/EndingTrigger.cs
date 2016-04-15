using UnityEngine;
using System.Collections;

public class EndingTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider col) {

        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.atEndTerrain = true;
        }
	}

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.atEndTerrain = false;
        }
    }
}
