using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float explosionForce = -1;
	public float explosionRadius = -1;
	public float damage = -1;

	/**
	 * Creates an explosion.
	 */
	public static void CreateExplosion(GameObject explosionPrefab, float explosionForce, float explosionRadius, float damage, Vector3 position)
	{
		GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity) as GameObject;
		explosion.GetComponent<Explosion>().SetupExplosion(explosionForce, explosionRadius, damage);
	}

	private void SetupExplosion(float explosionForce, float explosionRadius, float damage)
	{
		this.explosionForce = explosionForce;
		this.explosionRadius = explosionRadius;
		this.damage = damage;
	}

	void Update()
	{
		//Wait until fields are set
		if (explosionForce == -1 || explosionRadius == -1 || damage == -1)
			return;

		//The position of this suicide enemy
		Vector3 explosionPosition = GetComponent<Rigidbody2D>().transform.position;

		//Loop through all enemies in the room and damage + move them
		foreach (GameObject enemy in GameManager.currentFloor.currentRoom.GetEnemiesInRoom())
		{
			enemy.GetComponent<RigidBodyExpansion>().Explosion(explosionForce, explosionPosition, explosionRadius, damage);
		}

		//Damage + Move the player
		GameManager.GetPlayer().GetComponent<RigidBodyExpansion>().Explosion(explosionForce, explosionPosition, explosionRadius, damage);

		Destroy(this);
	}
}
