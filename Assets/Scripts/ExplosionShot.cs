using UnityEngine;
using System.Collections;

public class ExplosionShot : Projectile {


    public Transform explosion;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {   
        Destroy(gameObject);
        Transform projectile = Instantiate(explosion, transform.position, transform.rotation) as Transform;
    }
}
