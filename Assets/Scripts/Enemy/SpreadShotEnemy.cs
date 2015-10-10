﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpreadShotEnemy : RangedEnemy
{
	public int spread;
	public float spreadRange;

	public float minBurstTime;
	public float maxBurstTime;

	void Start()
	{
		Generate();
	}

	void Update()
	{
		MaybeFireAtPlayer();
	}

	public override void Fire(float damage)
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
			AudioSource source = GetComponent<AudioSource>();
			source.clip = actualSound;
			source.Play();
		}
	}

	public override void Generate()
	{
		damageMod = (float)(
			((50) + (System.Math.Pow(spreadRange, 0.7f))) /
			(((System.Math.Pow(fireFrequency, 1.1f))) * System.Math.Pow(spread, 1.1f)));

		// generate projectile
		int projectileSpriteIndex = UnityEngine.Random.Range(0, possibleProjectileSprites.Length);
		projectileSprite = possibleProjectileSprites[projectileSpriteIndex];

		// generate sound
		int soundIndex = UnityEngine.Random.Range(0, possibleSounds.Length);
		actualSound = possibleSounds[soundIndex];
	}

    protected override float CalculateFireStopTime()
    {
        return Time.time + Random.Range(minBurstTime, maxBurstTime);
    }
}