using UnityEngine;
using System.Collections;

public class BumpObjSFX : MonoBehaviour {

    public AudioClip touchWood;
    public AudioClip touchMetal;
    public AudioClip metalFence;

	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Terrain" || col.gameObject.tag == "Wood")
        {
            if (touchWood != null)
            {
                AudioSource.PlayClipAtPoint(touchWood, transform.position, 0.2f);
            }
        } else if(col.gameObject.tag == "Metal")
        {
            if (touchMetal != null)
            {
                AudioSource.PlayClipAtPoint(touchMetal, transform.position, 0.2f);
            }
        }

    }
}
