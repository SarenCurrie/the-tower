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

    //The number of enemies in the room
    public int enemyCount = 3;

    // The list of enemies that are in the room.
    public GameObject[] enemies;

    //The room that this world manager is managing.
    public GameObject room;

    public bool generatedRoom = false;

    public bool finishedRoom = false;

    /**
     * With luck, this will generate a room.
     */
    public void Generate(GameObject roomTo, GameObject[] enemyTypes)
    {
        enemies = new GameObject[enemyCount];

        // Sets up the room.
        this.room = Instantiate(roomTo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        // How do you do something like this? I Googled but I think I'm using the wrong terminology
        //Vector3 playerSpawn = room.GetComponentInChildren<Entrance>().

        int enemyType;

        // This is really dumb but it's because I can't solve the problem mentioned above so I am
        // hardcoding all the spawn locations to correlate with the spawn points in the room.
        for (int j = 0; j < enemies.Length; j++)
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

        generatedRoom = true;
        SpawnPlayer();
    }

    /**
     * This checks if all the enemies are dead.
     */
    public void checkEnemies()
    {
        if (!generatedRoom)
            return;

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
    //Currently hard coded location, should derive location for spawn based
    //on which door the player entered the room from.
    public void SpawnPlayer()
    {
        GetPlayer().transform.position = new Vector3(-4, 0);
    }

    /**
     * Check if the player is dead
     */
    public void checkPlayer()
    {
		if (!generatedRoom)
			return;
		//Player died
		if (GetPlayer() == null)
			print("YOU SUCK");     
    }

    public GameObject GetPlayer()
    {
        return GameManager.GetPlayer();
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        checkEnemies();
        checkPlayer();
	}
}
