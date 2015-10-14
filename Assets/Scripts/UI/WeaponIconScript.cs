using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This class is used to manage the weapon related elements for the Graphical User Interface HUD.
/// It is used to set and update the current sprites for the second weapon slot above the health meter
/// and highlight the weapon if it is currently selected/.
/// 
///  @author Harry
///  @author Jacob
/// 
/// </summary>
public class WeaponIconScript : MonoBehaviour {

	public int weaponNumber;

    private Sprite[] sprites = new Sprite[2];
    private Image weaponIcon;
	private bool showWindow = false;
	private RectTransform rect;
	private float startX;
	private float startY;
	private float popupHeight = 90f;
	private float popupWidth = 170f;

    public GUISkin mySkin;

    void Awake()
    {
		this.weaponIcon = this.GetComponent<Image>();
		weaponIcon.enabled = false;
    }

    void Start()
    {
		rect = this.GetComponent<RectTransform> ();
		startX = rect.anchoredPosition.x + rect.rect.width;
		startY = rect.anchoredPosition.y + popupHeight/2;
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
        }
		toggleWeapon();
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
                if (sprites[1]!=null)
					weaponIcon.sprite = sprites[1];
            }
        }

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
				int spreadCurrent = weapon.spread;
				float forceCurrent = weapon.fireForce;
				string currentMajor = weapon.weaponMajor.ToString();
				string currentMinor = weapon.weaponMinor.ToString();
				GUI.TextField(new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y - popupHeight*(popupHeight/Screen.height), popupWidth, popupHeight),"Damage:   " + System.Math.Round(damageCurrent, 2) + "\nSpread:       " + spreadCurrent + "\nForce:          " + forceCurrent + "\nMaj/Min:    " + currentMajor + "-" + currentMinor,"OutlineText");
			}
		}
    }

}
