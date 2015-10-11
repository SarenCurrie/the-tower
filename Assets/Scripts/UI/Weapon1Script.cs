using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This class is used to manage the second weapon related elements for the Graphical User Interface HUD.
/// It is used to set and update the current sprites for the second weapon slot above the health meter
/// and highlight the weapon if it is currently selected/.
/// 
///  @author Harry
/// 
/// </summary>
public class Weapon1Script : MonoBehaviour {

    private Sprite[] sprites = new Sprite[2];
    private Image weapon;
	private bool showWindow = false;
	private RectTransform rect;
	private float startX;
	private float startY;
	private float popupHeight = 65;
	private float popupWidth = 150;

    public GUISkin mySkin;

    void Awake()
    {
        this.weapon = this.GetComponent<Image>();

    }
    void Start()
    {
		rect = this.GetComponent<RectTransform> ();
		startX = rect.anchoredPosition.x + rect.rect.width;
		startY = rect.anchoredPosition.y + popupHeight/2;
        loadWeaponSprites();
    }

    private void loadWeaponSprites()
    {
        Weapon weapon1 = null;
        if (GameManager.GetPlayer().GetComponent<Player>().weapons[0] != null)
        {
            weapon1 = GameManager.GetPlayer().GetComponent<Player>().weapons[0].GetComponent<Weapon>();
            if (weapon1.selectedSprite != null)
            {
                sprites[0] = weapon1.selectedSprite;
            }
            if (weapon1.unSelectedSprite != null)
            {
                sprites[1] = weapon1.unSelectedSprite;
            }
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

        loadWeaponSprites();
        checkWeapon();
    }

    private void checkWeapon()
    {
        int currentWeapon = GameManager.GetPlayer().GetComponent<Player>().currentWeapon;
        if (currentWeapon != null)
        {
            if (currentWeapon == 0)
            {
                if (sprites != null) { 
                    weapon.sprite = sprites[0];
                }
            }

            if (currentWeapon == 1)
            {
                if (sprites!=null)
                    weapon.sprite = sprites[1];
            }
        }

    }

    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
        GUI.skin = mySkin;
		if (showWindow) {
			Player player = GameManager.GetPlayer().GetComponent<Player>();
			Weapon weapon = player.weapons[0].GetComponent<Weapon>();
			float damageCurrent = weapon.damageMod;
			int spreadCurrent = weapon.spread;
			float forceCurrent = weapon.fireForce;
			GUI.TextField(new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y - popupHeight, popupWidth, popupHeight),"Damage:   " + System.Math.Round(damageCurrent, 2) + "\nSpread:       " + spreadCurrent + "\nForce:          " + forceCurrent + "\n","OutlineText");
        }
    }

}
