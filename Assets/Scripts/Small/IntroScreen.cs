using UnityEngine;
using System.Collections;

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
            //Application.LoadLevel("Main Menu");
            //print("Intro Complete");
        }

        light.transform.Rotate(Vector3.down * Time.deltaTime * 7);

	}
 }
