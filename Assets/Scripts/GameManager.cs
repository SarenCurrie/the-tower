using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Non static fields can be edited with the Unity editor.
	public GameObject[] floorPrefabs;
	//Will be set to floorPrefabs
	public static GameObject[] staticFloorPrefabs;

	//The player to instantiate
	public GameObject playerPrefab;

    public static GameObject player;

	//Player does not start on a floor
	private static int currentFloorNumber = -1;
	public static Floor currentFloor;
    
    void Start () {
		staticFloorPrefabs = floorPrefabs;

		player = Instantiate(playerPrefab) as GameObject;
		MovePlayerToNextFloor();
    }

    public static GameObject GetPlayer()
    {
		return player;
    }

	public static void MovePlayerToNextFloor()
	{
		if (currentFloor != null)
			Destroy(currentFloor.gameObject);

		currentFloorNumber++;

        if (currentFloorNumber < staticFloorPrefabs.Length)
        {
            //Spawn the floor, generate it, and move the player to it
            currentFloor = Instantiate(staticFloorPrefabs[currentFloorNumber]).GetComponent<Floor>();
            currentFloor.GenerateFloor();
            currentFloor.MovePlayerToFloor(player);
        }
        else
        {
            print("You are already at the top floor!");
        }
	}
}