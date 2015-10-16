using UnityEngine;
using System.Collections;

/**
 * This projectile is used by the first boss to spawn a rotator enemy  
 */
public class SpawningShot : Projectile
{
    public GameObject rotator;

    
    void Start()
    {   //Sets the projectile to be destroyed after the kill time
        Destroy(gameObject, killTime);
    }
    //On collision the rotator will be instantiated and the projectile destroyed
    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(rotator, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
