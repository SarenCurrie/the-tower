using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public enum DOOR_ORIENTATION { TOP, BOTTOM, LEFT, RIGHT, DISABLED };
    public DOOR_ORIENTATION orientation;

    public const float DOOR_MOVEMENT_VALUE = 1.5f;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED)
        {
			if (GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
			{
				GameManager.currentFloor.MovePlayerRoom(this);
			}
        }
    }
}
