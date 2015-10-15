using UnityEngine;
using System.Collections;

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
