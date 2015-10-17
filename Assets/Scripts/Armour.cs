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
public class Armour : Item
{

    // The number of points allocated between the different stats
    private const int STAT_POINTS = 3;

    private int strength;
    private int dexterity;
    private int intelligence;

    private string armourName;

    //The slot this armour is
    public SLOTS slot;

    public Sprite[] sideLooks;

    private Sprite sideSprite;

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
        switch (slot)
        {
            case SLOTS.helm:
                gameObject.GetComponent<SpriteRenderer>().sprite = helm;
                sideSprite = sideLooks[0];
                break;
            case SLOTS.chest:
                gameObject.GetComponent<SpriteRenderer>().sprite = chest;
                sideSprite = sideLooks[1];
                break;
            case SLOTS.gloves:
                gameObject.GetComponent<SpriteRenderer>().sprite = gloves;
                sideSprite = sideLooks[3];
                break;
            case SLOTS.boots:
                gameObject.GetComponent<SpriteRenderer>().sprite = boots;
                sideSprite = sideLooks[2];
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

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

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

    public bool showWindow = false;
    void OnMouseEnter()
    {
        if (!showWindow && GameManager.currentFloor.currentRoom.EnemiesLeft() == 0)
            showWindow = true;
    }

    void OnMouseExit()
    {
        if(showWindow)
            showWindow = false;
    }

    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
        GUI.skin = mySkin;
        //Loads the textures being used for popup
        Texture2D texture = Resources.Load("Holographic/output/main/bg/bg") as Texture2D;
        Texture2D window = Resources.Load("Holographic/controls/windowBorderless") as Texture2D;

        Armour armourForStat = null;
        int intelligenceFromArmour = 0;
        int strengthFromArmour =  0;
        int dexterityFromArmour = 0;
        

        if (showWindow)
        {
            Player player = GetPlayer().GetComponent<Player>();
            //Gets the sidesprites for the popups
            switch (slot)
            {
                case SLOTS.helm:
                    type = "Helmet";
                    if (player.helm != null)
                    {
                        armourForStat = player.helm.GetComponent<Armour>();
                    }
                    break;
                case SLOTS.chest:
                    type = "Chest";

                    if (player.chest != null)
                    {
                        armourForStat = player.chest.GetComponent<Armour>();
                    }
                    break;
                case SLOTS.boots:
                    type = "Boots";

                    if (player.boots != null)
                    {
                        armourForStat = player.boots.GetComponent<Armour>();
                    }
                    break;
                case SLOTS.gloves:
                    type = "Gloves";

                    if (player.gloves != null)
                    {
                        armourForStat = player.gloves.GetComponent<Armour>();
                    }
                    break;
            }
            Texture2D groundArmourPiece = sideSprite.texture as Texture2D;

            int offset = 100;
            //Draws the textures being used for popup
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 45, 70, 40), groundArmourPiece);

            if (armourForStat != null)
            {
                intelligenceFromArmour = armourForStat.intelligence;
                strengthFromArmour = armourForStat.strength;
                dexterityFromArmour = armourForStat.dexterity;

            }
            //Generates new Window for the current weapon and floor weapon stats 
            if (Input.mousePosition.y <= (Screen.height / 3))
            {
                GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 160, 150, 90), window);
                if (armourForStat != null)
                {
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 170, Screen.height - Input.mousePosition.y - 160, 150, 90), window);
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 120, Screen.height - Input.mousePosition.y - 45, 70, 40), groundArmourPiece);
                    GUI.TextField(new Rect(Input.mousePosition.x - 170, Screen.height - Input.mousePosition.y - 160, 150, 90), type + ":\nStrength: " + strengthFromArmour + "\nDexterity:  " + dexterityFromArmour + "\nIntelligence " + intelligenceFromArmour + "\n", "OutlineText");
                }
                    GUI.TextField(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 160, 150, 90), type + ":\nStrength: " + strength + "\nDexterity: " + dexterity + "\nIntelligence: " + intelligence + "\n", "OutlineText");
            }
            else
            {
                GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y + 120 - offset, 150, 90), window);
                if (armourForStat != null)
                {
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 170, Screen.height - Input.mousePosition.y + 120 - offset, 150, 90), window);
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
                    GUI.DrawTexture(new Rect(Input.mousePosition.x - 120, Screen.height - Input.mousePosition.y - 45, 70, 40), groundArmourPiece);

                    GUI.TextField(new Rect(Input.mousePosition.x - 170, Screen.height - Input.mousePosition.y + 120 - offset, 150, 90), type + ":\nStrength: " + strengthFromArmour + "\nDexterity:  " + dexterityFromArmour + "\nIntelligence " + intelligenceFromArmour + "\n", "OutlineText");
                }
                    GUI.TextField(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y + 120 - offset, 150, 90),type + ":\nStrength: " + strength + "\nDexterity: " + dexterity + "\nIntelligence: " + intelligence + "\n", "OutlineText");

            }
            
        }
    }

}
