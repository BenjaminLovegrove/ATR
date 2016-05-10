using UnityEngine;
using System.Collections;

// Splash screen automatic light and transition script

public class IntroScreen : MonoBehaviour
{
    private float timer;
    public float duration;

    public GameObject light;
    public GameObject light2;

	void FixedUpdate ()
    {
        timer += Time.deltaTime;

        if (timer > (duration - 1.5f))
        {
            light2.SetActive(true);
        }

        if (timer > duration)
        {
            Application.LoadLevel("MainMenu");
        }

        light.transform.Rotate(Vector3.down * Time.deltaTime * 9);

	}
 }
