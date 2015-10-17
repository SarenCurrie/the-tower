using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///
/// Defines how the enemy moves around the room
///
/// </summary>
public class EnemyMovement : Enemy
{

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;
	public int baseScore;

	//Try to get rid of this
	private Vector3 lastKnownPlayerPosition;
	private Vector3 goToPosition;
	private Vector3 otherPatrolPosition;

	private bool canSeePlayer = false;

	private const float PROXIMITY = 0.4f;
	private const int DEGREE_DIVISION = 10;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		FindNewPatrol();
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.GetPlayer() == null)
			return;

		UpdateKnownPlayerPosition();

		// If the enemy can see the player, navigate towards it
		// Otherwise, navigate to the last known position of the player
		// If the players last known position is reached, start patrolling
		if (canSeePlayer)
		{
			AdjustDistanceFromPlayer();
		}
		else if ((transform.position - goToPosition).magnitude < PROXIMITY)
		{
			FindNewPatrol();
		}
		else
		{
			Patrol();
		}
	}

	/*
	* Starts a patrol. The enemy finds the furthest away point that it can navigate to in a straight line
	* and sets it as the next point to travel to.
	*/
	private void FindNewPatrol()
	{
		Vector2 winningPosition = new Vector2();
		float winningDistance = 0;

		for (int angle=0; angle<360; angle+=DEGREE_DIVISION)
		{
			Vector3 extentRight = Quaternion.Euler(0, 0, angle) * ((GetComponent<CircleCollider2D>().bounds.extents.x) * transform.right);
			Vector2 direction = Quaternion.Euler(0,0,angle) * transform.up;

			RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - extentRight, direction, Mathf.Infinity, GameManager.staticEnemySightLayerMask.value);
			RaycastHit2D hitRight = Physics2D.Raycast(transform.position + extentRight, direction, Mathf.Infinity, GameManager.staticEnemySightLayerMask.value);

			if ((hitLeft.point - hitRight.point).magnitude < GetComponent<CircleCollider2D>().bounds.extents.x + 0.4)
			{
				if (hitLeft.distance > winningDistance)
				{
					winningPosition = (hitLeft.point + hitRight.point)/2;
					winningDistance = hitLeft.distance;
				}
			}
		}

		otherPatrolPosition = transform.position;
		goToPosition = winningPosition;
	}

	/*
	* Tells the enemy to navigate to the next patrol point, switches to the other patrol point when
	* it reaches the current one
	*/
	private void Patrol()
	{
		if ((transform.position - goToPosition).magnitude < PROXIMITY)
		{
			goToPosition = otherPatrolPosition;
			otherPatrolPosition = transform.position;
		}
		else
		{
			RotateToFacePosition(goToPosition);
			rigidBody.AddForce(transform.up * movementSpeed * Time.deltaTime);
		}
	}

	/*
	* Moves the enemy towards or away from the player depending on the player's
	* distance from the enemy
	*/
	private void AdjustDistanceFromPlayer()
	{
		RotateToFaceLastKnownPlayerPosition();
		Vector3 relativePlayerPosition = GetRelativePlayerPosition();
		if (relativePlayerPosition.magnitude - preferedDistanceRange > preferedDistance)
		{
			rigidBody.AddForce(transform.up * movementSpeed * Time.deltaTime);
		}
		else if (relativePlayerPosition.magnitude < preferedDistance - preferedDistanceRange)
		{ 
			rigidBody.AddForce(-transform.up * movementSpeed * Time.deltaTime);
		}
	}

	// Get the position of the player relative to the enemy
	private Vector3 GetRelativePlayerPosition()
	{
		return GameManager.GetPlayer().GetComponent<Transform>().position - transform.position;
	}

	private void RotateToFaceLastKnownPlayerPosition()
	{
		if (lastKnownPlayerPosition != null)
		{
			RotateToFacePosition(lastKnownPlayerPosition);
		}
	}

	// Rotate the enemy towards the position passed
	private void RotateToFacePosition(Vector3 position)
	{
		Vector3 relativePosition = position - transform.position;
		float angle = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg + 270;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	/*
	* Checks if the enemy can see the player and update the variable holding the last
	* known player position if so
	*/
	private void UpdateKnownPlayerPosition()
	{
		Vector3 playerPosition = GameManager.GetPlayer().transform.position;
		Vector3 relativePlayerPosition = GetRelativePlayerPosition();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePlayerPosition, Mathf.Infinity, GameManager.staticEnemySightLayerMask.value);
		if (hit.collider != null && hit.collider.gameObject.tag == Tags.PLAYER)
		{
			canSeePlayer = true;
			lastKnownPlayerPosition = playerPosition;
			goToPosition = playerPosition;
		}
		else
		{
			canSeePlayer = false;
		}
	}
}

