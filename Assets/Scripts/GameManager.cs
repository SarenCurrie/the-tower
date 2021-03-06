﻿using UnityEngine;
using System.Collections;
using Achievements;

/// <summary>
///
/// The GameManager class persists data over each game
/// This class is reset each time the player starts a new game
///
/// </summary>
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

	//What can enemies see through?
	public LayerMask enemySightLayerMask;
	public static LayerMask staticEnemySightLayerMask;

	//cursor stuff
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = new Vector2(381f, 383.5f);

	void Start () {
		if(cursorTexture != null)
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

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
		if(GameObject.Find("mars-small") != null)
			GameObject.Find("mars-small").GetComponent<Music>().StartMainMusic();
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

		//Resets the player's health.
		GameManager.GetPlayer().GetComponent<UnitHealth>().ResetHealth();

		if (currentFloorNumber < staticFloorPrefabs.Length)
		{
			//Spawn the floor, generate it, and move the player to it
			currentFloor = Instantiate(staticFloorPrefabs[currentFloorNumber]).GetComponent<Floor>();
			currentFloor.GenerateFloor();
			currentFloor.MovePlayerToFloor(player);
			if (currentFloorNumber == 4 && GameObject.Find("mars-small") != null) //Final boss
			{
				GameObject.Find("mars-small").GetComponent<Music>().StartBossMusic();
			}
		}
		else
		{
			print("You are already at the top floor!");
		}
	}
}