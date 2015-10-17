using UnityEngine;
using System.Collections;

/// <summary>
///
/// A separate entity for playing death (and possibly other) sounds
///
/// </summary>
public class AudioManager : MonoBehaviour {

	private static AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	public static AudioSource GetAudioSource()
	{
		return audioSource;
	}
}
