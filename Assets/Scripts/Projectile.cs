using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public int damage;

	public void SetForce(Vector2 f)
	{
		GetComponent<Rigidbody2D>().AddForce(f);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		print("projectile collided");
		if (collision.gameObject.tag == "Enemy")
		{
			// Destroy projectile when hit
			print("enemy");
			var enemy = collision.gameObject.GetComponent<Enemy>();
			enemy.LoseHealth(damage);
			Destroy(gameObject);
		}
	}
}
