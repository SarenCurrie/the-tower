using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GunCreationScreen : MonoBehaviour {


    public Sprite[] topDownLooks;
    public Sprite[] sideLooks;

    public Sprite[] possibleProjectileSprites;

    private Sprite projectileSprite;
    private Sprite selectedSideLook;
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

    private string majorModifier="Strength";
    private string minorModifier="Dexterity";



    void Start () {
        topDownObject = GameObject.Find("topView");
        weaponObject = GameObject.Find("sideView");
        projectileObject = GameObject.Find("projectile");

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

    public void updateSprites()
    {
        weaponObject.GetComponent<Image>().sprite = sideLooks[weaponIndex];
        topDownObject.GetComponent<Image>().sprite = topDownLooks[weaponIndex];
        projectileObject.GetComponent<Image>().sprite = possibleProjectileSprites[projectileIndex];

    }

    public void UpdateFireRate()
    {
        int value = (int)(GameObject.Find("FireRate").GetComponent<Slider>().value);
        fireRate = value;
        GameObject.Find("fireRatePoints").GetComponent<Text>().text = value.ToString();
    }

    public void UpdateProjectiles()
    {
        int value = (int)(GameObject.Find("Projectiles").GetComponent<Slider>().value);
        projectiles = value;
        GameObject.Find("projectilePoints").GetComponent<Text>().text = value.ToString();
    }

    public void UpdateSpreadAngle()
    {
        int value = (int)(GameObject.Find("SpreadAngle").GetComponent<Slider>().value);
        spreadAngle = value;
        GameObject.Find("spreadPoints").GetComponent<Text>().text = value.ToString()+"°";
    }

    public void UpdateMajorModifier()
    {
        string value = (string)(GameObject.Find("MajorAttribute").GetComponent<Dropdown>().ToString());
        majorModifier = value;
    }

    public void UpdateMinorModifier()
    {
        string value = (string)(GameObject.Find("MinorAttribute").GetComponent<Dropdown>().ToString());
        minorModifier = value;
    }


}
