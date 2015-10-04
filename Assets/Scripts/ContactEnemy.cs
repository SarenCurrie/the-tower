using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContactEnemy : MonoBehaviour
{

	private float attackTick = 0;
	public float damage;

	void Start()
	{
	}

	void Update()
	{
		ContactDamage();
	}

	void ContactDamage()
	{
		if (Time.time > attackTick + 0.5f)
		{
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			CircleCollider2D player = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();
			if (body.IsTouching(player))
			{
				Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
				playerHealth.LoseHealth(10);
			}
			attackTick = Time.time;
		}
    }
}
