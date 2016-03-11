using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
    public float destroyTimer;

	void FixedUpdate ()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
