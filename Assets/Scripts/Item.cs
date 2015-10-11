using UnityEngine; 
using System.Collections;

/// <summary>
/// 
/// This class represents all Equippable items in the Tower,
/// including weapons and armour. These items are randomly generated
/// on the fly with a unique sprite, and unique properties (such
/// as spread and fire rate for weapons)
/// 
/// </summary>
public abstract class Item : MonoBehaviour {

    //Weapons are dropped 95% of the time
	private const int WEAPON_CHANCE = 0;
	private bool onFloor = true;

	public abstract void PickUp();
	public abstract void Generate();



	void Start()
	{
		Generate();
	}

	/**
	* Static method that returns a new item.
	*/
	public static GameObject GenerateItem(Vector3 v)
	{
        GameObject item;
        int type = Random.Range(0, 101);
        if (type < WEAPON_CHANCE)
        {
            return item = (Instantiate(Resources.Load("GroundGun"), v, Quaternion.identity)) as GameObject; 
        }
        else
        {
            return item = (Instantiate(Resources.Load("GroundArmour"), v, Quaternion.identity)) as GameObject; 
        }
	}

	/**
	* Static method that will definitely generate a weapon.
	*/
	public static GameObject GenerateWeapon(Vector3 v)
	{
		GameObject weapon = (Instantiate(Resources.Load("GroundGun"), v, Quaternion.identity)) as GameObject;

		return weapon;
	}

	/*
	*  Pick up the item it is on the ground and clicked
	*/
	void OnMouseDown()
	{
		if (onFloor)
		{
			PickUp();

			// Clear the onFloor flag
			onFloor = false;
		}
	}

	/*
	*  Put the item back on the ground
	*/
	public void ReturnToFloor()
	{
		// Start the sprite rendering again
		gameObject.GetComponent<SpriteRenderer>().enabled = true;

        //Re-enable the box collider
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

		gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Item_Dropped";

		//Stop following the player
		transform.parent = null;

		// Set the onFloor flag
		onFloor = true;

		// Add a small force to throw the weapon away
		Rigidbody2D rB = GetComponent<Rigidbody2D>();
		Transform itemTransform = GetComponent<Transform>();

		// Enable the Rigidbody
		rB.isKinematic = false;

		// Randomly throw the weapon to the left or the right
		int direction = UnityEngine.Random.Range(0, 2);
		int torque = UnityEngine.Random.Range(-40, 41);
		if(direction == 0){
			rB.AddForce((itemTransform.right)*300);
			rB.AddTorque(40);
		}else{
			rB.AddForce((itemTransform.right)*-300);
			rB.AddTorque(40);
		}
	}

	// Get the player object
	public Player GetPlayer()
	{
		return GameObject.FindObjectOfType<Player>();
	}



}
