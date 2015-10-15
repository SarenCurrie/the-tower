using UnityEngine;
using System.Collections;

public class RigidBodyExpansion : MonoBehaviour
{

	public float explosionForceModifier;

	public void Explosion(float explosionForce, Vector3 explosionPosition, float explosionRadius, float damage)
	{
		Vector3 dir = (gameObject.GetComponent<Rigidbody2D>().transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		if (wearoff > 0)
		{
			gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * explosionForce * wearoff * explosionForceModifier);
			gameObject.GetComponent<UnitHealth>().LoseHealth(damage * wearoff);
		}
	}

}
