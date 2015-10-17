using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///
/// Enemy that will chase the player, dealing damage as soon as it makes contact with them
///
/// </summary>
public class ContactEnemy : Enemy
{
	private float attackTick = 0;
	public float attackTime;
	public float damage;

	void Update()
	{
		ContactDamage();
	}

	/**
	* Deals damage to the player if the the enemy is in contact with it
	*/
	void ContactDamage()
	{
		if (Time.time > attackTick + attackTime)
		{
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			CircleCollider2D player = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();
			if (player == null)
				return;
			if (body.IsTouching(player))
			{
				UnitHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<UnitHealth>();
				playerHealth.LoseHealth(damage);
			}
			attackTick = Time.time;
		}
    }
}
