using UnityEngine;
using System.Collections;

public class PlayerTeleport : MonoBehaviour {

    public Transform teleportLoc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = teleportLoc.position;
        }
	}
}
