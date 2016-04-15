using UnityEngine;
using System.Collections;

public class scr_MemLookMovement : MonoBehaviour {

    public Transform startPos;
    public Transform endPos;
    public float lerpTime = 5f;
    public float lerpTimer;
	
    void Start()
    {
        lerpTimer = -1f / lerpTime;
    }

	void Update () {
        lerpTimer += Time.deltaTime / lerpTime;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, lerpTimer);
	}
}
