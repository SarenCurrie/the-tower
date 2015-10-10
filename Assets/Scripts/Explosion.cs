using UnityEngine;
using System.Collections;

public class Explosion : Projectile {

    void OnCollisionEnter2D(Collision2D collision)
    {
    }
    void Start()
    {
        SetDamage(20);
        Destroy(gameObject, killTime);
    }
}
