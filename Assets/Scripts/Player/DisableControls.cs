using UnityEngine;
using System.Collections;

public class DisableControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
}
