using UnityEngine;
using System.Collections;

/// <summary>
///
/// Singleton that plays the music
///
/// </summary>
public class GameObjectSingleton : MonoBehaviour {
	private static GameObjectSingleton instance = null;
	public static GameObjectSingleton Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
