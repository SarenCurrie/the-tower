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
	private Image image;

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

	void Start() {
		image = this.GetComponent<Image> ();
		InvokeRepeating("Flicker", 0, 5f);
	}
	
	void Update() {
	}

	IEnumerator Flicker()
	{
		// Want to flicker image here.
		float alpha = Random.Range(0.2f, 0.8f);
		float wait = Random.Range(1.0f, 2.0f);
		image.CrossFadeAlpha(alpha, 0.5f, false);

		yield return new WaitForSeconds(wait);
	}

	void OnGUI() {
		GUI.skin = mySkin;
		buttonRect = GUI.Window (1, buttonRect, ButtonArea, "");
		// Drop start box from top
		buttonRect.y = Mathf.MoveTowards(buttonRect.y,Screen.height/16,5);
	}

}