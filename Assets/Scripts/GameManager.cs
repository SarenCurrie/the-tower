using UnityEngine;
using System.Collections;
using Achievements;

public class GameManager : MonoBehaviour {

	public GameObject floorPrefab;

	public GameObject playerPrefab;

	public static GameObject player;

	public static Floor currentFloor;

	public static AchievementHandler achievementHandler;

	void Start () {
		currentFloor = Instantiate(floorPrefab).GetComponent<Floor>();
		currentFloor.GenerateFloor();
		player = Instantiate(playerPrefab) as GameObject;
		currentFloor.MovePlayerToFloor(player);
		achievementHandler = new AchievementHandler();
	}

	public static GameObject GetPlayer()
	{
		return player;
	}
}