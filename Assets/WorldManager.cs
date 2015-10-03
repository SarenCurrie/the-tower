using UnityEngine;
using System.Collections;

/**
 * A prototype world manager script. This will generate the room,
 * place enemies in it and the player.
 *
 * We will probably need a GameManager script also which is the overarching
 * script. This script should also potentially be renamed "RoomManager" for
 * the sake of making more sense.
 * 
 * TODO: Include event implementation.
 */
public class WorldManager : MonoBehaviour {

    // The types of enemies that can be spawned
    //TODO: Make 3 different versions for the different tiers
    public GameObject[] enemyTypes;

    // The list of possible rooms that can be spawned
    //TODO: Make 3 different versions for the different tiers
    public GameObject[] rooms;

    // The player prefab
    public GameObject playerPrefab;

    //The number of enemies in the room
    public int enemyCount = 3;

    // The list of enemies that are in the room.
    public GameObject[] enemies;

    //The room that this world manager is managing.
    public GameObject room;

    // The actual player
    public GameObject player;

    public bool finishedRoom = false;

    // This is probably bad too but this is just a prototype
    public bool roomCreated = false;

    /**
     * With luck, this will generate a room.
     */
    private void Generate()
    {
        enemies = new GameObject[enemyCount];

        int i = Random.Range(0, rooms.Length);

        // Sets up the room.
        room = Instantiate(rooms[i], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        // How do you do something like this? I Googled but I think I'm using the wrong terminology
        //Vector3 playerSpawn = room.GetComponentInChildren<Entrance>().

        //Hardcoded player spawn location
        player = Instantiate(playerPrefab, new Vector3(-4, 0), Quaternion.identity) as GameObject;

        int enemyType;

        // This is really dumb but it's because I can't solve the problem mentioned above so I am
        // hardcoding all the spawn locations to correlate with the spawn points in the room.
        for(int j = 0; j < enemies.Length; j++)
        {
            enemyType = Random.Range(0, enemyTypes.Length);
            switch (j)
            {
                case 0:
                    enemies[0] = Instantiate(enemyTypes[enemyType], new Vector3(3, -2, 0), Quaternion.identity) as GameObject;
                    break;
                case 1:
                    enemies[1] = Instantiate(enemyTypes[enemyType], new Vector3(3, 2, 0), Quaternion.identity) as GameObject;
                    break;
                case 2:
                    enemies[2] = Instantiate(enemyTypes[enemyType], new Vector3(-2, -2, 0), Quaternion.identity) as GameObject;
                    break;
            }
        }

        roomCreated = true;
    }

    /**
     * This checks if all the enemies are dead.
     */
    public void checkEnemies()
    {
        if (!roomCreated)
            return;

        //Just going to take a moment out here to say that I absolutely loathe my keyboard
        //and if anyone wants a mechanical keyboard they can have it for $100, it is a fancy
        //ducky one but I honestly don't know how anyone is supposed to type quickly or accurately
        //when all the keys are the size of dinner plates.
        for (int i = 0; i < enemies.Length; i++)
        {
            //An enemy is alive
            if (enemies[i] != null)
                return;
        }

        //All the enemies are dead
        finishedRoom = true;
		print("YOU WIN CLICK THE DOOR");

		//TODO: Make clicking the door something, I just don't know how to reference it.
    }

    /**
     * Check if the player is dead
     */
    public void checkPlayer()
    {
		if (!roomCreated)
			return;
		//Player died
		if (player == null)
			print("YOU SUCK");     
    }


	// Use this for initialization
	void Start () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
        checkEnemies();
        checkPlayer();
	}
}
