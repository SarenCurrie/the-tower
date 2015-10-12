using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : Enemy
{

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;
	public int baseScore;

	private Vector3 lastKnownPlayerPosition;
	private bool hasSeenPlayer = false;
	private bool canSeePlayer = false;

	private const float DISTANCE_FROM_LAST_KNOWN_PLAYER_POSITION = 0.1f;

	private const float WALL_CLEARANCE = 0.2f;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GetPlayer() == null)
			return;

		UpdateKnownPlayerPosition();
		RotateToFaceLastKnownPlayerPosition();

		//If player has ever been seen
		if (hasSeenPlayer)
		{
			if (canSeePlayer)
				AdjustDistanceFromPlayer();
			else
				MoveToLastKnownPlayerPosition();
		}
	}

	private void AdjustDistanceFromPlayer()
	{
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

	private Player GetPlayer()
	{
		return GameObject.FindObjectOfType<Player>();
	}

	private Vector3 GetRelativePlayerPosition()
	{
		return GetPlayer().GetComponent<Transform>().position - transform.position;
	}

	private void MoveToLastKnownPlayerPosition()
	{
		//if ((transform.position - lastKnownPlayerPosition).magnitude > DISTANCE_FROM_LAST_KNOWN_PLAYER_POSITION)
		//	hasSeenPlayer = false;
		rigidBody.AddForce(ChooseMovementDirection() * movementSpeed * Time.deltaTime);
	}

	private Vector3 ChooseMovementDirection()
	{
		Vector3 playerPosition = lastKnownPlayerPosition;
		//Shoot raycast from both left and right sides of collider
		Vector3 extentRight = GetComponent<CircleCollider2D>().bounds.extents.x * transform.right;
		Vector3[] viewPositions = new Vector3[] { transform.position - (2 + WALL_CLEARANCE) * extentRight, transform.position + (2 + WALL_CLEARANCE) * extentRight };
		List<RaycastHit2D> hits = new List<RaycastHit2D>();

		//Can the destination be reached by going straight forward?
		bool canReach = true;

		foreach (Vector3 raycastPosition in viewPositions)
		{
			Vector3 relativePlayerPosition = playerPosition - raycastPosition;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePlayerPosition, relativePlayerPosition.magnitude, GameManager.staticEnemySightLayerMask.value);
			hits.Add(hit);
			if (hit && hit.collider.tag != Tags.PLAYER)
			{
				canReach = false;
			}
        }

		if (canReach == false)
		{
			for (int i=0; i<viewPositions.Length; i++)
			{
				//If the point can be reached from here, move to here
				if (!hits[i] || hits[i].collider.tag == Tags.PLAYER)
				{
					return (playerPosition - viewPositions[i]).normalized;
				}
            }
		}
		return transform.up;
    }

	private void RotateToFaceLastKnownPlayerPosition()
	{
		if (lastKnownPlayerPosition != null)
		{
			Vector3 relativeLastKnownPlayerPosition = lastKnownPlayerPosition - transform.position;
			float angle = Mathf.Atan2(relativeLastKnownPlayerPosition.y, relativeLastKnownPlayerPosition.x) * Mathf.Rad2Deg + 270;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	private void UpdateKnownPlayerPosition()
	{
		Vector3 playerPosition = GetPlayer().transform.position;
		Vector3 relativePlayerPosition = GetRelativePlayerPosition();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePlayerPosition, Mathf.Infinity, GameManager.staticEnemySightLayerMask.value);
		if (hit.collider != null && hit.collider.gameObject.tag == Tags.PLAYER)
		{
			hasSeenPlayer = true;
			canSeePlayer = true;
			lastKnownPlayerPosition = playerPosition;
		}
		else
		{
			canSeePlayer = false;
		}
	}
}

