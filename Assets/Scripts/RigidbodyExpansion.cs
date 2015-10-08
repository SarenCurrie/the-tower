using UnityEngine;
using System.Collections;

public class RigidBodyExpansion : MonoBehaviour
{

	public float explosionForceModifier;

	public void Explosion(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		Vector3 dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		if (wearoff > 0) {
			body.AddForce(dir.normalized * explosionForce * wearoff * explosionForceModifier);
		}
	}

}
