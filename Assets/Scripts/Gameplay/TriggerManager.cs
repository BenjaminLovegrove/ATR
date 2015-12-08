using UnityEngine;
using System.Collections;

public class TriggerManager : MonoBehaviour {

    [Header("Trigger Settings")]
    bool triggered = false;

    [Header("Memory Trigger")]
    public bool Memory;
    public AudioClip memoryDialogue;

    [Header("Enemy Patrol Start Trigger")]
    public GameObject enemy;

	[Header("Checkpoint")]
	public bool checkpoint;


    void OnTriggerEnter (Collider col) {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
			triggered = true;

            if (Memory)
            {
                col.BroadcastMessage("EnterMemory");
            }

			if (checkpoint){
				EventManager.inst.currentCheckPoint ++;
			}
        }
	}
}
