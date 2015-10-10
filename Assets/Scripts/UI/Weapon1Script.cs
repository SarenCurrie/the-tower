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

    private Object[] objects;
    private Sprite[] sprites;
    private Image weapon;

    public GUISkin mySkin;

    void Awake()
    {
        this.weapon = this.GetComponent<Image>();


    }
    void Start()
    {
        this.objects = new Object[2];
        this.objects[0] = Resources.Load("Holographic/output/main/bg/Baxia_U", typeof(Sprite));
        this.objects[1] = Resources.Load("Holographic/output/main/bg/Baxia_S", typeof(Sprite));


        //Initialize the array of sprites with the same size as the objects array
        this.sprites = new Sprite[objects.Length];

        //Cast each Object to Sprite and store the result inside the Sprites array
        for (int i = 0; i < objects.Length; i++)
        {
            this.sprites[i] = (Sprite)this.objects[i];
        }
        weapon.sprite = sprites[0];


    }

    // Update is called once per frame
    void Update()
    {

        int currentWeapon = GameManager.GetPlayer().GetComponent<Player>().currentWeapon;
        if (currentWeapon == 0)
        {
            weapon.sprite = sprites[0];
        }

        if (currentWeapon == 1)
        {
            weapon.sprite = sprites[1];
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
        Weapon weapon = player.weapons[0].GetComponent<Weapon>();
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
            GUI.Window(0, new Rect( 250, 200, 250, 200), DoWindow0, "Current weapon:");
        }
    }

}
