using UnityEngine;
using System.Collections;

// Splash screen automatic light and transition script

public class IntroScreen : MonoBehaviour
{
    private float timer;
    public float duration;

    public GameObject light;

	void FixedUpdate ()
    {
        timer += Time.deltaTime;

        if (timer > duration)
        {
            Application.LoadLevel("MainMenu");
        }

        light.transform.Rotate(Vector3.down * Time.deltaTime * 9);

	}
 }
