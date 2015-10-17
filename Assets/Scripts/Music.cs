using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
	public AudioClip mainMusic;
	public AudioClip bossMusic;

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