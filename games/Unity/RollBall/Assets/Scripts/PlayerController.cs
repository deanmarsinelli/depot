using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector3 movement;
    private int count;
    public GUIText countText;
    private int currentLevel;
    public LevelController levelController;
    private bool isTimer;
    private float timer;

    // Start called on the frame when a script is enabled
    void Start()
    {
        isTimer = true;
        timer = 0.0f;

        count = 0;
        currentLevel = 1;
        movement = new Vector3(0.0f, 0.0f, 0.0f);
        SetCountText();
    }

    void Update()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
            if(timer >= 3.0f)
            {
                isTimer = false;
            }
        }
    }

    // FixedUpdate called before performing physics calculations
    void FixedUpdate()
    {
        if (isTimer)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement.x = moveHorizontal;
        movement.z = moveVertical;

        // this function is called independently of framerate
        // so not sure that deltaTime for smoothing is really needed
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }

    /*
     * OnTriggerEnter will be called when our object first touches
     * a trigger collider. We are given a reference to the collider it
     * touched as an argument called other.
     */
    void OnTriggerEnter(Collider other)
    {   
        // if the collider belongs to a pickup
        if (other.gameObject.tag == "PickUp")
        {
            GetComponent<AudioSource>().Play();
            // destroy the pickup
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            count++;
            if (count >= 12)
            {
                count = 0;
                currentLevel++;
                if (currentLevel < 4)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    isTimer = true;
                    timer = 0.00f;
                }

                if (currentLevel == 2)
                {
                    Vector3 reset = new Vector3(-7.0f, 0.5f, 7.0f);
                    this.transform.position = reset;
                }
                else if (currentLevel == 3)
                {
                    Vector3 reset = new Vector3(-7.0f, 0.5f, 0.0f);
                    this.transform.position = reset;
                }
                
                levelController.currentLevel = currentLevel;
            }
            SetCountText();
        }
       
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}