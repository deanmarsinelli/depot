using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

	// Use this for initialization
	void Start () 
    {
        offset = transform.position;
	}
	
	// LateUpdate used for follow cameras
	void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
	}
}
