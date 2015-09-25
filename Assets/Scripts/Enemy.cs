using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int maxHealth;
	private int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {

	}

	/**
	 * Changes the enemies health.
	 */
	public void LoseHealth (int val) {
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
