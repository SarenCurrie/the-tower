using UnityEngine;
using System.Collections;
/// <summary>
/// 
/// This projectile is used by the first boss, and will explode on contact
/// 
/// </summary>
public class ExplosionShot : Projectile {

	public float explosionForce;
	public float explosionRadius;
	public float explosionDamage;

    public GameObject explosion;
	
    //Spawn an explosion and destroy the projectile on collision
    void OnCollisionEnter2D(Collision2D collision)
    {
		Explosion.CreateExplosion(explosion, explosionForce, explosionRadius, explosionDamage, gameObject.transform.position);
        Destroy(gameObject);
    }
}
