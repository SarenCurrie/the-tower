using UnityEngine;
using System.Collections;

/// <summary>
///
/// All enemy scripts extend this class, so that GetComponent<Enemy> can
/// be called on any enemy
///
/// </summary>
public class Enemy : MonoBehaviour {

	/**
	 * Called on each enemy script before the game object is destroyed
	 * designed to be overwritten in extending scripts.
	 */
	public virtual void Die() { }

}
