using UnityEngine;
using System.Collections;

public class SuicideEnemy : MonoBehaviour
{

	public float explosionForce;
	public float explosionRadius;
	public float damage;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MaybeExplode();
	}

	void MaybeExplode()
	{

		Rigidbody2D body = GetComponent<Rigidbody2D>();
		GameObject player = GameObject.FindWithTag("Player");
		if (body.IsTouching(player.GetComponent<CircleCollider2D>()))
		{

			GetComponent<UnitHealth>().Die();
			Vector3 explosionPosition = body.transform.position;
			foreach (GameObject enemy in GameManager.currentFloor.currentRoom.GetEnemiesInRoom())
			{
				RigidBodyExpansion enemyBodyExp = enemy.GetComponent<RigidBodyExpansion>();
				enemyBodyExp.Explosion(
					enemy.GetComponent<Rigidbody2D>(),
					explosionForce, explosionPosition, explosionRadius);
			}
			player.GetComponent<RigidBodyExpansion>().Explosion(player.GetComponent<Rigidbody2D>(),
			   explosionForce, explosionPosition, explosionRadius);
			player.GetComponent<UnitHealth>().LoseHealth(damage);
		}

	}
}
