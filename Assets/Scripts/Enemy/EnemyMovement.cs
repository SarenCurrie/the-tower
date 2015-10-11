using UnityEngine;
using System.Collections;

public class EnemyMovement : Enemy
{

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;
	public int baseScore;

	private Vector3 lastKnownPlayerPosition;
	private bool hasSeenPlayer = false;
	private bool canSeePlayer = false;

	private const float DISTANCE_FROM_LAST_KNOWN_PLAYER_POSITION = 0.2f;

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
			MoveToLastKnownPlayerPosition();
		}
		else if (relativePlayerPosition.magnitude + preferedDistance < preferedDistanceRange)
		{
			MoveAwayFromPlayer();
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
		if ((transform.position - lastKnownPlayerPosition).magnitude > DISTANCE_FROM_LAST_KNOWN_PLAYER_POSITION)
			hasSeenPlayer = false;
		rigidBody.AddForce(transform.up * movementSpeed * Time.deltaTime);
	}

	private void FleePlayer()
	{

	}

	private void MoveAwayFromPlayer()
	{
		rigidBody.AddForce(transform.up * -1 * movementSpeed);
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
