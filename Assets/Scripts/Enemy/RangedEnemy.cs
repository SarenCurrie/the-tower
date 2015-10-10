using UnityEngine;
using System.Collections;

public class RangedEnemy : MonoBehaviour
{
    private const float BASE_HIT_DAMAGE = 0.01f;

    public Transform projectilePrefab;

    public float fireForce;
    public float fireFrequency;
    protected float damageMod;
    public float DAMAGEMULTIPLIER;

    protected float lastFired = 0;

    public float burstTime;

    public float minFireWait;
    public float maxFireWait;

    public float damage;

    protected float nextFireTime = 0;
    protected float fireStopTime = 0;
    protected bool waitingToFire = false;

    public Sprite[] possibleProjectileSprites;
    protected Sprite projectileSprite;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        MaybeFireAtPlayer();
    }

    public void Fire()
    {
        Fire(CalculateDamage());
    }

    public virtual void Fire(float damage)
    {
        if (Time.time > lastFired + 1 / fireFrequency)
        {
            Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
            projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
            Transform projectileTransform = projectile.GetComponent<Transform>();
            projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForce);
            projectile.GetComponent<Projectile>().SetDamage(damage);
            lastFired = Time.time;
        }
    }

    private float CalculateDamage()
    {
        return (DAMAGEMULTIPLIER * damageMod);
    }

    public virtual void Generate()
    {
        damageMod = (float)((50 * 25) /
            (20 * (System.Math.Pow(fireForce, 0.2)) + 15 * (System.Math.Pow(fireFrequency, 1.2))));
        int projectileSpriteIndex = UnityEngine.Random.Range(0, possibleProjectileSprites.Length);
        projectileSprite = possibleProjectileSprites[projectileSpriteIndex];
    }

    protected void MaybeFireAtPlayer()
    {
        if (!waitingToFire)
        {
            if (Time.time > fireStopTime)
            {
                nextFireTime = Time.time + Random.Range(minFireWait, maxFireWait);
                waitingToFire = true;
            }
            else
            {
                Fire();
            }
        }
        else if (Time.time > nextFireTime)
        {
            fireStopTime = CalculateFireStopTime();
            waitingToFire = false;
        }
    }

    protected virtual float CalculateFireStopTime()
    {
        return Time.time + burstTime;
    }
}
