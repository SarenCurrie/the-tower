using UnityEngine;
using System.Collections;

/// <summary>
///
/// Singleton that plays the music
///
/// </summary>
public class MusicSingleton : MonoBehaviour {
	private static MusicSingleton instance = null;
	public static MusicSingleton Instance {
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
