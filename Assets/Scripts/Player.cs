using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// The base value of all stats
	private const int MIN_STAT = 1;

	// Player stats
	private int strength = 1;
	private int dexterity = 1;
	private int intelligence = 1;

    public GameObject weapon1;
    public GameObject weapon2;

    public GameObject helm;
    public GameObject chest;
    public GameObject gloves;
    public GameObject boots;

    public float movementSpeed = 20.0f;

	private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
    }

    /*
    *  Picks a weapon up off the ground and puts it in the correct weapon slot
    */
    void PickUpWeapon (GameObject w, int position)
    {
		if (position == 1)
		{
			weapon1 = w;
		}
		else
		{
			weapon2 = w;
		}
        w.transform.parent = transform;
    }

    /*
    *  Picks up a piece of armour and puts it into the correct slot
    */
    public void PickUpArmour (GameObject a)
    {
    	// Get the armour slot
    	int slot = a.GetComponent<Armour>().GetSlot();

    	// Switch on the slot
    	switch(slot)
    	{
    	case 0:
    		// helmet
    		if(helm != null)
    		{
    			helm.GetComponent<Armour>().ReturnToFloor();
    		}

    		helm = a;
    		break;
    	case 1:
    		// chest
    		if(chest != null)
    		{
    			chest.GetComponent<Armour>().ReturnToFloor();
    		}

    		chest = a;
    		break;
    	case 2:
    		// gloves
    		if(gloves != null)
    		{
    			gloves.GetComponent<Armour>().ReturnToFloor();
    		}

    		gloves = a;
    		break;
    	case 3:
    		// boots
    		if(boots != null)
    		{
    			boots.GetComponent<Armour>().ReturnToFloor();
    		}

    		boots = a;
    		break;
    	}

    	// Update player stats
    	UpdateStats();

    	// TODO if a piece of armour exists in the slot put it on the ground
    }

    /*
    *  Checks if any of the movement keys have been pressed and adds an appropriate force to the
    *  player model
    */
    void CheckForMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
			rigidBody.AddForce(Vector2.left * movementSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
			rigidBody.AddForce(Vector2.right * movementSpeed);
		}
        if (Input.GetKey(KeyCode.W))
        {
			rigidBody.AddForce(Vector2.up * movementSpeed);
		}
        if (Input.GetKey(KeyCode.S))
        {
			rigidBody.AddForce(Vector2.down * movementSpeed);
		}
    }

    /*
    *  Checks if the player is currently pointing towards the mouse and rotates them towards it
    *  if not.
    */
    void CheckForRotation()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg + 270; //TODO: fix rotation when we change player model.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /*
    *  Check if any of the weapon fire buttons have been pressed and fire the appropriate weapon
    *  if it exists
    */
    void CheckForFire()
    {
		if (Input.GetMouseButton (0) && weapon1 != null) {
			weapon1.GetComponent<Weapon> ().Fire (this);
		} else if (Input.GetMouseButton (1) && weapon2 != null) {
			weapon2.GetComponent<Weapon> ().Fire (this);
		}
    }

	// Update is called once per frame
	void Update () {
        CheckForMovement();
        CheckForRotation();
        CheckForFire();
	}

	/*
	*  Get the total strength of all armour pieces
	*/
	private int GetItemStrength()
	{
		int helmStrength = 0;
		int chestStrength = 0;
		int gloveStrength = 0;
		int bootStrength = 0;

		// If the armour piece exists get its strength stat
		if (helm != null)
		{
			helmStrength = helm.GetComponent<Armour>().GetStrength();
		}

		if (chest != null)
		{
			chestStrength = chest.GetComponent<Armour>().GetStrength();
		}

		if (gloves != null)
		{
			gloveStrength = gloves.GetComponent<Armour>().GetStrength();
		}

		if (boots != null)
		{
			bootStrength = boots.GetComponent<Armour>().GetStrength();
		}

		// Total all strength stats
		return helmStrength + chestStrength + gloveStrength + bootStrength;
	}

	/*
	*  Get the total dexterity of all armour pieces
	*/
	private int GetItemDexterity()
	{
		int helmDexterity = 0;
		int chestDexterity = 0;
		int gloveDexterity = 0;
		int bootDexterity = 0;

		// If the armour piece exists get its dexterity stat
		if (helm != null)
		{
			helmDexterity = helm.GetComponent<Armour>().GetDexterity();
		}

		if (chest != null)
		{
			chestDexterity = chest.GetComponent<Armour>().GetDexterity();
		}

		if (gloves != null)
		{
			gloveDexterity = gloves.GetComponent<Armour>().GetDexterity();
		}

		if (boots != null)
		{
			bootDexterity = boots.GetComponent<Armour>().GetDexterity();
		}

		// Total all dexterity stats
		return helmDexterity + chestDexterity + gloveDexterity + bootDexterity;
	}

	/*
	*  Get the total intelligence of all armour pieces
	*/
	private int GetItemIntelligence()
	{
		int helmIntelligence = 0;
		int chestIntelligence = 0;
		int gloveIntelligence = 0;
		int bootIntelligence = 0;

		// If the armour piece exists get its intelligence stat
		if (helm != null)
		{
			helmIntelligence = helm.GetComponent<Armour>().GetIntelligence();
		}

		if (chest != null)
		{
			chestIntelligence = chest.GetComponent<Armour>().GetIntelligence();
		}

		if (gloves != null)
		{
			gloveIntelligence = gloves.GetComponent<Armour>().GetIntelligence();
		}

		if (boots != null)
		{
			bootIntelligence = boots.GetComponent<Armour>().GetIntelligence();
		}

		// Total all intelligence stats
		return helmIntelligence + chestIntelligence + gloveIntelligence + bootIntelligence;
	}

	private void UpdateStats()
	{
		strength = GetItemStrength() + MIN_STAT;
		dexterity = GetItemDexterity() + MIN_STAT;
		intelligence = GetItemIntelligence() + MIN_STAT;
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
