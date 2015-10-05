using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 
/// This class represents a contactable enemy which is an enemy
/// that the player can collide with.
/// 
/// </summary>
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
				UnitHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<UnitHealth>();
				playerHealth.LoseHealth(10);
			}
			attackTick = Time.time;
		}
    }
}
