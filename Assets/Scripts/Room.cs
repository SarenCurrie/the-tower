using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public const float ROOM_WIDTH = 9.0f;
	public const float ROOM_HEIGHT = 5.4f;

	public const float CAMERA_HEIGHT = -10f;

	public Vector3 GetCameraPosition()
	{
		Vector2 pos = transform.position;
		return new Vector3(pos.x, pos.y, CAMERA_HEIGHT);
	}

	public void SpawnEnemies(GameObject[] enemies)
	{
		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == Tags.ENEMY_SPAWN)
			{
				int randIndex = Random.Range(0, enemies.Length);
				GameObject enemy = Instantiate(enemies[randIndex], t.position, t.rotation) as GameObject;
				enemy.transform.parent = transform;
			}
		}
	}

	public void DisableDoor(Door.DOOR_ORIENTATION disableOrientation)
	{
		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == Tags.DOOR)
			{
				Door door = t.GetComponent<Door>();
				if (door.orientation == disableOrientation)
				{
					Destroy(t.gameObject);
				}
			}
		}
	}

	/// <summary>
	/// Disable or enable all enemies on this floor. Also makes
	/// enemies invisible.
	/// </summary>
	/// <param name="enable">Should enemies be enabled or not?</param>
	public void DisableOrEnableEnemies(bool enable)
	{
		foreach (Transform child in transform)
		{
			if (child.tag == Tags.ENEMY)
				child.gameObject.SetActive(enable);
		}
	}

	/// <summary>
	/// Gets the number of enemies left in the room
	/// </summary>
	/// <returns></returns>
	public int EnemiesLeft()
	{
		int enemyCount = 0;
		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == Tags.ENEMY)
			{
				enemyCount++;
			}
		}
		return enemyCount;
	}

	/// <summary>
	/// Gets a list of enemies in a room
	/// </summary>
	/// <returns></returns>
	public List<GameObject> getEnemiesInRoom()
	{
		List<GameObject> enemies = new List<GameObject>();

		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == Tags.ENEMY)
			{
				enemies.Add(t.gameObject);
			}
		}
		return enemies;
	}

	/// <summary>
	/// Move an existing player gameobject to the player spawnpoint in the room
	/// </summary>
	/// <param name="player">The player gameobject</param>
	public void MovePlayerToRoom(GameObject player)
	{
		foreach (Transform child in transform)
		{
			if (child.tag == Tags.PLAYER_SPAWN)
				player.transform.position = child.transform.position;
		}
	}
}
