﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthManager : MonoBehaviour
{
	//An integer to advance frames
	private int frameCounter = 0;	
	private float delay = 0.04f;
	private int t=85;
	private Object[] objects;
	private Sprite[] sprites;
	private Image mySprite;
	private float health = 100f;
	
	void Awake(){
		// store all sprites
		this.mySprite = this.GetComponent<Image>();
	}

	void Start ()
	{
		//Load all textures found on the Sequence folder, that is placed inside the resources folder
		this.objects = Resources.LoadAll("Holographic/output/pulse", typeof(Sprite));
		
		//Initialize the array of sprites with the same size as the objects array
		this.sprites = new Sprite[objects.Length];
		
		//Cast each Object to Sprite and store the result inside the Sprites array
		for(int i=0; i < objects.Length;i++)
		{
			this.sprites[i] = (Sprite)this.objects[i];
		}

	}
	
	void Update ()
	{
		delay = .015f+health/4000f;


		StartCoroutine("PlayLoop",delay);

		mySprite.sprite = sprites[frameCounter];
		this.transform.Find("health_text").GetComponent<Text>().text=health.ToString();
        health = GameManager.GetPlayer().GetComponent<Health>().health;
    

	}
	
	//The following methods return a IEnumerator so they can be yielded:
	//A method to play the animation in a loop
	IEnumerator PlayLoop(float delay)
	{
		//Wait for the time defined at the delay parameter
		yield return new WaitForSeconds(delay);  
		
		//Advance one frame
		frameCounter = (++frameCounter)%sprites.Length;
		
		//Stop this coroutine
		StopCoroutine("PlayLoop");
	}  
	
	//A method to play the animation just once
	IEnumerator Play(float delay)
	{
		//Wait for the time defined at the delay parameter
		yield return new WaitForSeconds(delay);  
		
		//If the frame counter isn't at the last frame
		if(frameCounter < sprites.Length)
		{
			//Advance one frame
			++frameCounter;
		}
		
		//Stop this coroutine
		StopCoroutine("PlayLoop");
	} 
	
}