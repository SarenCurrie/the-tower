using UnityEngine;
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
		Fire((CalculateDamage(p) / spread) + BASE_HIT_DAMAGE);
    }

	public void Fire (float damage)
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
				projectile.GetComponent<Rigidbody2D>().AddForce((projectileTransform.up) * fireForce);
				projectile.GetComponent<Projectile>().SetDamage(damage);
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
		int spreadRand = Random.Range (1, 4);//1/4
		switch (spreadRand)
		{
		case 1:
			//Multiple projectiles
			spread = Random.Range (5, 10);
			fireForce = Random.Range(15, 25);
			fireFrequency = Random.Range(1f, 5);
			break;
		case 2:
			//Low fire rate single fire weapon
			spread = 1;
			fireForce = Random.Range(80, 100);
			fireFrequency = Random.Range(1f, 2.5f);
			break;
		case 3:
			//High fire rate  single fire weapon
			spread = 1;
			fireForce = Random.Range(20, 35);
			fireFrequency = Random.Range(15f, 30);
			break;
		}
		//Spread range generation
		spreadRange = Random.Range(15, 61);

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
