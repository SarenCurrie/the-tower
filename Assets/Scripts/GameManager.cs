using UnityEngine;
using System.Collections;
using Achievements;

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

    public static AchievementHandler achievementHandler;

	//What can enemies see through?
	public LayerMask enemySightLayerMask;
	public static LayerMask staticEnemySightLayerMask;

	void Start () {
		staticFloorPrefabs = floorPrefabs;
		staticEnemySightLayerMask = enemySightLayerMask;

		player = Instantiate(playerPrefab) as GameObject;
		MovePlayerToNextFloor();
    }

    public static GameObject GetPlayer()
    {
		return player;
    }

    // To be called when the player restarts the game
    public static void Restart()
    {
        currentFloorNumber = -1;
    }

	public static int GetCurrentFloorNumber()
	{
		return currentFloorNumber;
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
			achievementHandler = new AchievementHandler();
        }
        else
        {
            print("You are already at the top floor!");
        }
	}
}