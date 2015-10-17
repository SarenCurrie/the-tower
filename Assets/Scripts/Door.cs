using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

	public enum DOOR_ORIENTATION { TOP, BOTTOM, LEFT, RIGHT, DISABLED };
	public DOOR_ORIENTATION orientation;

	//How far to move player
	public const float DOOR_MOVEMENT_VALUE = 1.3f;

	//How long player must stand in door before the camera fully moves and player is teleported
	public const float DOOR_CONTACT_TIME = 1f;

	private Vector3 cameraDestination;

	//Current pan time. Will be between 0 and DOOR_CONTACT_TIME
	private float transitionTime = 0f;

	//What the camera is currently doing
	private enum CAMERA_STATE { NONE, MOVING_ROOM, MOVING_BACK };
	private CAMERA_STATE cameraState = CAMERA_STATE.NONE;

	private const float UNBLACKINGING_TIME = DOOR_CONTACT_TIME;
	public const float BLACK_ALPHA = 0.8f;
	public const float UNBLACK_ALPHA = 0f;

	private const float MAX_FLICKER_VALUE = 0.5f;
	private const float FLICKER_CHANCE = 0.025f;

	void Start()
	{
		cameraDestination = Camera.main.transform.position;
	}

	public void Update()
	{
		if (cameraState != CAMERA_STATE.NONE)
		{
			if (transitionTime >= DOOR_CONTACT_TIME)
			{
				if (cameraState == CAMERA_STATE.MOVING_ROOM)
					MovePlayer();
				transitionTime = 0f;
				cameraState = CAMERA_STATE.NONE;
			}
			else if (transitionTime < 0)
			{
				transitionTime = 0f;
				cameraState = CAMERA_STATE.NONE;
			}
			else
			{
				Room currentRoom = GameManager.currentFloor.currentRoom;
				Room nextRoomPos = GameManager.currentFloor.GetDoorDestination(orientation);

				//Fade camera
				Camera.main.transform.position = Vector3.Lerp(currentRoom.GetCameraPosition(), nextRoomPos.GetCameraPosition(), (transitionTime / DOOR_CONTACT_TIME));

				//Zoom camera
				Camera.main.orthographicSize = Mathf.Lerp(currentRoom.GetCameraSize(), nextRoomPos.GetCameraSize(), (transitionTime / DOOR_CONTACT_TIME));


				//Fade lights
				float flicker = 0;
				if (Random.Range(0f, 1f) > 1 - FLICKER_CHANCE)
					flicker = Random.Range(0, MAX_FLICKER_VALUE);
				currentRoom.SetBlackerAlpha(Mathf.Lerp(UNBLACK_ALPHA, BLACK_ALPHA, transitionTime/UNBLACKINGING_TIME));
				nextRoomPos.SetBlackerAlpha(Mathf.Lerp(BLACK_ALPHA, UNBLACK_ALPHA, transitionTime/UNBLACKINGING_TIME) + flicker);

				//Update time
				if (cameraState == CAMERA_STATE.MOVING_ROOM)
					transitionTime += Time.deltaTime;
				else if (cameraState == CAMERA_STATE.MOVING_BACK)
					transitionTime -= Time.deltaTime;
			}
		}
	}

	private void MovePlayer()
	{
		Floor currentFloor = GameManager.currentFloor;
		currentFloor.currentRoom = currentFloor.GetDoorDestination(orientation);

		//In case lerp doesn't make it all the way
		Camera.main.transform.position = GameManager.currentFloor.currentRoom.GetCameraPosition();
		currentFloor.currentRoom.SetBlackerAlpha(UNBLACK_ALPHA);

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
			if (cameraState == CAMERA_STATE.MOVING_ROOM)
			{
				cameraState = CAMERA_STATE.MOVING_BACK;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.transform.tag == Tags.PLAYER && orientation != DOOR_ORIENTATION.DISABLED
			&& GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
		{
			cameraState = CAMERA_STATE.MOVING_ROOM;
		}
	}
}
