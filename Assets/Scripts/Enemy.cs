using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This class represents the generic enemies in the tower, and is responsible 
/// for the navigation of the enemy towards the player.
/// 
/// </summary>
public class Enemy : MonoBehaviour
{

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		RotateToFacePlayer();
		AdjustDistanceFromPlayer();
	}

	private void AdjustDistanceFromPlayer()
	{
		Vector3 relativePlayerPosition = GetRelativePlayerPosition();
		if (relativePlayerPosition.magnitude - preferedDistanceRange > preferedDistance)
		{
			MoveToPlayer();
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

	private void MoveToPlayer()
	{
		rigidBody.AddForce(transform.up * movementSpeed);
	}

	private void FleePlayer()
	{

	}

	private void MoveAwayFromPlayer()
	{
		rigidBody.AddForce(transform.up * -1 * movementSpeed);
	}

	private void RotateToFacePlayer()
	{
		Vector3 relativePlayerPos = GetRelativePlayerPosition();
		float angle = Mathf.Atan2(relativePlayerPos.y, relativePlayerPos.x) * Mathf.Rad2Deg + 270;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
