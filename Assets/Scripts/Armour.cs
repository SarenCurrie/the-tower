using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This class represents all equippable armour in the game.
/// 
/// It declares the unique properties of each Weapon, the main attributes include:
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
    private int slot;
    private int look;

    public Sprite[] looks;

    /*
    *  Called when the GameObject is created, generates the armour piece
    */
    public override void Generate()
    {
        //look = Random.Range(0, looks.Length);
        //GetComponent<SpriteRenderer>().sprite = looks[look];

        // Generate the slot the item will exist in
        // This might change to an enum at a later date
        slot = Random.Range(0, 4);

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

		switch (GetSlot())
        {
            case 0:
                // helmet
                if (GetPlayer().helm != null)
                {
                    GetPlayer().helm.GetComponent<Armour>().ReturnToFloor();
                }

                GetPlayer().helm = this.gameObject;
                break;
            case 1:
                // chest
                if (GetPlayer().chest != null)
                {
                    GetPlayer().chest.GetComponent<Armour>().ReturnToFloor();
                }

                GetPlayer().chest = this.gameObject;
                break;
            case 2:
                // gloves
                if (GetPlayer().gloves != null)
                {
                    GetPlayer().gloves.GetComponent<Armour>().ReturnToFloor();
                }

                GetPlayer().gloves = this.gameObject;
                break;
            case 3:
                // boots
                if (GetPlayer().boots != null)
                {
                    GetPlayer().boots.GetComponent<Armour>().ReturnToFloor();
                }

                GetPlayer().boots = this.gameObject;
                break;
        }

        // Update player stats
        GetPlayer().UpdateStats();
    }

    // Getters
    public int GetSlot()
    {
        return slot;
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
