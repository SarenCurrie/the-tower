using UnityEngine;
using System.Collections;

public class BossGun : BossBehaviour
{
	private const float BASE_HIT_DAMAGE = 0.01f;

	public Transform projectilePrefab;

	public float fireForce;
	public float fireFrequency;

	public float DAMAGEMULTIPLIER;

	protected float lastFired = 0;

	public float burstTime;

	public float fireWait;

	public float damage;

	//Used for burst fire and reload times
	private float weaponTime;

	private float nextFireTime = 0;
	private float fireStopTime = 0;

	private bool waitingToFire = false;
	public Sprite projectileSprite;

	void Update()
	{
		MaybeFireAtPlayer();
	}

	private void MaybeFireAtPlayer()
	{
		if (!waitingToFire)
		{
			if (weaponTime > fireStopTime)
			{
				Reload();
			}
			else
			{
				Fire(damage);
			}
		}
		else if (weaponTime > nextFireTime)
		{
			fireStopTime = burstTime;
			weaponTime = 0;
			waitingToFire = false;
		}
		weaponTime += Time.deltaTime;
	}

	private void Fire(float damage)
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
		}
	}

	protected void Reload()
	{
		nextFireTime = fireWait;
		weaponTime = 0;
		waitingToFire = true;
	}
}
