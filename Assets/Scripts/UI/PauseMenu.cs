using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GUISkin mySkin;
	public static bool paused = false;
	public static bool canPause = true;
	public string startScreenName;

	private static float pauseHeight = 300f;
	private static float pauseWidth = 280f;
	private static float pauseTop = 0 - Screen.height;
	private static float pauseBottom = (Screen.height - pauseHeight)/2;
	private Rect pauseRect = new Rect((Screen.width - pauseWidth)/2, pauseTop, pauseWidth, pauseHeight);

	void PauseArea(int windowID) 
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("PAUSED", "ShortLabel");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label("", "Divider");
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Resume", "ShortButton")) {
			// Resume the time
			paused = false;
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUI.enabled = false;
		GUILayout.Button("Video Options", "ShortButton");
		GUI.enabled = true;
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUI.enabled = false;
		GUILayout.Button("Game Options", "ShortButton");
		GUI.enabled = true;
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Quit", "ShortButton")) {
			paused = false;
			// Exit the game
			Application.LoadLevel(startScreenName);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
	}
	
	void Start () {
	}
	
	void Update() {;
		// On ESCAPE, if allowed, pause the game
		if (Input.GetKey(KeyCode.Escape) && canPause) {
			paused = true;
		}
		if (paused) {
			// Pause time
			Time.timeScale = 0.0f;
		} else {
			// Unpause the game.
			Time.timeScale = 1.0f;
		}
	}

	void OnGUI () {
		// Check pausing is enabled
		if (canPause) {
			GUI.skin = mySkin;
			// Create pause window
			pauseRect = GUI.Window (5, pauseRect, PauseArea, "");
			if (paused) {
				// Drop box from top
				pauseRect.y = Mathf.MoveTowards (pauseRect.y, pauseBottom, 10);
			} else {
				// Move back to top
				pauseRect.y = Mathf.MoveTowards (pauseRect.y, pauseTop, 10);
			}
		}
	}
}
