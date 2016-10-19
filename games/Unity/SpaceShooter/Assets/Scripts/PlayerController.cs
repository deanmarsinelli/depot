using UnityEngine;
using System.Collections;

// unity needs classes serialized to use the in the inspector
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate = 0.25f;
    private float tempFireRate;
    private float fireTimer;

    public float powerUpTimer = 8.0f;
    private float powerUpClock = 0.0f;
    private bool powerUp;
    private bool doubleShot;

    void Start()
    {
        tempFireRate = fireRate;
        powerUp = false;
        doubleShot = false;
        fireTimer = fireRate;
    }

    // called once per frame
    void Update()
    {
        if (powerUp)
        {
            powerUpClock += Time.deltaTime;
            if (powerUpClock >= powerUpTimer)
            {
                powerUpClock = 0.0f;
                powerUp = false;
                doubleShot = false;
                fireRate = tempFireRate;
            }
        }

        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && fireTimer >= fireRate)
        {
            fireTimer = 0.0f;
            if (doubleShot)
            {
                Vector3 leftSpawn = new Vector3(shotSpawn.position.x - 0.15f, shotSpawn.position.y, shotSpawn.position.z);
                Vector3 rightSpawn = new Vector3(shotSpawn.position.x + 0.15f, shotSpawn.position.y, shotSpawn.position.z);
                Instantiate(shot, leftSpawn, shotSpawn.rotation);
                Instantiate(shot, rightSpawn, shotSpawn.rotation);
            }
            else
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            GetComponent<AudioSource>().Play();
        }
        fireTimer += Time.deltaTime;
    }

    // called once before every physics step
    void FixedUpdate()
    {
        // get the input values
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!powerUp)
        {
            if (other.tag == "DoubleShot")
            {
                powerUp = true;
                doubleShot = true;
            }
            else if (other.tag == "FastShot")
            {
                powerUp = true;
                fireRate = 0.08f;
            }
        }
    }
}
