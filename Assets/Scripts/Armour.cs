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
                sideSprite = sideLooks[2];
                break;
            case SLOTS.boots:
                gameObject.GetComponent<SpriteRenderer>().sprite = boots;
                sideSprite = sideLooks[3];
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
        if(!showWindow)
            showWindow = true;
    }

    void OnMouseExit()
    {
        if(showWindow)
            showWindow = false;
    }


    //Creates current armour textfield for popup comparison
    private void DoWindow0(int windowID)
    {
        Player player = GetPlayer().GetComponent<Player>();

        Armour armourPiece = null;
        switch (slot)
        {
            case SLOTS.helm:
                if (player.helm != null)
                {
                    armourPiece = player.helm.GetComponent<Armour>();
                }
                type = "Helmet";
                break;
            case SLOTS.chest:
                if (player.chest != null)
                {
                    armourPiece = player.chest.GetComponent<Armour>();
                }
                type = "Chest";
                break;
            case SLOTS.gloves:
                if (player.gloves != null)
                {
                    armourPiece = player.gloves.GetComponent<Armour>();
                }
                type = "Gloves";
                break;
            case SLOTS.boots:
                if (player.boots != null)
                {
                    armourPiece = player.boots.GetComponent<Armour>();
                }
                type = "Boots";
                break;
        }

        if (armourPiece != null)
        {
            int intelligenceFromArmour = armourPiece.intelligence;
            int strengthFromArmour = armourPiece.strength;
            int dexterityFromArmour = armourPiece.dexterity;

            GUILayout.TextField(type + ":\nStrength: " + strengthFromArmour + "\nDexterity:  " + dexterityFromArmour + "\nIntelligence " + intelligenceFromArmour + "\n", "OutlineText");
        }
    }

    //Creates ground armour textfield for popup comparison
    private void DoWindow1(int windowID)
    {
        switch (slot)
        {
            case SLOTS.helm:
                type = "Helmet";
                break;
            case SLOTS.chest:
                type = "Chest";
                break;
            case SLOTS.gloves:
                type = "Gloves";
                break;
            case SLOTS.boots:
                type = "Boots";
                break;
        }

        GUILayout.TextField(type + ":\nStrength: " + strength + "\nDexterity: " + dexterity + "\nIntelligence: " + intelligence + "\n", "OutlineText");


    }
    //Called every frame to check if the on hover will open a comparison popup for the weaopn
    void OnGUI()
    {
        GUI.skin = mySkin;
        //Loads the textures being used for popup
        Texture2D texture = Resources.Load("Holographic/output/main/bg/bg") as Texture2D;
        Texture2D armourPiece=null;
        

        if (showWindow)
        {
            Player player = GetPlayer().GetComponent<Player>();
            switch (slot)
            {
                case SLOTS.helm:
                    if(player.helm!=null)
                    armourPiece = player.helm.GetComponent<Armour>().sideSprite.texture as Texture2D;
                    break;
                case SLOTS.chest:
                    if (player.chest != null)
                    armourPiece = player.chest.GetComponent<Armour>().sideSprite.texture as Texture2D;
                    break;
                case SLOTS.boots:
                    if (player.boots != null)
                    armourPiece = player.boots.GetComponent<Armour>().sideSprite.texture as Texture2D;
                    break;
                case SLOTS.gloves:
                    if (player.gloves != null)
                    armourPiece = player.gloves.GetComponent<Armour>().sideSprite.texture as Texture2D;
                    break;
            }
            Texture2D groundArmourPiece = sideSprite.texture as Texture2D;

            int offset = 100;
            //Draws the textures being used for popup
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 160, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - offset, 150, 150), texture);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 120, Screen.height - Input.mousePosition.y - 60, 70, 70), armourPiece);
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 60, 70, 70), groundArmourPiece);
            //Generates new Window for the current weapon and floor weapon stats 
            if (Input.mousePosition.y <= (Screen.height / 3))
            {
                GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y -160 - offset, 250, 200), DoWindow0, "Current armour:");
                GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y - 160 - offset, 250, 200), DoWindow1, "Floor armour:");
            }
            else
            {
                GUI.Window(0, new Rect(Input.mousePosition.x - 250, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow0, "Current armour:");
                GUI.Window(1, new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y + 120 - offset, 250, 200), DoWindow1, "Floor armour:");
            }
            
        }
    }

}
