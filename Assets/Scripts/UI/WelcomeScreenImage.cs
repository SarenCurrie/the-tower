using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WelcomeScreenImage : MonoBehaviour {
		
	public GUISkin mySkin;
	public string firstLevelName;
	public static bool continueEnabled = false;
	// Start rect above the screen
	private Rect buttonRect = new Rect(Screen.width/20, 0 - Screen.height, 280, 230);
	private bool onStartScreen = true;

	void ButtonArea(int windowID){
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("THE TOWER", "ShortLabel");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Begin", "ShortButton")) {
			// Start game here
			Application.LoadLevel(firstLevelName);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUI.enabled = continueEnabled;
		GUILayout.Button("Continue", "ShortButton");
		GUI.enabled = true;
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Quit", "ShortButton")) {
			// Exit the game
			Application.Quit();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
	}


	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
	}

	void OnGUI() {
		GUI.skin = mySkin;
		buttonRect = GUI.Window (1, buttonRect, ButtonArea, "");
		// Drop start box from top
		buttonRect.y = Mathf.MoveTowards(buttonRect.y,Screen.height/16,5);
	}

}