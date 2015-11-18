using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
	public static EventManager inst;					// Create singleton of this script
	public int playerHp = 100;							// Player Health
	public Vector3 lastPlayerSighting;        		    // Last place an enemy spotted the player
	public Transform playerTrans;						// Player's position
	public GameObject playerObj;						// Reference to the player object
	public bool playerCrouch = false;					// State of player crouching

	void Awake ()
	{
		// Check for duplicate singletons
		if(inst == null)
		{
			inst = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(this);
	}

	void FixedUpdate ()
	{
		//playerTrans = playerObj.transform;
	}

	void ResetData ()
	{
		playerHp = 100;
	}
}
