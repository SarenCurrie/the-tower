using UnityEngine;
using System.Collections;


/// <summary>
/// 
/// This class represents the projectiles (bullets) that are fired
/// from each weapon.
/// 
/// Has a specific speed and damage which can be set.
/// 
/// </summary>
public class Projectile : MonoBehaviour {

	public float speed;
	public float killTime = 2.0f;

	private float damage = 1.0f;

	public float GetDamage() {
		return damage;
	}

	public void SetDamage(float d) {
		damage = d;
	}

	public void SetForce(Vector2 f)
	{
		GetComponent<Rigidbody2D>().AddForce(f);
	}

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, killTime);
    }

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy(gameObject);
	}
}
