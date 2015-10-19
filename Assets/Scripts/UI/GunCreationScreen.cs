using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// 
/// This class represents the screen where players are able to create and customise
/// their own weapon based on their personal gameplay preferences.
/// 
/// This includes choosing their weapon appearance, projectile, and weapon statistics.
/// 
/// @author Harry
/// 
/// </summary>
public class GunCreationScreen : MonoBehaviour {


    public Sprite[] topDownLooks;
    public Sprite[] selectedSideLooks;
    public Sprite[] unselectedSideLooks;

    public Sprite[] possibleProjectileSprites;

    private Sprite projectileSprite;
    private Sprite selectedSideLook;
    private Sprite unselectedSideLook;
    private Sprite selectedTopDownLook;

    private int weaponIndex=0;
    private int projectileIndex = 0;

    GameObject weaponObject;
    GameObject topDownObject;
    GameObject projectileObject;

    private int fireRate = 1;
    private int projectiles =1;
    private int spreadAngle = 0;
    private int fireForce = 15;

    private int majorModifier=0;
    private int minorModifier=1;

    private float damageMod = 56.17f;


    //Initialize defaults
    void Start () {
        topDownObject = GameObject.Find("topView");
        weaponObject = GameObject.Find("sideView");
        projectileObject = GameObject.Find("projectile");
        selectedTopDownLook = topDownLooks[0];
        selectedSideLook = selectedSideLooks[0];
        unselectedSideLook = unselectedSideLooks[0];
        projectileSprite = possibleProjectileSprites[0];
        majorModifier = 0;
        minorModifier = 1;

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void previousGun()
    {
        weaponIndex--;
        if (weaponIndex < 0)
        {
            weaponIndex = topDownLooks.Length-1;
        }
        updateSprites();

    }

    public void nextGun()
    {
        weaponIndex++;
        weaponIndex = weaponIndex % (topDownLooks.Length);
        updateSprites();

    }

    public void nextProjectile()
    {
        projectileIndex--;
        if (projectileIndex < 0)
        {
            projectileIndex = possibleProjectileSprites.Length - 1;
        }
        updateSprites();

    }

    public void previousProjectile()
    {
        projectileIndex++;
        projectileIndex = projectileIndex % (possibleProjectileSprites.Length);
        updateSprites();

    }
    //Called to updated the sprites chosen by the player to show on the screen
    public void updateSprites()
    {
        weaponObject.GetComponent<Image>().sprite = selectedSideLooks[weaponIndex];

        selectedSideLook = selectedSideLooks[weaponIndex];
        unselectedSideLook = unselectedSideLooks[weaponIndex];
        selectedTopDownLook = topDownLooks[weaponIndex];

        topDownObject.GetComponent<Image>().sprite = topDownLooks[weaponIndex];
        projectileObject.GetComponent<Image>().sprite = possibleProjectileSprites[projectileIndex];
        projectileSprite = possibleProjectileSprites[projectileIndex]; 

    }

    //Various update methods for checking what the user selects for each weapon stat.
    public void UpdateFireRate()
    {
        int value = (int)(GameObject.Find("FireRate").GetComponent<Slider>().value);
        fireRate = value;
        GameObject.Find("fireRatePoints").GetComponent<Text>().text = value.ToString();
        RecalculateDamage();
    }

    public void UpdateFireForce()
    {
        int value = (int)(GameObject.Find("FireForce").GetComponent<Slider>().value);
        fireForce = value;
        GameObject.Find("forcePoints").GetComponent<Text>().text = value.ToString();
        RecalculateDamage();
    }

    public void UpdateProjectiles()
    {
        int value = (int)(GameObject.Find("Projectiles").GetComponent<Slider>().value);
        projectiles = value;
        GameObject.Find("projectilePoints").GetComponent<Text>().text = value.ToString();
        RecalculateDamage();
    }

    public void UpdateSpreadAngle()
    {
        int value = (int)(GameObject.Find("SpreadAngle").GetComponent<Slider>().value);
        spreadAngle = value;
        GameObject.Find("spreadPoints").GetComponent<Text>().text = value.ToString()+"°";
        RecalculateDamage();
    }

    public void UpdateMajorModifier()
    {
        int value = (int)(GameObject.Find("MajorAttribute").GetComponent<Dropdown>().value);
        majorModifier = value;
    }

    public void UpdateMinorModifier()
    {
        
        int value = (int)(GameObject.Find("MinorAttribute").GetComponent<Dropdown>().value);
        minorModifier = value;
    }


    //Creates the weapon when the user presses the create weapon button
    public void CreateWeapon()
    {
        if (majorModifier == minorModifier)
        {
            GameObject.Find("MessageWarning").GetComponent<Text>().text = "Minor and Major stats cannot be the same.";
            return;

        }
        else
        {
            GameObject.Find("MessageWarning").GetComponent<Text>().text = "";
        }
        GameObject weapon = Item.GenerateWeapon(new Vector3(10000,10000,10000), true);
        weapon.GetComponent<Weapon>().GenerateCustom(projectileSprite, selectedSideLook, unselectedSideLook, selectedTopDownLook, fireRate, projectiles, spreadAngle,
        fireForce, damageMod, majorModifier, minorModifier);
        Application.LoadLevel("StartScreen");
    }

    // Calculates the damage of the weapon, balancing it with the other choices the player makes.
    public void RecalculateDamage()
    {
        damageMod = 1.0f+(float)(3f*(((50) + (System.Math.Pow(spreadAngle, 0.7f))) / (float)((((System.Math.Pow(fireRate, 1.1f))) * System.Math.Pow(projectiles, 1.1f))+ System.Math.Pow(fireForce, 0.2))));
        GameObject.Find("Damage").GetComponent<Text>().text = damageMod.ToString();

       

    }

    public void BackToMainScreen()
    {
		Application.LoadLevel ("StartScreen");
    }


}
