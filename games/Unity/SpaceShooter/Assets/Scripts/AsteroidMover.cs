using UnityEngine;
using System.Collections;

public class AsteroidMover : MonoBehaviour
{
    public float speed;
    public float speedLevelModifier;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController script");
        }
        GetComponent<Rigidbody>().velocity = transform.forward * (speed - (gameController.GetLevel() * speedLevelModifier));
    }
}
