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

	public void LoseHealth (int val) {
		health -= val;
		if (health > maxHealth) {
			health = maxHealth;
		}
		else if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
