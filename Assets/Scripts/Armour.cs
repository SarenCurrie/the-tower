using UnityEngine;
using System.Collections;

public class Armour : MonoBehaviour {

	// The number of points allocated between the different stats
	private const int STAT_POINTS = 3;

	private int strength;
	private int dexterity;
	private int intelligence;

	private string armourName;
	private int slot;

	private bool onFloor = true;

	// Use this for initialization
	void Start () {
		GenerateArmour();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	*  Called when the GameObject is created, generates the armour piece
	*/
	public void GenerateArmour() 
	{
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
	*  Pick up the armour if it is on the ground and clicked
	*/
	void OnMouseDown()
	{
		if(onFloor)
		{
			// Pass the object to the Player PickUpArmour method
			GetPlayer().PickUpArmour(this.gameObject);

			// Stop the sprite rendering
			GetComponent<SpriteRenderer>().enabled = false;

			// Clear the onFloor flag
			onFloor = false;
		}
	}

	/*
	*  Put the armour back on the ground
	*/
	public void ReturnToFloor()
	{
		// Start the sprite rendering again
		GetComponent<SpriteRenderer>().enabled = true;

		// Set the onFloor flag
		onFloor = true;
	}

	// Get the player object
	private Player GetPlayer()
	{
		return GameObject.FindObjectOfType<Player>();
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
