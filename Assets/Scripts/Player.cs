using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

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

    void CheckForRotation()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lookPos = lookPos - transform.position;
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg + 270; //TODO: fix rotation when we change player model.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void CheckForFire()
    {
		if (Input.GetMouseButton(0) && weapon1 != null)
		{
			weapon1.GetComponent<Weapon>().Fire(this);
		}
		else if (Input.GetMouseButton(1) && weapon2 != null)
		{
			weapon2.GetComponent<Weapon>().Fire(this);
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

		if (helm != null)
		{
			helmIntelligence = helm.GetComponent<Armour>().GetDexterity();
		}

		if (chest != null)
		{
			chestIntelligence = chest.GetComponent<Armour>().GetDexterity();
		}

		if (gloves != null)
		{
			gloveIntelligence = gloves.GetComponent<Armour>().GetDexterity();
		}

		if (boots != null)
		{
			bootIntelligence = boots.GetComponent<Armour>().GetDexterity();
		}

		return helmIntelligence + chestIntelligence + gloveIntelligence + bootIntelligence;
	}

	public int GetStrength()
	{
		return 1 + GetItemStrength();
	}

	public int GetDexterity()
	{
		return 1 + GetItemDexterity();
	}

	public int GetIntelligence()
	{
		return 1 + GetItemIntelligence();
	}
}
