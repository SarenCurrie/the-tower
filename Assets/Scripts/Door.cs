using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum DOOR_ORIENTATION { TOP, BOTTOM, LEFT, RIGHT, DISABLED };
	public DOOR_ORIENTATION orientation;

	//How far to move player
	public const float DOOR_MOVEMENT_VALUE = 1.5f;

	//How long player must stand in door before it works
	public const float DOOR_CONTACT_TIME = 2f;

	private Vector3 cameraDestination = Camera.main.transform.position;

	private float t = 0f;

	//What the camera is currently doing
	private enum CAMERA_STATE {NONE, MOVEING_ROOM, MOVING_BACK};
	private CAMERA_STATE cameraState = CAMERA_STATE.NONE;

	public void Update()
	{
		if (cameraState != CAMERA_STATE.NONE)
		{
			if (t >= DOOR_CONTACT_TIME)
			{
				if (cameraState == CAMERA_STATE.MOVEING_ROOM)
					MovePlayer();
				t = 0;
				cameraState = CAMERA_STATE.NONE;
			}
			else
			{
				print(t / DOOR_CONTACT_TIME);
				Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraDestination, (t / DOOR_CONTACT_TIME)/10);
				t += Time.deltaTime;
			}
		}
	}

	private void MovePlayer()
	{
		Floor currentFloor = GameManager.currentFloor;
		currentFloor.currentRoom = currentFloor.GetDoorDestination(this);

		//In case camera pan doesn't make it all the way
		Camera.main.transform.position = GameManager.currentFloor.currentRoom.GetCameraPosition();

		Vector3 playerOffset = new Vector3();
		switch (orientation)
		{
			case Door.DOOR_ORIENTATION.BOTTOM:
				playerOffset = Vector3.down;
				break;
			case Door.DOOR_ORIENTATION.TOP:
				playerOffset = Vector3.up;
				break;
			case Door.DOOR_ORIENTATION.LEFT:
				playerOffset = Vector3.left;
				break;
			case Door.DOOR_ORIENTATION.RIGHT:
				playerOffset = Vector3.right;
				break;
		}

		GameManager.player.transform.position += playerOffset * DOOR_MOVEMENT_VALUE;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED)
		{
			t = 0f;
			if (cameraState == CAMERA_STATE.MOVEING_ROOM)
			{
				cameraState = CAMERA_STATE.MOVING_BACK;
				cameraDestination = GameManager.currentFloor.currentRoom.GetCameraPosition();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED
			&& GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
		{
			cameraState = CAMERA_STATE.MOVEING_ROOM;
			t = 0f;
			cameraDestination = GameManager.currentFloor.GetDoorDestination(this).GetCameraPosition();
		}
	}
}
