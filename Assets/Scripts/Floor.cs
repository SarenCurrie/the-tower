using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor : MonoBehaviour {

	public const int NO_ROOMS_X = 7;
	public const int NO_ROOMS_Y = 7;

	public const int STARTING_ROOM_X = NO_ROOMS_X/2;
	public const int STARTING_ROOM_Y = NO_ROOMS_Y/2;

	//Percentage chance flavour text will be displayed upon entering room
	public const int DIALOGCHANCE = 100;
	//Amount of time that DIALOG is displayed
	public const int DIALOGTIME = 5;

	//How many rooms there will be. Guaranteed to be reachable.
	int NO_ROOMS = 14;

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

		int room_num = 0;
		while (room_num < NO_ROOMS)
		{
			if (floorMap[x][y] == null)
			{
				//Which room to spawn
				int randRoomIndex = Random.Range(0, rooms.Length);
				//Where to spawn it
				Vector2 worldPosition = new Vector2(x * Room.ROOM_WIDTH, -y * Room.ROOM_HEIGHT);

				//Spawn the room and add it to the grid
				GameObject room = Instantiate(rooms[randRoomIndex], worldPosition, Quaternion.identity) as GameObject;
				Room roomScript = room.GetComponent<Room>();
				floorMap[x][y] = room;
				roomPath.Add(new int[] {x, y});

				//No enemies in the first room
				if (room_num != 0)
				{
					roomScript.SpawnEnemies(enemies);
					roomScript.DisableOrEnableEnemies(false);
				}

				room_num++;
			}

			//Choose new direction
			int  direction = Random.Range(0, 4);
			if (direction == 0 && x + 1 < NO_ROOMS_X)
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
			// TODO uncomment this when it works
			//SpeechScreen.ShowDialog(floorDialog[index], DIALOGTIME);
			floorDialog.RemoveAt(index);
		}
	}

	public void MovePlayerToFloor(GameObject player)
	{
		currentRoom.MovePlayerToRoom(player);
		currentRoom.DisableOrEnableEnemies(true);
		Camera.main.transform.position = currentRoom.GetCameraPosition();
		// TODO uncomment when this works
		// Show the entry dialog
		//SpeechScreen.ShowDialog(enterDialog, DIALOGTIME);
	}
}
