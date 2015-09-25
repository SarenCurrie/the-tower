using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject weapon1;
    public GameObject weapon2;

    public float speed = 6.0f;

    // Use this for initialization
    void Start () {
        
	}

    void PickUpWeapon (GameObject w, int position)
    {
        if (position == 1)
            weapon1 = w;
        else
            weapon2 = w;
        w.transform.parent = transform;
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
        float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void CheckForFire()
    {
		if (Input.GetMouseButton(0) && weapon1 != null)
			weapon1.GetComponent<Weapon>().Fire();
		else if (Input.GetMouseButton(1) && weapon2 != null)
			weapon2.GetComponent<Weapon>().Fire();
    }

	// Update is called once per frame
	void Update () {
        CheckForMovement();
        CheckForRotation();
        CheckForFire();
	}
}
