using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {

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
        //if (type < 75)
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

            // Stop the sprite rendering
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

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

        // Set the onFloor flag
        onFloor = true;
    }

    // Get the player object
    public Player GetPlayer()
    {
        return GameObject.FindObjectOfType<Player>();
    }

}
