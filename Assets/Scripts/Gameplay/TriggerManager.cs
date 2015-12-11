using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class TriggerManager : MonoBehaviour {

    [Header("Trigger Settings")]
    bool triggered = false;

    [Header("Memory Trigger")]
    public bool Memory;
    public AudioClip memoryDialogue;

    [Header("Fog Change")]
    public bool fogChange;
    public float targFog;
    private GlobalFog fog;
    private float startFog;
    private float fogLerp = 1;

    [Header("Enemy Patrol Start Trigger")]
    public GameObject enemy;

	[Header("Checkpoint")]
	public bool checkpoint;

    void Start()
    {
        fog = GameObject.Find("Player").GetComponent<GlobalFog>();
    }

    void OnTriggerEnter (Collider col) {
	    if (col.gameObject.tag == "Player" && !triggered)
        {
			triggered = true;

            //Memory
            if (Memory)
            {
                col.BroadcastMessage("EnterMemory");
            }

            //Checkpoint
			if (checkpoint){
				EventManager.inst.currentCheckPoint ++;
			}

            //Fog
            if (fogChange)
            {
                startFog = fog.heightDensity;
                fogLerp = 0;
            }
        }
	}

    void Update()
    {
        //Change fog
        if (fogLerp < 1)
        {
            fogLerp += Time.deltaTime / 2;
            fog.heightDensity = Mathf.Lerp(startFog, targFog, fogLerp);
        }

    }
}
