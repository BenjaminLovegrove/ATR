using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{
	public static EventManager inst;
	public int playerHp = 100;
	public Vector3 lastPlayerSighting;
	public Transform playerTrans;
	public GameObject playerObj;
	public bool playerCrouch = false;

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
		//print (playerTrans);
		playerTrans = playerObj.transform;
	}

	void ResetData ()
	{
		playerHp = 100;
	}
}
