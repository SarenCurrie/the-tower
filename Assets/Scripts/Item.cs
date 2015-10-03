using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {
    private const int WEAPON_CHANCE = 50;
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
        GameObject item = (Instantiate(Resources.Load("GroundGun"), v, Quaternion.identity)) as GameObject;
        int type = Random.Range(0, 100);
        //75% chance a dropped item will be a weapon
        //if (type < WEAPON_CHANCE)
        //{
            return item;
        //}
        //else
        //{
        //    item.GetComponent<Armour>().Generate();
        //    return item;
        //}
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

		//Stop following the player
		transform.parent = null;

        // Set the onFloor flag
        onFloor = true;

    }

    // Get the player object
    public Player GetPlayer()
    {
        return GameObject.FindObjectOfType<Player>();
    }

}
