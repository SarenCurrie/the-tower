using UnityEngine;
using System.Collections;


/**
*The behaviour script for the second boss
*/

public class BossBehaviour2 : BossBehaviour
{


	private bool isCharging = false;
	public float chargeDamage;

	private float attackTick = 0;
	public float attackTime;

	public float movementSpeed;
	public int baseScore;

	void Update()
	{
		if (GameManager.GetPlayer() == null)
			return;

		//Check if the boss is charging
		if (isCharging)
		{   //Call the SpreadShotEnemy script to fire, 
			//rotate to face the player and move towards the player

			gameObject.GetComponent<SpreadShotEnemy>().Fire();
			MoveToPlayer();
			RotateToFacePlayer();
		}
		//Check if the boss is ready to attack again
		if (Time.time > attackTick + attackTime)
		{
			isCharging = true;
		}
	}

	private void MoveToPlayer()
	{
		GetComponent<Rigidbody2D>().AddForce(transform.up * movementSpeed * Time.deltaTime);
	}

	//Change the attackTick to the current time when boss is enabled so it doesn't attack 
	//on player entering the room
	void OnEnable()
	{
		attackTick = Time.time;
	}

	//On collision the boss should stop adding force and set charging state to false.
	//Should also do damage to the player should it be touching it
	void OnCollisionEnter2D(Collision2D collision)
	{   //Check to see if it hasn't collided with a projectile
		if (collision.gameObject.tag != "PlayerProjectile")
		{
			attackTick = Time.time;
			isCharging = false;
			Rigidbody2D body = GetComponent<Rigidbody2D>();
			CircleCollider2D player = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();

			//Check if body is touching the player and if so then damage the player
			if (body.IsTouching(player))
			{
				UnitHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<UnitHealth>();
				playerHealth.LoseHealth(chargeDamage);
			}
		}
	}


}
