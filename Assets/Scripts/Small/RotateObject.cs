using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    public GameObject rotatingObj;
    public int rotationSpeed;
	
	void FixedUpdate ()
    {
        rotatingObj.transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
	}
}
