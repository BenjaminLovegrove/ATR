using UnityEngine;
using System.Collections;

// Add this script to a light to create a flickering effect
// Note - Attach the script to the parent of the gameobject and note the light object itself
// Use spotlight for the best effect (refer to Main Menu scene)

public class FlickeringLight : MonoBehaviour
{
    public GameObject light;
    private float timer;
    private float lightDuration = 1f;
    private bool flicker = false;


	void FixedUpdate ()
    {
        timer += Time.deltaTime;

        if (timer > lightDuration && flicker == false)
        {
            flicker = true;
            light.SetActive(false);
            timer = 0;
            lightDuration = 0.1f;
        }

        if (timer > lightDuration && flicker == true)
        {
            flicker = false;
            light.SetActive(true);
            timer = 0;
            lightDuration = RandNumber();
        }
	}

    public float RandNumber()
    {
        float i = Random.Range(0.1f, 3f);
        return i;
    }
}
