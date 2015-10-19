using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is used to manage the first weapon related elements for the Graphical User Interface HUD.
/// It is used to set and update the current sprites for the first weapon slot above the health meter
/// and highlight the weapon if it is currently selected/.
/// 
/// 
/// </summary>
public class Weapon2Script : MonoBehaviour
{
    private Sprite[] sprites = new Sprite[2];
    private Image weapon;

    public GUISkin mySkin;

    void Awake()
    {
        this.weapon = this.GetComponent<Image>();
        this.gameObject.GetComponent<Image>().enabled = false;
    }
    void Start()
    {
        loadWeaponSprites();



    }

    private void loadWeaponSprites()
    {
        Weapon weapon1 = null;
        if (GameManager.GetPlayer().GetComponent<Player>().weapons[1] != null)
        {
            weapon1 = GameManager.GetPlayer().GetComponent<Player>().weapons[1].GetComponent<Weapon>();
            if (weapon1.selectedSprite != null)
            {
               this.gameObject.GetComponent<Image>().enabled = true;
               sprites[1] = weapon1.selectedSprite;
            }
            if (weapon1.unSelectedSprite != null)
            {
                this.gameObject.GetComponent<Image>().enabled = true;
                sprites[0] = weapon1.unSelectedSprite;
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        loadWeaponSprites();
        checkWeapon();

        //Vector3 weapon1Position = GetComponent<RectTransform>().transform.position;
        //int xtolerance = 75;
        //int ytolerance = 20;
        //int i = 0;
        //if ((Input.mousePosition.x < weapon1Position.x + xtolerance && Input.mousePosition.x > weapon1Position.x - xtolerance) && (Input.mousePosition.y < weapon1Position.y + ytolerance && Input.mousePosition.y > weapon1Position.x - ytolerance))
        // {
        //    print("HIT"+i);
        //        showWindow = true;
        //}
        //else
        //{
        //        showWindow = false;
        //}



    }

    private void checkWeapon()
    {
        int currentWeapon = GameManager.GetPlayer().GetComponent<Player>().currentWeapon;
        if (currentWeapon != null)
        {
            if (currentWeapon == 0)
            {
                if (sprites != null)
                {
                    weapon.sprite = sprites[0];
                }
            }

            if (currentWeapon == 1)
            {
                if (sprites != null)
                    weapon.sprite = sprites[1];
            }
        }

    }

    public bool showWindow = false;
    public void OnMouseEnter()
    {
        if (!showWindow)
            showWindow = true;
    }

    void OnMouseExit()
    {
        if (showWindow)
            showWindow = false;
    }


    //Creates current weapon textfield for popup comparison
    private void DoWindow0(int windowID)
    {
        Player player = GameManager.GetPlayer().GetComponent<Player>();
        Weapon weapon = player.weapons[1].GetComponent<Weapon>();
        float damageCurrent = weapon.damageMod;
        int spreadCurrent = weapon.spread;
        float forceCurrent = weapon.fireForce;

        GUILayout.TextField("Damage:   " + System.Math.Round(damageCurrent, 2) + "\nSpread:       " + spreadCurrent + "\nForce:          " + forceCurrent + "\n", "OutlineText");
    }


    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
        GUI.skin = mySkin;
        if (showWindow)
        {
            //Draws the textures being used for popup
            //Generates new Window for the current weapon and floor weapon stats 
            GUI.Window(0, new Rect(250, 200, 250, 200), DoWindow0, "Current weapon:");
        }
    }

}
