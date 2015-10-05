using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This class represents each floor on the tower and 
/// the random generation of a floor plan from a pool of
/// potential rooms.
/// 
/// </summary>
public class Floor : MonoBehaviour {

	public const int NO_ROOMS_X = 3;
	public const int NO_ROOMS_Y = 2;

	public const int STARTING_ROOM_X = 0;
	public const int STARTING_ROOM_Y = 0;

	//Posible enemies to have on this floor
	public GameObject[] enemies;

	//The list of possible rooms that can be spawned
	//TODO: Make 3 different versions for the different tiers
	public GameObject[] rooms;

	//The current floor map
	private GameObject[][] floorMap = new GameObject[NO_ROOMS_X][];

	private int currentRoomX = STARTING_ROOM_X;
	private int currentRoomY = STARTING_ROOM_Y;

	public Room currentRoom
	{
		get
		{
			return floorMap[currentRoomX][currentRoomY].GetComponent<Room>();
		}
	}

	public void GenerateFloor()
	{
		for (int x = 0; x < NO_ROOMS_X; x++)
		{
			floorMap[x] = new GameObject[NO_ROOMS_Y];
			for (int y = 0; y < NO_ROOMS_Y; y++)
			{
				int randRoomIndex = Random.Range(0, rooms.Length);
				Vector2 worldPosition = new Vector2(x * Room.ROOM_WIDTH, -y * Room.ROOM_HEIGHT);
				GameObject room =  Instantiate(rooms[randRoomIndex], worldPosition, Quaternion.identity) as GameObject;
				Room roomScript = room.GetComponent<Room>();
				floorMap[x][y] = room;

				roomScript.SpawnEnemies(enemies);
				roomScript.DisableOrEnableEnemies(false);

				if (x == 0)
					roomScript.DisableDoor(Door.DOOR_ORIENTATION.LEFT);
				if (x == NO_ROOMS_X + 1)
					roomScript.DisableDoor(Door.DOOR_ORIENTATION.RIGHT);
				if (y == 0)
					roomScript.DisableDoor(Door.DOOR_ORIENTATION.TOP);
				if (y == NO_ROOMS_Y + 1)
					roomScript.DisableDoor(Door.DOOR_ORIENTATION.BOTTOM);
            }
		}
	}

	public int EnemiesLeft()
	{
		int enemyCount = 0;
		foreach (GameObject[] xRoom in floorMap)
			foreach (GameObject room in xRoom)
				enemyCount += room.GetComponent<Room>().EnemiesLeft();
		return enemyCount;
	}

	public void MovePlayerToFloor(GameObject player)
	{
        currentRoom.MovePlayerToRoom(player);
		currentRoom.DisableOrEnableEnemies(true);
	}

	public void MovePlayerRoom(Door doorWalkedInto)
	{
		currentRoom.DisableOrEnableEnemies(false);

		Vector3 locationOffset = new Vector3();
		Vector3 cameraOffset = new Vector3();
        switch (doorWalkedInto.orientation)
        {
            case Door.DOOR_ORIENTATION.BOTTOM:
				if (currentRoomY < NO_ROOMS_Y - 1)
				{
					currentRoomY++;
					locationOffset.y = -1;
					cameraOffset.y = -Room.ROOM_HEIGHT;
				}
				break;
			case Door.DOOR_ORIENTATION.TOP:
				if (currentRoomY > 0)
				{
					currentRoomY--;
					locationOffset.y = 1;
					cameraOffset.y = Room.ROOM_HEIGHT;
				}
				break;
			case Door.DOOR_ORIENTATION.LEFT:
				if (currentRoomX > 0)
				{
					currentRoomX--;
					locationOffset.x = -1;
					cameraOffset.x = -Room.ROOM_WIDTH;
				}
				break;
			case Door.DOOR_ORIENTATION.RIGHT:
				if (currentRoomX < NO_ROOMS_X - 1)
				{
					currentRoomX++;
					locationOffset.x = 1;
					cameraOffset.x = Room.ROOM_WIDTH;
				}
                break;
        }
		GameManager.player.transform.position += locationOffset * Door.DOOR_MOVEMENT_VALUE;
		Camera.main.transform.position += cameraOffset;

		currentRoom.DisableOrEnableEnemies(true);
	}
}
