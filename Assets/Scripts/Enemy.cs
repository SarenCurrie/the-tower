using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject weapon;

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;

	public float minBurstTime;
	public float maxBurstTime;

	public float minFireWait;
	public float maxFireWait;

	public float damage;

	private float nextFireTime = 0;
	private float fireStopTime = 0;
	private bool waitingToFire = false;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		RotateToFacePlayer();
		AdjustDistanceFromPlayer();
		MaybeFireAtPlayer();
	}

	private void AdjustDistanceFromPlayer ()
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

	private Player GetPlayer ()
	{
		return GameObject.FindObjectOfType<Player>();
    }

	private Vector3 GetRelativePlayerPosition ()
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

	private void MaybeFireAtPlayer()
	{
		if (!waitingToFire)
		{
			if (Time.time > fireStopTime)
			{
				nextFireTime = Time.time + Random.Range(minFireWait, maxFireWait);
				waitingToFire = true;
			} else {
				Fire();
			}
		} else if (Time.time > nextFireTime) {
			fireStopTime = Time.time + Random.Range(minBurstTime, maxBurstTime);
			waitingToFire = false;
		}
    }

	private void Fire()
	{
		weapon.GetComponent<Weapon>().Fire(damage);
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
