using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
    public float speed;

    // executed on the first frame the object is instantiated
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 1.0f) * speed; // +z axis
    }
}
