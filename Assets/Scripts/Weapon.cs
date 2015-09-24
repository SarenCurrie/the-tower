using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public Transform projectilePrefab;
    public int spread;
    public int damage;
    public float fireForce;
    public float fireFrequency;

    private float lastFired = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire ()
    {
        if (Time.time > lastFired + 1 / fireFrequency)
        {
            Transform projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity) as Transform;
            projectile.GetComponent<Projectile>().SetForce(transform.right * fireForce);
            lastFired = Time.time;
        }
    }
}
