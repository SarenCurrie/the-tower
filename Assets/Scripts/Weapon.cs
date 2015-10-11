
using UnityEngine;
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
public class Weapon : Item
{

    public enum WeaponStat { Str, Dex, Int };

    private const float BASE_HIT_DAMAGE = 0.01f;
    public const float SINGLE_SHOT_MULTIPLIER = 0.65f;
    public const float SPREAD_SHOT_MULTIPLIER = 1.1f;
    public const float FAST_SHOT_MULTIPLIER = 0.9f;

    public Transform projectilePrefab;

    public int spread = 1;
    public float spreadRange;
    public float fireForce;
    public float fireFrequency;//
    public float strengthModifier;//
    public float dexterityModifier;//
    public float intelligenceModifier;//
    public float damageMod;

    public WeaponStat weaponMajor;
    public WeaponStat weaponMinor;

    private float lastFired = 0;

    private int look;

    public Sprite[] looks;
    public Sprite[] selectedSideLooks;
    public Sprite[] unseletedSideLooks;

    public Sprite[] possibleProjectileSprites;
    private Sprite projectileSprite;

    public AudioClip[] possibleRifleSounds;
    public AudioClip[] possibleShotgunSounds;
    public AudioClip[] possibleSmgSounds;
    private AudioClip actualSound;

    public GUISkin mySkin;

    private UnityEngine.Object[] hoverElements;

    public Sprite selectedSprite;
    public Sprite unSelectedSprite;




    public void Fire(Player p)
    {
        Fire(CalculateDamage(p));
    }

    public void Fire(float damage)
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
            AudioSource source = GetComponent<AudioSource>();
            source.clip = actualSound;
            source.Play();
        }
    }

    private float CalculateDamage(Player p)
    {
        return ((p.GetStrength() * strengthModifier +
            p.GetDexterity() * dexterityModifier +
                p.GetIntelligence() * intelligenceModifier) * damageMod);
    }

    public override void Generate()
    {
        look = UnityEngine.Random.Range(0, looks.Length);
        if (selectedSideLooks.Length == 5 && unseletedSideLooks.Length == 5)
        {
            selectedSprite = selectedSideLooks[look];
            unSelectedSprite = unseletedSideLooks[look];
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = looks[look];

        int projectileSpriteIndex = UnityEngine.Random.Range(0, possibleProjectileSprites.Length);
        projectileSprite = possibleProjectileSprites[projectileSpriteIndex];

        int spreadRand = UnityEngine.Random.Range(1, 4);

        int randSound;
		switch (spreadRand)
		{
			case 1:
				//Multiple projectiles
				spread = UnityEngine.Random.Range(5, 10);
				fireForce = UnityEngine.Random.Range(15, 20);
				fireFrequency = UnityEngine.Random.Range(1f, 5);
				spreadRange = UnityEngine.Random.Range(15, 61);
				damageMod = SPREAD_SHOT_MULTIPLIER * (float)(((50) + (System.Math.Pow(spreadRange, 0.7f))) / (((System.Math.Pow(fireFrequency, 1.1f))) * System.Math.Pow(spread, 1.1f)));

				// generate sound
				if (possibleShotgunSounds.Length > 0)
				{
					randSound = UnityEngine.Random.Range(0, possibleShotgunSounds.Length);
					actualSound = possibleShotgunSounds[randSound];
				}
				break;
			case 2:
				//Low fire rate single fire weapon
				spread = 1;
				fireForce = UnityEngine.Random.Range(80, 100);
				fireFrequency = UnityEngine.Random.Range(1f, 2.5f);
				spreadRange = 1;
				damageMod = SINGLE_SHOT_MULTIPLIER * (float)((50 * 1.5) / (System.Math.Pow(fireFrequency, 1.5f)));

				// generate sound
				if(possibleRifleSounds.Length > 0)
				{
					randSound = UnityEngine.Random.Range(0, possibleRifleSounds.Length);
					actualSound = possibleRifleSounds[randSound];
				}
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

				// generate sound
				if (possibleSmgSounds.Length > 0)
				{
					randSound = UnityEngine.Random.Range(0, possibleSmgSounds.Length);
					actualSound = possibleSmgSounds[randSound];
				}
                break;
        }

        //Generate main attributes
        List<int> attributes = new List<int>(new int[] { 0, 1, 2 });
        float[] modifiers = new float[3] { 0, 0, 0 };

        int major = UnityEngine.Random.Range(0, 3);
        weaponMajor = (WeaponStat) major;
        float majorMod = UnityEngine.Random.Range(0.5f, 0.85f);

        modifiers[attributes[major]] = majorMod;
       
        int minor = UnityEngine.Random.Range(0, 3);
        while (minor == major)
        {
            minor = UnityEngine.Random.Range(0, 3);
        }
        weaponMinor = (WeaponStat)minor;
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

        transform.localPosition = new Vector3(0.15f, 0.3f, 0);

        GetComponent<SpriteRenderer>().sortingLayerName = "Held_Weapon";

        GetPlayer().GetComponent<Player>().PickUpWeapon(this);
    }



    public bool showWindow = false;
    void OnMouseOver()
    {

        if (!showWindow && GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
            showWindow = true;
    }

    void OnMouseExit()
    {
        if(showWindow)
            showWindow = false;
    }


    //Creates current weapon textfield for popup comparison
    private void DoWindow0(int windowID)
    {
        Player player = GetPlayer().GetComponent<Player>();
        Weapon weapon = player.weapons[player.currentWeapon].GetComponent<Weapon>();
        float damageCurrent = weapon.damageMod;
        float currentArc = weapon.spreadRange;
        int spreadCurrent = weapon.spread;
        float fireRateCurrent = weapon.fireFrequency;
        WeaponStat currentMajor = weapon.weaponMajor;
        WeaponStat currentMinor = weapon.weaponMinor;




        GUILayout.TextField("Damage:   " + Math.Round(damageCurrent, 2) + "\nProjectiles:  " + spreadCurrent + "\nFire Rate:    " + Math.Round(fireRateCurrent, 2) + "\nMaj/Min:    " + currentMajor + "-" + currentMinor, "OutlineText");
    }

    //Creates ground weapon textfield for popup comparison
    private void DoWindow1(int windowID)
    {
        GUILayout.TextField("Damage:   " + Math.Round(damageMod, 2) + "\nProjectiles:  " + spread + "\nFire Rate:    " + Math.Round(fireFrequency, 2) + "\nMaj/Min:    " + weaponMajor + "-" + weaponMinor, "OutlineText");
    }

    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
        GUI.skin = mySkin;
        //Loads the textures being used for popup
        Texture2D texture = Resources.Load("Holographic/output/main/bg/bg") as Texture2D;
        
        

        if (showWindow)
        {
            Player player = GetPlayer().GetComponent<Player>();
            Weapon weapon = player.weapons[player.currentWeapon].GetComponent<Weapon>();
            //Gets the sidesprites for the popups
            Texture2D weapon1 = weapon.selectedSprite.texture as Texture2D;
            Texture2D weapon2 = selectedSprite.texture as Texture2D;
            int offset = 100;
            //Draws the textures being used for popup
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 120, Screen.height - Input.mousePosition.y - 45, 70, 40), weapon1);
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 45, 70, 40), weapon2);
            //Generates new Window for the current weapon and floor weapon stats 

            if (Input.mousePosition.y <= (Screen.height / 3))
            {
                GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y - 160 - offset, 250, 200), DoWindow0, "Current weapon:");
                GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y - 160 - offset, 250, 200), DoWindow1, "Floor weapon:");
            }
            else
            {
                GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow0, "Current weapon:");
                GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow1, "Floor weapon:");
            }
            
            }
    }
}

