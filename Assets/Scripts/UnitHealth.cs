using UnityEngine;
using System;
using System.Collections;
using Achievements;

/// <summary>
///
/// This class represents the Health behaviour that both enemies and the player
/// currently inherit this behaviour.
///
/// The class is responsible for decreasing the health of the entity as well
/// as causing the entity to die.
///
/// </summary>
public class UnitHealth : MonoBehaviour {

	public float maxHealth;
	public float health;
	public bool shouldDrop;

	/**
	 * This is is used as a "hack" to prevent the enemy from dying twice.
	 *
	 * This needed to be resolved in this manner as it seems the Destroy(GameObject, Float)
	 * method is broken within the current version of Unity. The first enemy that is killed
	 * is never destroyed when this method is called (until it is called for a second time.
	 */
	public bool isDead = false;

	public GameObject[] bloodPrefabs;
	public AudioClip deathSound;

	// Blood constants
	public const int MIN_BLOOD_ON_DEATH = 15;
	public const int MAX_BLOOD_ON_DEATH = 28;
	public const float BLOOD_SPATTER_DEATH = 2;
	public const float BLOOD_SPATTER = 0.2f;
	public const float BLOOD_SIZE_MIN = 0.5f;
	public const float BLOOD_SIZE_MAX = 1.2f;

	/**
	* This is an array of tags which specifies what the attached gameObject
	* will take damage from
	*
	* This needs to be modified in the unity editor on a per-prefab basis.
	*/
	public string[] damagedBy = { "Player", "Enemy", "PlayerProjectile", "EnemyProjectile" };

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	/**
	* Decreases the health by the specified float value.
	*/
	public void LoseHealth(float val)
	{
		if (gameObject.tag == Tags.PLAYER) {
			DamageFlash.flashDamage ();
		}

		//Make the blood first.
		if(!isDead)
			MakeBlood(BLOOD_SPATTER);

		if (health > maxHealth)
		{
			// Cannot excede max health
			health = maxHealth;
		}

		if (health - val > 0)
		{
			health -= val;
		}
		else
		{
			health = 0;
			Die();
		}
		UIController.GetUI().UpdateHealth();
	}

	/**
	* Returns the current health value.
	*/
	public float GetHealth()
	{
		return health;
	}

	/**
	* Called when something collides with the attached gameObject.
	* This will check what type of projectile has hit the gameObject
	* and then perform the required action.
	*/
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Projectile>() == null)
			return;


		int ind = Array.IndexOf(damagedBy, collision.gameObject.tag);
		if (ind > -1)
		{
			LoseHealth(collision.gameObject.GetComponent<Projectile>().GetDamage());
		}
	}

	private void MakeBlood(float spread)
	{
		// Spawn a blood object
		if (bloodPrefabs.Length > 0)
		{
			Vector3 rotation = Vector3.forward * UnityEngine.Random.Range(0f, 360f);
			GameObject bloodPrefab = bloodPrefabs[UnityEngine.Random.Range(0, bloodPrefabs.Length)];
			Vector3 bloodOSet = new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread));
			GameObject blood = Instantiate(bloodPrefab, gameObject.transform.position + bloodOSet, Quaternion.identity) as GameObject;
			blood.transform.parent = GameManager.currentFloor.currentRoom.transform;
			blood.transform.localScale *= UnityEngine.Random.Range(BLOOD_SIZE_MIN, BLOOD_SIZE_MAX);
			blood.transform.Rotate(rotation);
		}
	}

	/**
	 * Sets the health back to the maximum health.
	 */
	public void ResetHealth()
	{
		health = maxHealth;
	}

	/**
	* Destroys the gameObject this script is attached to.
	*
	* In the future, this can be used for scoring/loot etc by overriding
	* this method with the custom implementation for the gameObject.
	*/
	public void Die()
	{
		//Make some blood
		if (bloodPrefabs.Length > 0 && !isDead)
		{
			for (int i = 0; i < UnityEngine.Random.Range(MIN_BLOOD_ON_DEATH, MAX_BLOOD_ON_DEATH); i++)
			{
				MakeBlood(BLOOD_SPATTER_DEATH);
			}
		}

		isDead = true;

		string tag = gameObject.tag;
		//Increment the player score upon killing an enemy;
		if (tag.Equals("Enemy"))
		{
			// Add the enemies health to the players score
			UIController.GetUI().GetScoreManager().IncrementScore((int)gameObject.GetComponent<UnitHealth>().maxHealth);

			foreach (Enemy e in gameObject.GetComponents<Enemy>())
			{
				e.enabled = false;
				//Make the enemy disappear
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
			}

			if (shouldDrop)
			{
				GameObject a = Item.GenerateItem(gameObject.GetComponent<Transform>().position);

				//Prevents any enemy dropping twice on death.
				shouldDrop = false;
			}

			// log kill and score for achievemnts
			GameManager.achievementHandler.AddKill();
			GameManager.achievementHandler.AddScore();
		}
		else if (tag.Equals("Player"))
		{
			UIController.GetUI().showDeathMenu();
		}

		AudioSource audioSource = GetComponent<AudioSource>();
		if (audioSource != null)
		{
			Renderer renderer = GetComponent<Renderer>();
			audioSource.clip = deathSound;
			audioSource.Play();
			renderer.enabled = false;
			Destroy(gameObject, deathSound.length);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
