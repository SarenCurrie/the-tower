using UnityEngine;
using System.Collections;

/// <summary>
///
/// Script associated with the elevator that spawns in every boss room
///
/// </summary>
public class Elevator : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.transform.tag == Tags.PLAYER && GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
        {
			//Multiplies the player score by their remaining health
			//and then sets their health to max health again
			UIController.GetUI().GetScoreManager().FloorClear();

			GameManager.MovePlayerToNextFloor();
		}
	}
}
