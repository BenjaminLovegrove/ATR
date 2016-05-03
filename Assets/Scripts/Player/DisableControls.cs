using UnityEngine;
using System.Collections;

public class DisableControls : MonoBehaviour
{
    private float delay;

    void Update()
    {
        delay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape) && delay > 3f)
        {
            Destroy(this);
        }

        if (delay > 31.4f)
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
