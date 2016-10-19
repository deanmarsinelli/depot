using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

	// Use this for initialization
	void Awake ()
    {
        // load the game manager on the first level
	    if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
	}
}
