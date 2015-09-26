using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject weapon;

	public float preferedDistance;
	public float preferedDistanceRange;
	public float movementSpeed;

	public float maxHealth;
	private float health;

	public float damage;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		health = maxHealth;
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
		Fire();
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

	public float GetHealth () {
		return health;
	}

	/**
	 * Changes the enemies health.
	 */
	public void LoseHealth (float val) {
		health -= val;
		if (health > maxHealth) {
			// Cannot excede max health
			health = maxHealth;
		}
		else if (health <= 0) {
			// If dead, die.
			Die();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlayerProjectile")
		{
			print("Collision");
			health -= collision.gameObject.GetComponent<Projectile>().GetDamage();
			if (health > maxHealth)
			{
				// Cannot excede max health
				health = maxHealth;
			}
			else if (health <= 0)
			{
				Die();
			}
		}
	}

	/**
	 * Destroys the enemy.
	 *
	 * In the future, this can be used for scoring/loot etc.
	 */
	public void Die () {
		Destroy(gameObject);
	}
}
