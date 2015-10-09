using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		GameManager.MovePlayerToNextFloor();
	}
}
