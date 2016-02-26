using UnityEngine;
using System.Collections;

public class FadeOutObjMove : MonoBehaviour
{
    public Transform targetTrans;
    float delay = 2f;

    void FixedUpdate ()
    {
        delay -= Time.deltaTime;

        if (delay < 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTrans.position, 1f);
        }
	}
}
