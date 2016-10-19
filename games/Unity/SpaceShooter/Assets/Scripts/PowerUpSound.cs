using UnityEngine;
using System.Collections;

public class PowerUpSound : MonoBehaviour 
{
    public GameObject pickupSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(pickupSound);
            Destroy(gameObject);
        }
    }
}
