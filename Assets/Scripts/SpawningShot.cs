using UnityEngine;
using System.Collections;

public class SpawningShot : Projectile
{
    public GameObject rotator;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, killTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(rotator, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
