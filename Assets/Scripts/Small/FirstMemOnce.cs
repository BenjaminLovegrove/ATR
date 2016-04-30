using UnityEngine;
using System.Collections;

public class FirstMemOnce : MonoBehaviour
{
    public Collider thisCollider;

    public IEnumerator FirstPlayDelay()
    {
        yield return new WaitForSeconds(3f);
        EventManager.inst.firstPlay = false;
    }

	void Awake () {
	    if (EventManager.inst.firstPlay)
        {
            thisCollider.enabled = true;
            StartCoroutine("FirstPlayDelay");
        }
	}
	
}
