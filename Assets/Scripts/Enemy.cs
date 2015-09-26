using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float maxHealth;
	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {

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

	/**
	 * Destroys the enemy.
	 *
	 * In the future, this can be used for scoring/loot etc.
	 */
	public void Die () {
		Destroy(gameObject);
	}
}
