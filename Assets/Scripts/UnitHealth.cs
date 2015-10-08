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
    public GameObject blood;

    // Blood constants
    public const int MIN_BLOOD_ON_DEATH = 50;
    public const int MAX_BLOOD_ON_DEATH = 100;
    public const float BLOOD_SPATTER_DEATH = 1;
    public const float BLOOD_SPATTER = 0.2f;

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
        health -= val;
        if (health > maxHealth)
        {
            // Cannot excede max health
            health = maxHealth;
        }
        else if (health < 0)
        {
            // If dead, die.
            health = 0;
            Die();
        }
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
        int ind = Array.IndexOf(damagedBy, collision.gameObject.tag);
        if (ind > -1)
        {
            health -= collision.gameObject.GetComponent<Projectile>().GetDamage();

            // Spawn a blood object
            if (blood != null) {
                Vector3 bloodOSet = new Vector3(UnityEngine.Random.Range(-BLOOD_SPATTER,BLOOD_SPATTER), UnityEngine.Random.Range(-BLOOD_SPATTER,BLOOD_SPATTER));
                Instantiate(blood, gameObject.transform.position + bloodOSet, gameObject.transform.rotation);
            }
            if (health > maxHealth)
            {
                // Cannot excede max health
                health = maxHealth;
            }
            else if (health < 0)
            {
                health = 0;
                if (blood != null)
                {
                    for (int i = 0; i < UnityEngine.Random.Range(MIN_BLOOD_ON_DEATH, MAX_BLOOD_ON_DEATH); i++)
                    {
                        Vector3 bloodOffset = new Vector3(UnityEngine.Random.Range(-BLOOD_SPATTER_DEATH,BLOOD_SPATTER_DEATH), UnityEngine.Random.Range(-BLOOD_SPATTER_DEATH,BLOOD_SPATTER_DEATH));
                        Instantiate(blood, gameObject.transform.position + bloodOffset, gameObject.transform.rotation);
                    }
                }
                Die();
            }
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
        string tag = gameObject.tag;
        health = 0;
        //Increment the player score upon killing an enemy;
        if (tag.Equals("Enemy")) {
            int baseScore = (int) gameObject.GetComponent<UnitHealth>().maxHealth;
            baseScore += (int) GameManager.GetPlayer().GetComponent<UnitHealth>().health;
            GameManager.GetPlayer().GetComponent<Player>().score += baseScore;
        }
        //Makes the dead thing drop an item
        GameObject a = Item.GenerateItem(gameObject.GetComponent<Transform>().position);
        Debug.Log("ENTITY:"+tag);
        if (tag.Equals("Player"))
        {
            DeathMenu.dead = true;

        }

        Destroy(gameObject);
    }
}
