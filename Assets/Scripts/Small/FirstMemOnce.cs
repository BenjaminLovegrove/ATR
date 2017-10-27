using UnityEngine;
using System.Collections;

public class FirstMemOnce : MonoBehaviour
{
    public Collider thisCollider;
    public float startDelay = 2f;

    public IEnumerator FirstPlayDelay()
    {
        yield return new WaitForSeconds(3f);
        EventManager.inst.firstPlay = false;
    }

	void Update () {
	    if (EventManager.inst.firstPlay && thisCollider.enabled == false)
        {
            startDelay -= Time.deltaTime;

            if (startDelay <= 0)
            {
                thisCollider.enabled = true;
                StartCoroutine("FirstPlayDelay");
            }
        }

    }

}
