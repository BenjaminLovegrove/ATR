using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    public GameObject rotatingObj;
    public int rotationSpeed;

    public bool rotateZ;
    public bool rotateY;
    public bool rotateX;

	
	void FixedUpdate ()
    {
        if (rotateZ)
        {
            rotatingObj.transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
        }

        if (rotateY)
        {
            rotatingObj.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
        }

        if (rotateX)
        {
            rotatingObj.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        
	}
}
