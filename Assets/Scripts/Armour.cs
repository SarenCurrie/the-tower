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
}
