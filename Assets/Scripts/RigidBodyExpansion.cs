using UnityEngine;
using System.Collections;

/// <summary>
///
/// An expansion class that makes the GameObject it is added to respond to explosions
///
/// </summary>
public class RigidBodyExpansion : MonoBehaviour
{

	public float explosionForceModifier;

	/**
	* Creates an explosion at the specified position.
	* explosionForce - the force the explosion exerts on the object
	* explosionPosition - the center of the explosion
	* explosionRadius - distance from explosionPosition that the explosion has an impact
	* damage - how much damage the explosion does
	*/
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
