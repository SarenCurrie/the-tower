using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed;
	public int damage;
	public float killTime = 2.0f;

	public void SetForce(Vector2 f)
	{
		GetComponent<Rigidbody2D>().AddForce(f);
	}

	// Use this for initialization
	void Awake ()
    {
        GameObject.Destroy(this, killTime);
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
		if (collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
	}
}
