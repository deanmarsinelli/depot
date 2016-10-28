using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberWizard : MonoBehaviour
{
	int max;
	int min;
	int guess;
	int maxGuessesAllowed = 6;
	public Text guessText;
	public LevelManager levelManager;
	
	// Use this for initialization
	void Start()
	{
		StartGame();
	}
	
	void StartGame()
	{
		max = 1000;
		min = 1;
		guess = 500;
		max = max + 1; // to fix rounding issues
		
		guessText.text = "Is the number higher or lower than " + guess + "?";
	}

	public void GuessHigher()
	{
		min = guess;
		NextGuess();
	}
	
	public void GuessLower()
	{
		max = guess;
		NextGuess();
	}
	
	void NextGuess()
	{
		guess = (max + min) / 2;
		maxGuessesAllowed -= 1;
		if (maxGuessesAllowed <= 0)
		{
			// player wins
			levelManager.LoadLevel("Win");
		}
		else
		{
			guessText.text = "Is the number higher or lower than " + guess + "?";
		}
	}
}
