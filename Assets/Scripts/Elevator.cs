using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.transform.tag == Tags.PLAYER && GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
        {
			//Multiplies the player score by their remaining health
			//and then sets their health to max health again
			Canvas.FindObjectOfType<ScoreManager>().FloorClear();

			GameManager.MovePlayerToNextFloor();
		}
	}
}
