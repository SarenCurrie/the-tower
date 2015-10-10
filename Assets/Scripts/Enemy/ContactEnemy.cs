using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContactEnemy : Enemy
{
	private float attackTick = 0;
	public float attackTime;
	public float damage;

	void Update()
	{
		ContactDamage();
	}

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
