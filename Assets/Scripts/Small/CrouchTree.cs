using UnityEngine;
using System.Collections;

public class CrouchTree : MonoBehaviour {

    private CapsuleCollider col;
    private Vector3 startVec;

    void Start()
    {
        col = gameObject.GetComponent<CapsuleCollider>();
        startVec = col.center;
    }

	void Update () {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            col.center = new Vector3(startVec.x + 1f, startVec.y, startVec.z);
        } else
        {
            col.center = startVec;
        }
    }
}
