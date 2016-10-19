using UnityEngine;
using System.Collections;

public class SpawnPowerUp : MonoBehaviour {

    public GameObject doubleShot;
    public GameObject fastShot;
    public Vector3 spawnValues;

    public float chancePerSecond;
    private float timer = 0.0f;

	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            float rand = Random.Range(0.0f, 100.0f);

            if (chancePerSecond > rand)
            {
                int powerUp = Random.Range(1, 100);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                
                if ((powerUp % 2) == 0)
                {
                    Instantiate(doubleShot, spawnPosition, spawnRotation); 
                }
                else
                {
                    Instantiate(fastShot, spawnPosition, spawnRotation);
                }
            }

            timer = 0.0f;
        }
	}
}
