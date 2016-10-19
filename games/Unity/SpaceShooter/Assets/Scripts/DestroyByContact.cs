using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private int health;

    void Start()
    {
        health = 3;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController script");
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boundary")
        {
            return;
        }

        if (other.tag == "Bolt")
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                gameController.AddScore(scoreValue);
            }
        }
        else if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            health = 0;
            Destroy(other.gameObject);
        }
       
        
        if (health <= 0)
        {
            // destroy the asteroid
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
