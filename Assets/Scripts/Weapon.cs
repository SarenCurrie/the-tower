﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
///
/// This class represents all equippable weapons in the game.
///
/// It declares the unique properties of each Weapon, the main attributes include:
///
/// -spread - How much the weapon spreads
/// -fireForce - The force of the weapon projectiles
/// -fireFrequency - How often the weapon can fire a projectile
/// -damage - based on the Entity's statistics and the damageMod (damage modifier)
///
///
/// </summary>
/// 
public class Weapon : Item {

    private const float BASE_HIT_DAMAGE = 0.01f;
    public const float SINGLE_SHOT_MULTIPLIER = 0.65f;
    public const float SPREAD_SHOT_MULTIPLIER = 1.1f;
    public const float FAST_SHOT_MULTIPLIER = 0.9f;

    public Transform projectilePrefab;

    private int spread = 1;
    private float spreadRange;
    private float fireForce;
    private float fireFrequency;//
    private float strengthModifier;//
    private float dexterityModifier;//
    private float intelligenceModifier;//
    private float damageMod;

    private float lastFired = 0;

    private int look;

    public Sprite[] looks;

	public Sprite[] possibleProjectileSprites;
	private Sprite projectileSprite;

    public GUISkin mySkin;

    private UnityEngine.Object[] hoverElements;


    public void Fire (Player p)
    {
        Fire(CalculateDamage(p));
    }

    public void Fire (float damage)
    {
        if (Time.time > lastFired + 1 / fireFrequency)
        {
            for (int i = 0; i < spread; i++)
            {
                Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
				projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
                Transform projectileTransform = projectile.GetComponent<Transform>();
                if (spread > 1)
                {
                    projectileTransform.Rotate(new Vector3(0, 0, -(spreadRange / 2) + i * (spreadRange / (spread - 1))));
                }
                projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForce);
                projectile.GetComponent<Projectile>().SetDamage(damage);
            }
            lastFired = Time.time;
        }
    }

    private float CalculateDamage (Player p)
    {
        return ((p.GetStrength() * strengthModifier +
            p.GetDexterity() * dexterityModifier +
                p.GetIntelligence() * intelligenceModifier) * damageMod);
    }

	public override void Generate()
	{
		look = UnityEngine.Random.Range(0, looks.Length);
		gameObject.GetComponent<SpriteRenderer>().sprite = looks[look];

		int projectileSpriteIndex = UnityEngine.Random.Range(0, possibleProjectileSprites.Length);
		projectileSprite = possibleProjectileSprites[projectileSpriteIndex];

		int spreadRand = UnityEngine.Random.Range (1, 4);
		switch (spreadRand)
		{
			case 1:
				//Multiple projectiles
				spread = UnityEngine.Random.Range (5, 10);
				fireForce = UnityEngine.Random.Range(15, 20);
				fireFrequency = UnityEngine.Random.Range(1f, 5);
				spreadRange = UnityEngine.Random.Range(15, 61);
				damageMod = SPREAD_SHOT_MULTIPLIER * (float)(((50) + (System.Math.Pow(spreadRange, 0.7f))) / (((System.Math.Pow(fireFrequency, 1.1f))) * System.Math.Pow(spread, 1.1f)));
				break;
			case 2:
				//Low fire rate single fire weapon
				spread = 1;
				fireForce = UnityEngine.Random.Range(80, 100);
				fireFrequency = UnityEngine.Random.Range(1f, 2.5f);
				spreadRange = 1;
				damageMod = SINGLE_SHOT_MULTIPLIER * (float)((50 * 1.5) / (System.Math.Pow(fireFrequency, 1.5f)));
				break;
			case 3:
				//High fire rate  single fire weapon
				spread = 1;
				fireForce = UnityEngine.Random.Range(20, 35);
				fireFrequency = UnityEngine.Random.Range(15f, 30);
				spreadRange = 1;
				//(DPS_CONST * SOME_CONST) / ((MIN_FORCE * FORSE^FORCE_CONST) + MIN_FREQ * FREQ^FREQ_CONST))
				//lower damage for higher fire rate and/or faster bullet speed (total difference of roughly .3 of a second)
				damageMod = FAST_SHOT_MULTIPLIER * (float)((50 * 25) / (20 * (System.Math.Pow(fireForce, 0.2)) + 15 * (System.Math.Pow(fireFrequency, 1.2))));
				break;
		}

        //Generate main attributes
        List<int> attributes = new List<int>(new int[] { 0, 1, 2 });
        float[] modifiers = new float[3] { 0, 0, 0 };

        int major = UnityEngine.Random.Range(0, 3);
        float majorMod = UnityEngine.Random.Range(0.5f, 0.85f);

        modifiers[attributes[major]] = majorMod;
        attributes.RemoveAt(major);
        int minor = UnityEngine.Random.Range(0, 2);
        modifiers[attributes[minor]] = 1 - majorMod;

        strengthModifier = modifiers[0];
        dexterityModifier = modifiers[1];
        intelligenceModifier = modifiers[2];
    }

    /*
    *  Picks a weapon up off the ground and puts it in the correct weapon slot
    */
    public override void PickUp()
    {
        //disable the box collider
        GetComponent<BoxCollider2D>().enabled = false;

        // Disable the Rigidbody
        GetComponent<Rigidbody2D>().isKinematic = true;

        // Set the position of the weapon to that of the player.
        transform.position = GetPlayer().transform.position;
        transform.rotation = GetPlayer().transform.rotation;
        transform.parent = GetPlayer().transform; //Weapon will follow the player.
		GetComponent<SpriteRenderer>().sortingLayerName = "Held_Weapon";

		GetPlayer().GetComponent<Player>().PickUpWeapon(this);
	}

  

    public static bool showWindow = false;
    void OnMouseEnter()
    {
        showWindow = true;
    }

    void OnMouseExit()
    {
        showWindow = false;
    }


    
    void DoWindow0(int windowID)
    {
        Player player = GetPlayer().GetComponent<Player>();
        Weapon weapon = player.weapons[player.currentWeapon].GetComponent<Weapon>();
        float damageCurrent = weapon.damageMod;
        int spreadCurrent = weapon.spread;
        float forceCurrent = weapon.fireForce;

        GUILayout.TextField("Damage:   " + Math.Round(damageCurrent, 2) + "\nSpread:       " + spreadCurrent + "\nForce:          " + forceCurrent + "\n", "OutlineText");
    }

    void DoWindow1(int windowID)
    {
        Weapon weapon = this.gameObject.GetComponent<Weapon>();
        Debug.Log(weapon.damageMod);
        float damageFloor = weapon.damageMod;
        int spreadFloor = weapon.spread;
        float forceFloor = weapon.fireForce;

        GUILayout.TextField("Damage:   " + Math.Round(damageMod, 2) + "\nSpread:       " + spread + "\nForce:          " + fireForce + "\n", "OutlineText");
        

    }
    void OnGUI()
    {
        GUI.skin = mySkin;
        //GUI.skin.window = mySkin ;
        Texture2D texture = Resources.Load("Holographic/output/main/bg/bg") as Texture2D;
        //doWindow0 = GUI.Toggle(new Rect(10, 10, 100, 20), doWindow0, "Window 0");
        
        if (showWindow)
        {
            int offset = 100;
            //if (this.gameObject.name.Contains("Ground") || this.gameObject.name.Contains("Enemy") )
            //{

                GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
                GUI.DrawTexture(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
                GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow0, "Current weapon:");

                GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow1, "Floor weapon:");
            //}
        }
    }
}
    