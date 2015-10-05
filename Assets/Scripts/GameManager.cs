using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This GameManager class is responsible for instantiating a floor and player, and
/// moving the player to that floor.
/// 
/// </summary>
public class GameManager : MonoBehaviour {

	public GameObject floorPrefab;

    public GameObject playerPrefab;

    public static GameObject player;

    public static Floor currentFloor;
    
    void Start () {
		currentFloor = Instantiate(floorPrefab).GetComponent<Floor>();
		currentFloor.GenerateFloor();
		player = Instantiate(playerPrefab) as GameObject;
		currentFloor.MovePlayerToFloor(player);
    }

    public static GameObject GetPlayer()
    {
		return player;
    }
}