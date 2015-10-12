using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script is used to modify the player's score. It handles GUI updating.
/// </summary>
public class ScoreManager : MonoBehaviour {

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

		SetScore(this.score * health * floorMultiplier);

		//Resets the player's health.
		GameManager.GetPlayer().GetComponent<UnitHealth>().ResetHealth();

		//Increment the floor multiplier
		floorMultiplier++;
	}

	private void SetScore(int newScore)
	{
		score = newScore;
		this.transform.Find("Score").GetComponent<Text>().text = newScore.ToString();
	}

}
