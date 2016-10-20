using UnityEngine;
using System.Collections;
using System;

public class LevelController : MonoBehaviour 
{
    public GUIText winText;
    public GUIText restartText;
    public GUIText timer;
    private int timerNum;
    private float timerCount;

    public GUIText levelTime;
    public GameObject level1Prefab;
    public GameObject level2Prefab;
    public GameObject level3Prefab;
    private GameObject currLevelPrefab;

    private float currTime;
    private int prevLevel;
    public int currentLevel;

    private float level1Time;
    private float level2Time;
    private float level3Time;

    bool isTimer;
    bool restart;

	// Use this for initialization
	void Start() 
    {
        winText.text = "";
        restartText.text = "";
        isTimer = true;
        timerCount = 0.00f;
        timer.text = "3";
        timerNum = 3;

        restart = false;

        currTime = 0.00f;
        prevLevel = 1;
        currentLevel = 1;
        currLevelPrefab = Instantiate(level1Prefab) as GameObject;
	}

	// Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            levelTime.text = "";
            DisplayTimer();
            return;
        }
        else if (restart)
        {
            restartText.text = "Press Space to Restart";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        
        if (prevLevel != currentLevel)
        {
            LoadLevel(currentLevel);
            prevLevel = currentLevel;
        }
        if (currentLevel < 4)
        {
            currTime += Time.deltaTime;
            levelTime.text = "Level " + currentLevel + " Time: " + Math.Round(currTime, 2);
        }
        else
        {
            levelTime.text = "";
            restart = true;
        }
    }
    void LoadLevel(int level)
    {
        switch (level)
        {
            case 2:
                level1Time = currTime;
                Destroy(currLevelPrefab);
                currLevelPrefab = Instantiate(level2Prefab) as GameObject;
                isTimer = true;
                timer.text = timerNum.ToString();
                break;
            case 3:
                level2Time = currTime;
                Destroy(currLevelPrefab);
                currLevelPrefab = Instantiate(level3Prefab) as GameObject;
                isTimer = true;
                timer.text = timerNum.ToString();
                break;
            case 4:
                level3Time = currTime;
                winText.text = "Level 1 Time: " + level1Time.ToString() + "\nLevel 2 Time: " + level2Time.ToString() + "\nLevel 3 Time: " + level3Time.ToString();
                Debug.Log("Attempting Upload...");
                var url = "http://deanmarsinelli.com/projects/games/RollingBall/highscores.php";
                var form = new WWWForm();
                form.AddField("level1", level1Time.ToString());
                form.AddField("level2", level2Time.ToString());
                form.AddField("level3", level3Time.ToString());
                var w = new WWW(url, form);
                StartCoroutine(WaitForRequest(w));
                break;
        }
        currTime = 0.00f;
    }

    void DisplayTimer()
    {
       if (timerCount == 0.0f)
       {
           GetComponents<AudioSource>()[0].Play();
       }
       timerCount += Time.deltaTime;
       if (timerCount >= 1.0f)
        {
            
            timerNum--;
            timer.text = timerNum.ToString();
            timerCount = 0.00f;
        }
        if (timerNum == 0)
        {
            GetComponents<AudioSource>()[1].Play();
            timerNum = 3;
            timer.text = "";
            timerCount = 0.00f;
            isTimer = false;
        }
    }

    IEnumerator WaitForRequest(WWW w)
     {
         yield return w;
 
         // check for errors
         if (w.error == null)
         {
             Debug.Log("WWW Ok!: " + w.text);
         } else {
             Debug.Log("WWW Error: "+ w.error);
         }    
     }   
}
