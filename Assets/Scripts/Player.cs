using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject weapon1;
    public GameObject weapon2;

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
			weapon1.GetComponent<Weapon>().Fire();
		}
		else if (Input.GetMouseButton(1) && weapon2 != null)
		{
			weapon2.GetComponent<Weapon>().Fire();
		}
    }

	// Update is called once per frame
	void Update () {
        CheckForMovement();
        CheckForRotation();
        CheckForFire();
	}
}
