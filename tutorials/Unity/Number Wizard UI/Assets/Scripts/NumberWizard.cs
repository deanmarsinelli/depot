﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberWizard : MonoBehaviour
{
	int max;
	int min;
	int guess;
	int maxGuessesAllowed = 8;
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
		GuessNumber();
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
		maxGuessesAllowed -= 1;
		if (maxGuessesAllowed <= 0)
		{
			// player wins
			levelManager.LoadLevel("Win");
		}
		else
		{
			GuessNumber();
		}
	}
	
	void GuessNumber()
	{
		guess = Random.Range(min, max + 1);
		guessText.text = "Is the number higher or lower than " + guess + "?";
	}
}
