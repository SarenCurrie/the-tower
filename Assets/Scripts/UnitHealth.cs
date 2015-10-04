﻿using UnityEngine;
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
        for (int i = 0; i < damagedBy.Length; i++)
        {
            if (collision.gameObject.tag == damagedBy[i])
            {
                health -= collision.gameObject.GetComponent<Projectile>().GetDamage();
                if (health > maxHealth)
                {
                    // Cannot excede max health
                    health = maxHealth;
                }
                else if (health < 0)
                {
                    health = 0;
                    Die();
                }
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
        health = 0;
        //Increment the player score upon killing an enemy;
        GameManager.GetPlayer().GetComponent<Player>().score+=100;
        //Makes the dead thing drop an item
        GameObject a = Item.GenerateItem(gameObject.GetComponent<Transform>().position);
        string tag = gameObject.tag;
        Debug.Log("ENTITY:"+tag);
        if (tag.Equals("Player"))
        {
            DeathMenu.dead = true;

        }

        Destroy(gameObject);

    }
}