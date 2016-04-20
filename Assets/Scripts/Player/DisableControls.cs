using UnityEngine;
using System.Collections;

public class DisableControls : MonoBehaviour
{
    public float getIntoMem;

    void Update()
    {
        getIntoMem += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this);
        }

        if (!EventManager.inst.memoryPlaying && getIntoMem > 10)
        {
            Destroy(this);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            EventManager.inst.controlsDisabled = true;
        }
    }

    void OnDestroy()
    {
        EventManager.inst.controlsDisabled = false;
    }


}
