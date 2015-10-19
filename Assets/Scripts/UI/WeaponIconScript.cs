using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


/// <summary>
/// This class is used to manage the weapon related elements for the Graphical User Interface HUD.
/// It is used to set and update the current sprites for the weapon slot above the health meter
/// ,show details via popup and highlight the weapon if it is currently selected
/// 
/// 
/// </summary>
public class WeaponIconScript : MonoBehaviour {

	public int weaponNumber;

    private Sprite[] sprites = new Sprite[2];
    private Image weaponIcon;
	private bool showWindow = false;
	private RectTransform rect;
	private float startX;

	private GameObject myPopUp;

    public GUISkin mySkin;

    void Awake()
    {
		this.weaponIcon = this.GetComponent<Image>();
		weaponIcon.enabled = false;
		myPopUp = GameObject.Find("WeaponPopup"+weaponNumber.ToString());
    }

    void Start()
    {
		rect = this.GetComponent<RectTransform> ();
		startX = rect.anchoredPosition.x + rect.rect.width/2;
    }

    public void reloadWeaponSprites()
    {
        Weapon weapon1 = null;
		if (GameManager.GetPlayer().GetComponent<Player>().weapons[weaponNumber] != null)
        {
			weapon1 = GameManager.GetPlayer().GetComponent<Player>().weapons[weaponNumber].GetComponent<Weapon>();
            if (weapon1.selectedSprite != null)
            {
				this.gameObject.GetComponent<Image>().enabled = true;
                sprites[0] = weapon1.selectedSprite;
            }
            if (weapon1.unSelectedSprite != null)
            {
				this.gameObject.GetComponent<Image>().enabled = true;
                sprites[1] = weapon1.unSelectedSprite;
			}
			toggleWeapon();
        }
    }

    // Update is called once per frame
    void Update()
    {
		// Check we contain the mouse pointer
		if (RectTransformUtility.RectangleContainsScreenPoint (rect, Input.mousePosition)) {
			showWindow = true;
		} else {
			showWindow = false;
		}
    }

	// Toggles the weapon sprite for this weapon between equipped and not equipped
    public void toggleWeapon()
    {
        int currentWeapon = GameManager.GetPlayer().GetComponent<Player>().currentWeapon;
        if (currentWeapon != null)
        {
			if (currentWeapon == weaponNumber)
            {
                if (sprites[0]!=null) { 
					weaponIcon.sprite = sprites[0];
                }
            }
            else
            {
                if (sprites[1]!=null) {
					weaponIcon.sprite = sprites[1];
				}
            }
       }

    }

	// Moves the popup into, or out of, position
	private void togglePopup(bool show, string text)
	{
		Vector2 pos = myPopUp.GetComponent<RectTransform>().anchoredPosition;
		if (show) {
			pos.x = startX;

		} else {
			pos.x = Screen.width * 2;
		}
		myPopUp.GetComponent<RectTransform>().anchoredPosition = pos;
		myPopUp.GetComponentInChildren<Text>().text = text;
	}

    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
		GUI.matrix = UIController.GetGUIMatrix();
        GUI.skin = mySkin;
		if (showWindow) {
			Player player = GameManager.GetPlayer().GetComponent<Player>();
			if (player.weapons[weaponNumber] != null){
				Weapon weapon = player.weapons[weaponNumber].GetComponent<Weapon>();
                float damageCurrent = weapon.damageMod;
                float currentArc = weapon.spreadRange;
                int spreadCurrent = weapon.spread;
                float fireRateCurrent = weapon.fireFrequency;
				string currentMajor = weapon.weaponMajor.ToString();
				string currentMinor = weapon.weaponMinor.ToString();
                togglePopup(true, "Damage:   " + Math.Round(damageCurrent, 2) + "\nProjectiles:  " + spreadCurrent + "\nFire Rate:    " + Math.Round(fireRateCurrent, 2) + "\nMaj/Min:    " + currentMajor + "-" + currentMinor);
				//GUI.TextField(new Rect (Input.mousePosition.x, Screen.height - Event.current.mousePosition.y - popupHeight * (popupHeight/Screen.height), popupWidth, popupHeight),"Damage:   " + System.Math.Round(damageCurrent, 2) + "\nSpread:       " + spreadCurrent + "\nForce:          " + forceCurrent + "\nMaj/Min:    " + currentMajor + "-" + currentMinor,"OutlineText");
			}
		} else {
			togglePopup(false,"");
		}
    }

}
