using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.transform.tag == Tags.PLAYER && GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
        {
            GameManager.MovePlayerToNextFloor();
        }
	}
}
