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

}
