using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public const int NO_ROOMS_X = 3;
	public const int NO_ROOMS_Y = 3;

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

		set
		{
			currentRoom.DisableOrEnableEnemies(false);
			for (int x = 0; x < floorMap.Length; x++)
			{
				for (int y = 0; y < floorMap[0].Length; y++)
				{
					if (floorMap[x][y] == value.gameObject)
					{
						currentRoomX = x;
						currentRoomY = y;
						currentRoom.DisableOrEnableEnemies(true);
					}
				}
			}
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
				GameObject room = Instantiate(rooms[randRoomIndex], worldPosition, Quaternion.identity) as GameObject;
				Room roomScript = room.GetComponent<Room>();
				floorMap[x][y] = room;

				//roomScript.SpawnEnemies(enemies);
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

	public Room GetDoorDestination(Door door)
	{
		int x = currentRoomX;
		int y = currentRoomY;

		switch (door.orientation)
		{
			case Door.DOOR_ORIENTATION.BOTTOM:
				if (currentRoomY < NO_ROOMS_Y - 1)
					y++;
				break;
			case Door.DOOR_ORIENTATION.TOP:
				if (currentRoomY > 0)
					y--;
				break;
			case Door.DOOR_ORIENTATION.LEFT:
				if (currentRoomX > 0)
					x--;
				break;
			case Door.DOOR_ORIENTATION.RIGHT:
				if (currentRoomX < NO_ROOMS_X - 1)
					x++;
				break;
		}
		return floorMap[x][y].GetComponent<Room>();
	}

	public void MovePlayerToFloor(GameObject player)
	{
		currentRoom.MovePlayerToRoom(player);
		currentRoom.DisableOrEnableEnemies(true);
	}
}
