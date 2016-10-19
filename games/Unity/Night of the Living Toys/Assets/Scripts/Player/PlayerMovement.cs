using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100.0f; // ray cast from camera to floor layer

    // awake called before start, use to set up references
    void Awake()
    {
        // get the layer mask for the "Floor" quad 
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // called every physics update
    void FixedUpdate()
    {
        // get raw input (-1, 0, or 1, nothing in between)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // call move functions
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // normalize the input so diagonal is not faster
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        // move player from current position to curr + movement vector
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // we want to turn the player to where the mouse is pointing

        // cast a ray from the camera into the scene from the point of the mouse
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        // if the ray cast hits something in the scene
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // create a vector from the player to where the ray cast hit
            Vector3 playerToMouse = floorHit.point - transform.position;
            // set y to 0, because the ray will be going to the floor
            playerToMouse.y = 0.0f;

            // look down that vector
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // if either h or v has a value, set walking to true
        bool walking = h != 0.0f || v != 0.0f;
        anim.SetBool("IsWalking", walking);
    }
}
