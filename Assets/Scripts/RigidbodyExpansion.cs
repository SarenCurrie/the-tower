using UnityEngine;
using System.Collections;

public class RigidBodyExpansion : MonoBehaviour {

	public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		var dir = (body.transform.position - explosionPosition);
		float wearoff = 1 - (dir.magnitude / explosionRadius);
		body.AddForce(dir.normalized * explosionForce * wearoff);
	}

}
