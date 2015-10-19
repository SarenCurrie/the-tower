using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This projectile is used by the first boss to spawn a rotator enemy  
/// 
/// </summary>
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
        GameObject r = Instantiate(rotator, transform.position, transform.rotation) as GameObject;
		r.transform.parent = GameManager.currentFloor.currentRoom.transform;
		Destroy(gameObject);
    }
}
