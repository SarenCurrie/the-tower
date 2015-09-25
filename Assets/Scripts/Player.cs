using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject weapon;

	// Use this for initialization
	void Start () {

	}

	void PickUpWeapon (GameObject w)
	{
		weapon = w;
		weapon.transform.parent = transform;
	}

	public float speed;

	void FireWeapon()
	{
		weapon.GetComponent<Weapon>().Fire();
	}

	void CheckForMovement()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
	}

	void CheckForRotation()
	{
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
		lookPos = lookPos - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg + 270;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void CheckForFire()
	{
		if (Input.GetMouseButton(0))
		{
			FireWeapon();
		}
	}

	// Update is called once per frame
	void Update () {
		CheckForMovement();
		CheckForRotation();
		CheckForFire();
	}
}
