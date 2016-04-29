using UnityEngine;
using System.Collections;

public class FirstMemOnce : MonoBehaviour {

    public Collider thisCollider;

	void Awake () {
	    if (EventManager.inst.firstPlay)
        {
            thisCollider.enabled = true;
            EventManager.inst.firstPlay = false;
        }
	}
	
}
