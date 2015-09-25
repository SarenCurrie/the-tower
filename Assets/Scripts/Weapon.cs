using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	private const float BASE_HIT_DAMAGE = 0.01f;

    public Transform projectilePrefab;

    public int spread = 1;
	public float spreadRange;
    public float fireForce;
    public float fireFrequency;
	public float strengthModifier;
	public float dexterityModifier;
	public float intelligenceModifier;

	private float lastFired = 0;

	// Use this for initialization
	void Start () {
		GenerateWeapon();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Fire (Player p)
    {
        if (Time.time > lastFired + 1 / fireFrequency)
        {
			for (int i = 0; i < spread; i++)
			{
				Transform projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as Transform;
				Transform projectileTransform = projectile.GetComponent<Transform>();
				projectileTransform.Rotate(new Vector3(0, 0, -(spreadRange/2) + i*(spreadRange/(spread-1))));
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
		
	}
}
