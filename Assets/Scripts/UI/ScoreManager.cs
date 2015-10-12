using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This script is used to modify the player's score. It handles GUI updating.
/// </summary>
public class ScoreManager : MonoBehaviour {

	//The score for the current play through
	public int score = 0;

	/**
	 * Increments the score by an integer value
	 */
	public void incrementScore(int score)
	{
		this.score += score;

		this.transform.Find("Score").GetComponent<Text>().text = this.score.ToString();
	}

}
