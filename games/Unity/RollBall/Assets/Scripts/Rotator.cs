using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
    Vector3 rotation = new Vector3(15, 30, 45);

	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(rotation * Time.deltaTime);
	}
}
