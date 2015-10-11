using UnityEngine;
using System.Collections;

public class RangedEnemy : Enemy
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

    public AudioClip[] possibleSounds;
    protected AudioClip actualSound;

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

			//Should be added as part of the current room
			projectile.parent = GameManager.currentFloor.currentRoom.transform;

            lastFired = Time.time;

            // Play Sound
            AudioSource source = GetComponent<AudioSource>();
            source.clip = actualSound;
            source.Play();
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

        // generate sound
        int soundIndex = UnityEngine.Random.Range(0, possibleSounds.Length);
        actualSound = possibleSounds[soundIndex];
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
