using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This class represents all equippable armour in the game.
/// 
/// It declares the unique properties of a piece of armour, the main attributes are:
/// 
/// -strength - How much the armour improves the Player's strength stat by
/// -dexterity -  How much the armour improves the Player's dexterity stat by
/// -intelligence -  How much the armour improves the Player's intelligence stat by
/// 
/// 
/// </summary>
public class Armour : Item {

    // The number of points allocated between the different stats
    private const int STAT_POINTS = 3;

    private int strength;
    private int dexterity;
    private int intelligence;

    private string armourName;

    //The slot this armour is
    public SLOTS slot;

    private string type;

    //The different possible slots for an armour
    public enum SLOTS
    {
        helm = 0,
        chest = 1,
        gloves = 2,
        boots = 3
    }

    //The looks for different slots
    //TODO: In future these could be arrays with the potential looks
    public Sprite helm;
    public Sprite chest;
    public Sprite boots;
    public Sprite gloves;

    public GUISkin mySkin;


    /*
    *  Called when the GameObject is created, generates the armour piece
    */
    public override void Generate()
    {
        // Generate the slot the item will exist in
        // This might change to an enum at a later date
        slot = (SLOTS)(Random.Range(0, 4));
        switch(slot)
        {
            case SLOTS.helm:
                gameObject.GetComponent<SpriteRenderer>().sprite = helm;
                break;
            case SLOTS.chest:
                gameObject.GetComponent<SpriteRenderer>().sprite = chest;
                break;
            case SLOTS.gloves:
                gameObject.GetComponent<SpriteRenderer>().sprite = gloves;
                break;
            case SLOTS.boots:
                gameObject.GetComponent<SpriteRenderer>().sprite = boots;
                break;
        }

        // Assign the stat points
        for (int i = 0; i < STAT_POINTS; i++)
        {
            var stat = Random.Range(0, 3);

            switch (stat)
            {
                case 0:
                    strength++;
                    break;
                case 1:
                    dexterity++;
                    break;
                case 2:
                    intelligence++;
                    break;
            }
        }
    }

    /*
    *  Picks up a piece of armour and puts it into the correct slot
    */
    public override void PickUp()
    {

        // Stop the sprite rendering
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Disable the Rigidbody
        GetComponent<Rigidbody2D>().isKinematic = true;

        // Set the position of the armour to that of the player.
        transform.position = GetPlayer().transform.position;
        transform.rotation = GetPlayer().transform.rotation;
        transform.parent = GetPlayer().transform; //Weapon will follow the player.
        GetComponent<SpriteRenderer>().sortingLayerName = "Held_Weapon";

        GetPlayer().GetComponent<Player>().PickUpArmour(this);

    }



    public int GetStrength()
    {
        return strength;
    }

    public int GetDexterity()
    {
        return dexterity;
    }

    public int GetIntelligence()
    {
        return intelligence;
    }

    public static bool showWindow = false;
    void OnMouseEnter()
    {
        showWindow = true;
    }

    void OnMouseExit()
    {
        showWindow = false;
    }



    void DoWindow0(int windowID)
    {
        Player player = GetPlayer().GetComponent<Player>();

        Armour armourPiece = null;
        if (this.slot == SLOTS.helm)
        {
            armourPiece = player.helm.GetComponent<Armour>();
            type = "Helmet";
        }
        else if (this.slot == SLOTS.chest)
        {
            armourPiece = player.chest.GetComponent<Armour>();
            type = "Chest";
        }
        else if (this.slot == SLOTS.gloves)
        {
            armourPiece = player.gloves.GetComponent<Armour>();
            type = "Gloves";
        }
        else if (this.slot == SLOTS.boots)
        {
            armourPiece = player.boots.GetComponent<Armour>();
            type = "Boots";

        }

        int intelligenceFromArmour = armourPiece.intelligence;
        int strengthFromArmour = armourPiece.strength;
        int dexterityFromArmour = armourPiece.dexterity;

        GUILayout.TextField(type+":\nStrength: " + strengthFromArmour + "\nDexterity:  " + dexterityFromArmour + "\nIntelligence " + intelligenceFromArmour + "\n", "OutlineText");
    }

    void DoWindow1(int windowID)
    {

        Armour armourPiece = null;
        if (this.slot == SLOTS.helm)
        {
            type = "Helmet";
        }
        else if (this.slot == SLOTS.chest)
        {
            type = "Chest";
        }
        else if (this.slot == SLOTS.gloves)
        {
            type = "Gloves";
        }
        else if (this.slot == SLOTS.boots)
        {
            type = "Boots";

        }

        GUILayout.TextField(type + ":\nStrength: " + this.strength + "\nDexterity: " + this.dexterity + "\nIntelligence: " + this.intelligence + "\n", "OutlineText");


    }
    void OnGUI()
    {
        GUI.skin = mySkin;
        //GUI.skin.window = mySkin ;
        Texture2D texture = Resources.Load("Holographic/output/main/bg/bg") as Texture2D;
        //doWindow0 = GUI.Toggle(new Rect(10, 10, 100, 20), doWindow0, "Window 0");

        if (showWindow)
        {
            int offset = 100;
            //if (this.gameObject.name.Contains("Ground") || this.gameObject.name.Contains("Enemy") )
            //{

            GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow0, "Current armour:");

            GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow1, "Floor armour:");
            //}
        }
    }

}
