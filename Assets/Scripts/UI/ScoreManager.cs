﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 
/// This script is used to modify the player's score. It handles GUI updating.
/// 
/// </summary>
public class ScoreManager {

	//The score for the current play through
	public int score = 0;
	public int floorMultiplier = 0;

	/**
	 * Increments the score by an integer value
	 */
	public void IncrementScore(int score)
	{
		SetScore(this.score + score);
	}

	/**
	 * Called when a player finishes a floor. This multiplies the players
	 * score by the amount of health remaining and the current floor number
	 * and then resets their health.
	 */
	public void FloorClear()
	{
		int health = (int)GameManager.GetPlayer().GetComponent<UnitHealth>().health;

		IncrementScore(health * floorMultiplier * 50);

		//Increment the floor multiplier
		floorMultiplier++;
	}

	private void SetScore(int newScore)
	{
		score = newScore;
		UIController.GetUI().SetScore(newScore);
	}

}
