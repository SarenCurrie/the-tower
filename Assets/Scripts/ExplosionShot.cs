using UnityEngine;
using System.Collections;

public class ExplosionShot : Projectile {

	public float explosionForce;
	public float explosionRadius;
	public float damage;

    public GameObject explosion;
	
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		Explosion.CreateExplosion(explosion, explosionForce, explosionRadius, damage, gameObject.transform.position);
        Destroy(gameObject);
    }
}
