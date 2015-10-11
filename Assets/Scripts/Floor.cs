using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

	public const int NO_ROOMS_X = 7;
	public const int NO_ROOMS_Y = 7;

	public const int STARTING_ROOM_X = NO_ROOMS_X/2;
	public const int STARTING_ROOM_Y = NO_ROOMS_Y/2;

	//Percentage chance flavour text will be displayed upon entering room
	public const int DIALOGCHANCE = 20;
	//Amount of time that DIALOG is displayed
	public const int DIALOGTIME = 10;

	//How many rooms there will be. Guaranteed to be reachable.
	//Excludes first room and boss room
	public int numberOfRooms = 14;

	//Posible enemies to have on this floor
	public GameObject[] enemies;

	//The list of possible rooms that can be spawned
	//TODO: Make 3 different versions for the different tiers
	public GameObject[] rooms;

	//The flavour text upon entering the floor
	public string enterDialog;

	//The flavour text upon beating the boss
	public string exitDialog;

	//The flavour text that can occur throughout the floor
	public List<string> floorDialog = new List<string>();

	public GameObject firstRoom;
	public GameObject bossRoom;

	public GameObject screenBlackerPrefab;

	//Stores the generated path of room indexes
	private List<int[]> roomPath = new List<int[]>();

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

			// Have a chance to display some of the flavour text
			int r = Random.Range(0, 101);
			if(r <= DIALOGCHANCE)
			{
				DisplayFloorDialog();
			}
		}
	}

	public void GenerateFloor()
	{
		for (int i = 0; i < NO_ROOMS_X; i++)
			floorMap[i] = new GameObject[NO_ROOMS_Y];

		int x = STARTING_ROOM_X;
		int y = STARTING_ROOM_Y;

		int roomNum = -1;
		while (roomNum <= numberOfRooms)
		{
			if (floorMap[x][y] == null)
			{
				if (roomNum == -1)
				{
					//Spawn first room
					floorMap[x][y] = Instantiate(firstRoom, GetWorldPosition(x, y), Quaternion.identity) as GameObject;
				}
				else if (roomNum == numberOfRooms)
				{
					//Spawn boss room
					floorMap[x][y] = Instantiate(bossRoom, GetWorldPosition(x, y), Quaternion.identity) as GameObject;
					floorMap[x][y].GetComponent<Room>().DisableOrEnableEnemies(false);
				}
				else
				{
					//Spawn a random room
					int randRoomIndex = Random.Range(0, rooms.Length);
					GameObject room = Instantiate(rooms[randRoomIndex], GetWorldPosition(x, y), Quaternion.identity) as GameObject;
					Room roomScript = room.GetComponent<Room>();

					//Spawn the room and add it to the grid
					floorMap[x][y] = room;

					//Spawn all enemies disabled
					roomScript.SpawnEnemies(enemies);
					roomScript.DisableOrEnableEnemies(false);

					if (screenBlackerPrefab != null)
						floorMap[x][y].GetComponent<Room>().SpawnScreenBlacker(screenBlackerPrefab);
				}

                //Rooms should be removed when the floor is
                floorMap[x][y].transform.parent = transform;

                //Add to path of generated rooms
				roomPath.Add(new int[] { x, y });
				roomNum++;
			}

			//Choose new direction
			int  direction = Random.Range(0, 4);

			//First room of first floor
			if (roomNum == 0 && GameManager.GetCurrentFloorNumber() == 0)
				x++;
			else if (direction == 0 && x + 1 < NO_ROOMS_X)
				x++;
			else if (direction == 1 && x - 1 >= 0)
				x--;
			else if (direction == 2 && y + 1 < NO_ROOMS_Y)
				y++;
			else if (direction == 3 && y - 1 >= 0)
				y--;
		}

		//Disable all doors that don't lead somewhere
		foreach (int[] roomIndex in roomPath)
		{
			foreach (Door.DOOR_ORIENTATION orientation in System.Enum.GetValues(typeof(Door.DOOR_ORIENTATION)))
			{
				if (GetDoorDestination(roomIndex[0], roomIndex[1], orientation) == floorMap[roomIndex[0]][roomIndex[1]].GetComponent<Room>())
				{
					floorMap[roomIndex[0]][roomIndex[1]].GetComponent<Room>().DisableDoor(orientation);
				}
			}
		}
	}

	private Vector3 GetWorldPosition(int x, int y)
	{
		return new Vector2(x * Room.ROOM_WIDTH, -y * Room.ROOM_HEIGHT);
	}

	public int EnemiesLeft()
	{
		int enemyCount = 0;
		foreach (GameObject[] xRoom in floorMap)
			foreach (GameObject room in xRoom)
				enemyCount += room.GetComponent<Room>().EnemiesLeft();
		return enemyCount;
	}

	public Room GetDoorDestination(Door.DOOR_ORIENTATION orientation)
	{
		return GetDoorDestination(currentRoomX, currentRoomY, orientation);
	}

	public Room GetDoorDestination(int x, int y, Door.DOOR_ORIENTATION orientation)
	{
		switch (orientation)
		{
			case Door.DOOR_ORIENTATION.BOTTOM:
				if (y < NO_ROOMS_Y - 1 && floorMap[x][y + 1] != null)
					y++;
				break;
			case Door.DOOR_ORIENTATION.TOP:
				if (y > 0 && floorMap[x][y - 1] != null)
					y--;
				break;
			case Door.DOOR_ORIENTATION.LEFT:
				if (x > 0 && floorMap[x - 1][y] != null)
					x--;
				break;
			case Door.DOOR_ORIENTATION.RIGHT:
				if (x < NO_ROOMS_X - 1 && floorMap[x + 1][y] != null)
					x++;
				break;
		}
		return floorMap[x][y].GetComponent<Room>();
	}

	// Displays a piece of flavour text and makes sure that it can't be shown again
	public void DisplayFloorDialog()
	{
		int dialogLength = floorDialog.Count;

		if(dialogLength != 0)
		{
			int index = UnityEngine.Random.Range(0, dialogLength);
			SpeechScreen.ShowDialog(floorDialog[index], DIALOGTIME);
			floorDialog.RemoveAt(index);
		}
	}

	public void MovePlayerToFloor(GameObject player)
	{
		currentRoom.MovePlayerToRoom(player);
		currentRoom.DisableOrEnableEnemies(true);
		Camera.main.transform.position = currentRoom.GetCameraPosition();
		// Show the entry dialog
		SpeechScreen.ShowDialog(enterDialog, DIALOGTIME);
	}
}
