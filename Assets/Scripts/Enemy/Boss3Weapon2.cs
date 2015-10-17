using UnityEngine;
using System.Collections;

public class Boss3Weapon2 : Enemy
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

    //How much of a burst needs to be completed before the enemy will reload if it cannot see the player
    public float reloadFactor = 1;
    //Used for burst fire and reload times
    private float weaponTime;

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
        if (possibleSounds.Length > 0)
        {
            int soundIndex = UnityEngine.Random.Range(0, possibleSounds.Length);
            actualSound = possibleSounds[soundIndex];
        }
    }

    protected void MaybeFireAtPlayer()
    {
        if (CanSeePlayer())
        {
            if (!waitingToFire)
            {
                if (weaponTime > fireStopTime)
                {
                    Reload();
                }
                else
                {
                    Fire();
                }
            }
            else if (weaponTime > nextFireTime)
            {
                fireStopTime = CalculateFireStopTime();
                weaponTime = 0;
                waitingToFire = false;
            }
            weaponTime += Time.deltaTime;
        }
        else
        {
            if (waitingToFire)
                weaponTime += Time.deltaTime;
            else if (weaponTime > reloadFactor * ((minFireWait + maxFireWait) / 2))
                Reload();
        }
    }

    protected void Reload()
    {
        nextFireTime = Random.Range(minFireWait, maxFireWait);
        weaponTime = 0;
        waitingToFire = true;
    }

    protected virtual float CalculateFireStopTime()
    {
        return burstTime;
    }

    protected bool CanSeePlayer()
    {
        if (GameManager.GetPlayer() == null)
            return false;
        Vector3 playerPosition = GameManager.GetPlayer().transform.position;
        Vector3 relativePlayerPosition = playerPosition - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, relativePlayerPosition, Mathf.Infinity, GameManager.staticEnemySightLayerMask.value);
        if (hit.collider != null && hit.collider.gameObject.tag == Tags.PLAYER)
        {
            return true;
        }
        return false;
    }
}

