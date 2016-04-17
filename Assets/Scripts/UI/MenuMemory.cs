using UnityEngine;
using System.Collections;

public class MenuMemory : MonoBehaviour
{    
    public float memoryDuration;
    public float normalDuration;

    public GameObject car;
    public GameObject memoryObj;
    public GameObject carSpawn;
    public GameObject[] switchObjects;

    private bool triggerBool;
   
    private float normalTimer;
    private float memoryTimer;

	void Awake ()
    {
        triggerBool = false;
	}	

	void Update ()
    {
        TriggerTimers();
	}
    
    void TriggerTimers()
    {
        if (!triggerBool)
        {
            normalTimer += Time.deltaTime;
        }

        if (triggerBool)
        {
            memoryTimer += Time.deltaTime;
        }

        if (normalTimer > normalDuration && !triggerBool)
        {
            triggerBool = true;
            TriggerMemory();
        }

        if (memoryTimer > memoryDuration)
        {
            triggerBool = false;
            normalTimer = 0;
            memoryTimer = 0;
        }
    }

    void TriggerMemory()
    {
        Instantiate(car, carSpawn.transform.position, Quaternion.Euler(0, -90, 0));
        memoryObj.SendMessage("EnterMemory", memoryDuration);
        memoryObj.SendMessage("SetSwitch", switchObjects);
    }
}
