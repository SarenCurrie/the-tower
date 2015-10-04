using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // The list of possible rooms that can be spawned
    //TODO: Make 3 different versions for the different tiers
    public GameObject[] rooms;

    // The types of enemies that can be spawned
    //TODO: Make 3 different versions for the different tiers
    public GameObject[] enemyTypes;

    // The player prefab
    public GameObject playerPrefab;

    // The actual player
    public static GameObject player;

    // This is probably bad too but this is just a prototype
    public bool floorMapCreated = false;

    //The current floor map
    public static GameObject[][] floorMap;

    //The current world manager object
    private WorldManager worldManager;

    //X Rooms
    public const int ROOM_X = 7;

    //Y Rooms
    public const int ROOM_Y = 7;

    /**
     * Moves the player off the screen (as in out of the camera).
     */
    public static void FinishRoom()
    {
        player.transform.position = new Vector3(-20, -20);
    }

    // Use this for initialization
    void Start () {
        // player starts off the screen
        player = Instantiate(playerPrefab, new Vector3(-20, -20), Quaternion.identity) as GameObject;

        // generates rooms based on the ROOMS_X and ROOMS_Y constants
        floorMap = new GameObject[ROOM_X][];

        int randRoomIndex;

        // Create the map of rooms
        for (int x = 0; x < ROOM_X; x++)
        {
            floorMap[x] = new GameObject[ROOM_Y];
            for (int y = 0; y < ROOM_Y; y++)
            {
                randRoomIndex = Random.Range(0, rooms.Length);
                floorMap[x][y] = rooms[randRoomIndex];
            }
        }

        floorMapCreated = true;

        //Always start in room [0][0]
        worldManager = new WorldManager();
        //Generate the first room, pass the enemytypes that are relevant for this floor
        //pass the predetermined room gameobject that this world manager is for
        for(int x = 0; x < ROOM_X; x++)
        {
            for(int y = 0; y < ROOM_Y; y++)
            {
                float ycast = y;
                ycast = -ycast * (float)5.4;
                worldManager.Generate(floorMap[x][y], enemyTypes,x*9,ycast);
            }
        }
    }

    // Get the player
    public static GameObject GetPlayer()
    {
        if (player == null)
            print("YOU HAVE MADE A MISTAKE, MAX WILL FIX IT");
        return player;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
