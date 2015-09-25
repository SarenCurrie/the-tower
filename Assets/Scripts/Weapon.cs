﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

	private const float BASE_HIT_DAMAGE = 0.01f;

    public Transform projectilePrefab;

    private int spread = 1;
	private float spreadRange;
	private float fireForce;
	private float fireFrequency;
	private float strengthModifier;
	private float dexterityModifier;
	private float intelligenceModifier;

	private float lastFired = 0;

	// Use this for initialization
	void Start () {
		GenerateWeapon();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public void Fire (Player p)
    {
        if (Time.time > lastFired + 1 / fireFrequency)
        {
			for (int i = 0; i < spread; i++)
			{
				Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
				Transform projectileTransform = projectile.GetComponent<Transform>();
				if (spread > 1)
				{
					projectileTransform.Rotate(new Vector3(0, 0, -(spreadRange / 2) + i * (spreadRange / (spread - 1))));
				}
				projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.right) * fireForce);
				projectile.GetComponent<Projectile>().SetDamage((CalculateDamage(p) / spread) + BASE_HIT_DAMAGE);
			}
			lastFired = Time.time;
		}
    }

	private float CalculateDamage (Player p)
	{
		return p.GetStrength() * strengthModifier +
			p.GetDexterity() * dexterityModifier +
			p.GetIntelligence() * intelligenceModifier;
	}

	public void GenerateWeapon()
	{
		if(Random.Range(1,5) == 1)
		{
			if (Random.Range(0, 100) == 50)
			{
				spread = 100;
            } else {
				spread = Random.Range(3, 10);
				if (spread % 2 == 0)
				{
					spread += 1;
				}
			}
		}
		else
		{
			spread = 1;
		}
		if (Random.Range(1, 101) < 98)
		{
			spreadRange = Random.Range(15, 91);
		}
		else
		{
			spreadRange = 360;
		}
		fireForce = Random.Range(90, 111);

		fireFrequency = Random.Range(1, 21);

		//Generate main attributes
		List<int> attributes = new List<int>(new int[] { 0, 1, 2 });
		float[] modifiers = new float[3] { 0, 0, 0 };

		int major = Random.Range(0, 3);
		float majorMod = Random.Range(0.5f, 0.85f);

		modifiers[attributes[major]] = majorMod;
		attributes.RemoveAt(major);
		int minor = Random.Range(0, 2);
		modifiers[attributes[minor]] = 1 - majorMod;

		strengthModifier = modifiers[0];
		dexterityModifier = modifiers[1];
		intelligenceModifier = modifiers[2];
	}
}
