using UnityEngine;
using System.Collections;

public class BossBehaviour1 : BossBehaviour {

    private const float BASE_HIT_DAMAGE = 0.01f;

    public Transform projectilePrefab;
    public Transform explodingProjectilePrefab;

    public float fireForceCannon;
    public float fireForceSpread;

    public float fireFrequencyCannon;
    public float fireFrequencySpread;

    protected float damageMod;
    public float DAMAGEMULTIPLIER;

    private float lastFiredSpread = 0;
    private float lastFiredCannon = 0;

    public float burstTimeCannon;
    public float burstTimeSpread;

    public float fireWaitCannon;
    public float fireWaitSpread;

    public float spreadDamage;
    public float mainGunDamage;

    public int spread;
    public float spreadRange;

    //Used for burst fire and reload times
    private float weaponTime;
    private float cannonTime;

    private float nextFireTimeSpread = 0;
    private float nextFireTimeCannon = 0;

    private float spreadStopTime = 0;
    private float cannonStopTime = 0;

    private bool waitingToFireSpread = false;
    private bool waitingToFireCannon = false;

    public Sprite spreadProjectileSprite;
    public Sprite mainCannonProjectileSprite;
   
    public AudioClip cannonSound;
    public AudioClip spreadSound;

	void Update () {
        MaybeSpreadAtPlayer();
        MaybeCannonAtPlayer();
    }

    //Fire main gun
    public virtual void FireCannon(float damage)
    {
        if (Time.time > lastFiredCannon + 1 / fireFrequencyCannon)
        {
            Transform projectile = Instantiate(explodingProjectilePrefab, transform.position, transform.rotation) as Transform;
            projectile.GetComponent<SpriteRenderer>().sprite = mainCannonProjectileSprite;
            Transform projectileTransform = projectile.GetComponent<Transform>();
            projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForceCannon);
            projectile.GetComponent<Projectile>().SetDamage(damage);

            //Should be added as part of the current room
            projectile.parent = GameManager.currentFloor.currentRoom.transform;

            lastFiredCannon = Time.time;

            // Play Sound
            AudioSource source = GetComponent<AudioSource>();
            source.clip = cannonSound;
            source.Play();
        }
    }

    //Fire a spread
    public void FireSpread(float damage)
    {
        if (Time.time > lastFiredSpread + 1 / fireFrequencySpread)
        {
            for (int i = 0; i < spread; i++)
            {
                Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
                projectile.GetComponent<SpriteRenderer>().sprite = spreadProjectileSprite;
                Transform projectileTransform = projectile.GetComponent<Transform>();
                if (spread > 1)
                {
                    projectileTransform.Rotate(new Vector3(0, 0, -(spreadRange / 2) + i * (spreadRange / (spread - 1))));
                }
                projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForceSpread);
                projectile.GetComponent<Projectile>().SetDamage(damage);

                //Should be added as part of the current room
                projectile.parent = GameManager.currentFloor.currentRoom.transform;
            }
            lastFiredSpread = Time.time;
            AudioSource source = GetComponent<AudioSource>();
            source.clip = spreadSound;
            source.Play();
        }
    }

    protected void MaybeCannonAtPlayer()
    {       
            if (!waitingToFireCannon)
            {
                if (cannonTime >cannonStopTime)
                {
                    ReloadCannon();
                }
                else
                {
                FireCannon(mainGunDamage);
            }
            }
            else if (cannonTime > nextFireTimeCannon)
            {
                cannonStopTime = burstTimeCannon;
                cannonTime = 0;
                waitingToFireCannon = false;
            }
            cannonTime += Time.deltaTime;
     }

    protected void MaybeSpreadAtPlayer()
    {
        if (!waitingToFireSpread)
        {
            if (weaponTime > spreadStopTime)
            {
                ReloadSpread();
            }
            else
            {
                FireSpread(spreadDamage);
            }
        }
        else if (weaponTime > nextFireTimeSpread)
        {
            spreadStopTime = burstTimeSpread;
            weaponTime = 0;
            waitingToFireSpread = false;
        }
        weaponTime += Time.deltaTime;
    }

    private void ReloadCannon()
    {
        nextFireTimeCannon = fireWaitCannon;
        weaponTime = 0;
        waitingToFireCannon = true;
    }
    private void ReloadSpread()
    {
        nextFireTimeSpread = fireWaitSpread;
        weaponTime = 0;
        waitingToFireSpread = true;
    }
}
