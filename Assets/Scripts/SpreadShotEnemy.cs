using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// This class represents an enemy that has a spread 
/// shot attack type.
/// 
/// </summary>
public class SpreadShotEnemy : MonoBehaviour
{

	private const float BASE_HIT_DAMAGE = 0.01f;

	public Transform projectilePrefab;

	private int spread = 1;
	private float spreadRange;
	private float fireForce;
	private float fireFrequency;
	private float damageMod;

	private float lastFired = 0;

	public float minBurstTime;
	public float maxBurstTime;

	public float minFireWait;
	public float maxFireWait;

	public float damage;

	private float nextFireTime = 0;
	private float fireStopTime = 0;
	private bool waitingToFire = false;

	public Sprite[] possibleProjectileSprites;
	private Sprite projectileSprite;

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

	public void Fire(float damage)
	{
		if (Time.time > lastFired + 1 / fireFrequency)
		{
			for (int i = 0; i < spread; i++)
			{
				Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
				projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
                Transform projectileTransform = projectile.GetComponent<Transform>();
                if (spread > 1)
                {
                    projectileTransform.Rotate(new Vector3(0, 0, -(spreadRange / 2) + i * (spreadRange / (spread - 1))));
                }
                projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForce);
                projectile.GetComponent<Projectile>().SetDamage(damage);
			}
			lastFired = Time.time;
		}
	}

	private float CalculateDamage()
	{
		return (1.0f * damageMod);
	}

	public void Generate()
	{
		//Multiple projectiles
		spread = UnityEngine.Random.Range(5, 10);
		fireForce = UnityEngine.Random.Range(15, 20);
		fireFrequency = UnityEngine.Random.Range(1f, 5);
		spreadRange = UnityEngine.Random.Range(15, 61);
		damageMod = (float)(
			((50) + (System.Math.Pow(spreadRange, 0.7f))) / 
			(((System.Math.Pow(fireFrequency, 1.1f))) * System.Math.Pow(spread, 1.1f)));
		int projectileSpriteIndex = UnityEngine.Random.Range(0, possibleProjectileSprites.Length);
		projectileSprite = possibleProjectileSprites[projectileSpriteIndex];
	}

	private void MaybeFireAtPlayer()
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
			fireStopTime = Time.time + Random.Range(minBurstTime, maxBurstTime);
			waitingToFire = false;
		}
	}
}
