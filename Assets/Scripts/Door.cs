using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public enum DOOR_ORIENTATION { TOP, BOTTOM, LEFT, RIGHT, DISABLED };
    public DOOR_ORIENTATION orientation;

	//How far to move player
    public const float DOOR_MOVEMENT_VALUE = 1.3f;

	public const float DOOR_CONTACT_TIME = 1;

	private float collision_start_time = Mathf.Infinity;

	public void Update()
	{
		if (Time.time > collision_start_time + DOOR_CONTACT_TIME)
		{
			if (GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
			{
				GameManager.currentFloor.MovePlayerRoom(this);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED)
		{
			collision_start_time = Mathf.Infinity;
		}
	}

    void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED)
		{
			collision_start_time = Time.time;
		}
    }
}
