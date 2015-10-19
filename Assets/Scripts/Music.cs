using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This class represents the background music that is
/// played upon starting the game for both the main and boss
/// music.
//
/// </summary>
public class Music : MonoBehaviour
{
	private static Music instance = null;
	public static Music Instance {
		get { return instance; }
	}

	public AudioClip mainMusic;
	public AudioClip bossMusic;

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public void StartMainMusic()
	{
		GetComponent<AudioSource>().clip = mainMusic;
		GetComponent<AudioSource>().Play();
	}

	public void StartBossMusic()
	{
		GetComponent<AudioSource>().clip = bossMusic;
		GetComponent<AudioSource>().Play();
	}
}