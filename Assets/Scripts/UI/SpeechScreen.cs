﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechScreen : MonoBehaviour {

	private string text;
	private float waitTime;

	private bool shown = false;
	private float _MoveSpeed = 20f;

	private float dialogWidth;
	private float dialogOut;
	private float dialogIn;
	
	private RectTransform dialogArea;
	private Vector2 temp;

	// Singleton speech screen instance - assumed only assigned to one place
	private static SpeechScreen singleton;

	void Awake()
	{
		if (singleton == null) {
			singleton = this;
		}
		dialogArea = this.GetComponent<RectTransform>();
		dialogWidth = dialogArea.rect.width;
		dialogOut = Screen.width + 2 * dialogWidth;
		dialogIn = dialogArea.anchoredPosition.x;
	}
	
	private void Start()
	{
		temp = dialogArea.anchoredPosition;
		temp.x = dialogOut;
		dialogArea.anchoredPosition = temp;
	}

	// Static method to show dialog
	public static void ShowDialog(string text, float time)
	{
		// Ask singleton to show dialog
		singleton.Show(text, time);
	}
	
	public void Show(string text, float time)
	{
		GameObject.Find ("SpeechText").GetComponent<Text>().text = text;
		waitTime = time;
		shown = true;
	}

	public void Hide()
	{
		shown = false;
	}

	public void Update()
	{
		if(waitTime > 0)
			waitTime -= Time.deltaTime;
		
		if (!shown && waitTime > 0)
			waitTime = 0f;
		
		if(waitTime <= 0)
			shown = false;
		GUI.matrix = UIController.GetGUIMatrix();
		if (shown)
		{
			// Move player area
			temp = dialogArea.anchoredPosition;
			temp.x = Mathf.MoveTowards(dialogArea.anchoredPosition.x, dialogIn, _MoveSpeed);
			dialogArea.anchoredPosition = temp;
		}
		else
		{
			// Move player area
			temp = dialogArea.anchoredPosition;
			temp.x = Mathf.MoveTowards(dialogArea.anchoredPosition.x, dialogOut, _MoveSpeed);
			dialogArea.anchoredPosition = temp;
		}
	}

}