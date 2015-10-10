using UnityEngine;
using System;
using System.Collections;

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
    public GameObject[] bloodPrefabs;
    public Boolean shouldDrop;
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

        health -= val;
        if (health > maxHealth)
        {
            // Cannot excede max health
            health = maxHealth;
        }
        else if (health < 0)
        {
            Die();
        }

		MakeBlood(BLOOD_SPATTER);
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
	 * Destroys the gameObject this script is attached to.
	 *
	 * In the future, this can be used for scoring/loot etc by overriding
     * this method with the custom implementation for the gameObject.
	 */
    public void Die()
    {
		health = 0;

		//Make some blood
		if (bloodPrefabs.Length > 0)
		{
			for (int i = 0; i < UnityEngine.Random.Range(MIN_BLOOD_ON_DEATH, MAX_BLOOD_ON_DEATH); i++)
			{
				MakeBlood(BLOOD_SPATTER_DEATH);
			}
		}

		string tag = gameObject.tag;
        //Increment the player score upon killing an enemy;
        if (tag.Equals("Enemy")) {
            int baseScore = gameObject.GetComponent<Enemy>().baseScore;
            baseScore += (int) GameManager.GetPlayer().GetComponent<UnitHealth>().health;
            GameManager.GetPlayer().GetComponent<Player>().score += baseScore;

            //Makes the dead thing drop an item
            if (shouldDrop)
            {
                GameObject a = Item.GenerateItem(gameObject.GetComponent<Transform>().position);
            }
		}
		else if (tag.Equals("Player"))
		{
			DeathMenu.dead = true;
		}

        Destroy(gameObject);
    }
}
