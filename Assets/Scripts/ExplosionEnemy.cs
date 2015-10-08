using UnityEngine;
using System.Collections;

public class ExplosionEnemy : MonoBehaviour {

	public float explosionForce;
	public float explosionRadius;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MaybeExplode();
	}

	void MaybeExplode()
	{
		
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		GameObject player = GameObject.FindWithTag("Player");
		if (body.IsTouching(player.GetComponent<CircleCollider2D>()))
		{
			Vector3 explosionPosition = body.transform.position;
			foreach (Enemy enemy in GameManager.currentFloor.currentRoom.GetEnemiesInRoom())
			{
				enemy.GetComponent<RigidbodyExplansion>().Explosion(enemy.GetComponent<RigidBody>(),
					explosionForce, explosionPosition, explosionRadius);
			}
			player.GetComponent<RigidbodyExpansion>().Explosion(player.GetComponent<RigidBody2D>(),
			   explosionForce, explosionPosition, explosionRadius);
		}
    }
}
