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


    void OnTriggerEnter (Collider col) {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
            if (Memory)
            {
                col.BroadcastMessage("EnterMemory");
                triggered = true;
            }
        }
	}
}
