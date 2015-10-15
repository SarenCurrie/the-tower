using UnityEngine;
using System.Collections;

public class SuicideEnemy : Enemy
{

	public float explosionForce;
	public float explosionRadius;
	public float damage;

	public GameObject explosionPrefab;

	private Rigidbody2D body;

	// Use this for initialization
	void Start()
	{
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		MaybeExplode();
	}

	void MaybeExplode()
	{
		if (body.IsTouching(GameManager.GetPlayer().GetComponent<CircleCollider2D>()))
		{
			//Create an explosion at this point
			Explosion.CreateExplosion(explosionPrefab, explosionForce, explosionRadius, damage, transform.position);

			//Kill this suicide enemy
			GetComponent<UnitHealth>().Die();
		}

	}
}
