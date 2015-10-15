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
            weaponIndex = topDownLooks.Length;
        }
        topDownObject.GetComponent<Image>().sprite = sideLooks[weaponIndex];
        
    }

    public void nextGun()
    {
        print("CLICKED");
        weaponIndex++;
        weaponIndex = weaponIndex % (topDownLooks.Length);
        weaponObject.GetComponent<Image>().sprite = sideLooks[weaponIndex];
        topDownObject.GetComponent<Image>().sprite = topDownLooks[weaponIndex];

    }

    public void updateSprites()
    {
        weaponObject.GetComponent<Image>().sprite = sideLooks[weaponIndex];
        topDownObject.GetComponent<Image>().sprite = topDownLooks[weaponIndex];
    }
}
