using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

	public GUISkin mySkin;
	public static bool dead = false;
	public string firstLevelName;
	public string startScreenName;

	private static float deathHeight = 200f;
	private static float deathWidth = 350f;
	private static float deathTop = 0 - Screen.height;
	private static float deathBottom = (Screen.height)/2;
	private Rect deathRect = new Rect((Screen.width - deathWidth)/2, deathTop, deathWidth, deathHeight);

	void DeathArea(int windowID) 
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("YOU HAVE DIED","Shortlabel");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label("", "Divider");
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Restart", "ShortButton")) {
			dead = false;
			PauseMenu.canPause = true;
			// Restart the game
			Application.LoadLevel(firstLevelName);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button ("Quit", "ShortButton")) {
			dead = false;
			PauseMenu.canPause = true;
			// Quit the game
			Application.LoadLevel(startScreenName);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
	}

	void Start () {}

	void Update () {}

	void OnGUI () {
		if (dead){
			// Stop pausing from happening
			PauseMenu.canPause = false;
			GUI.skin = mySkin;
			// Create death window
			deathRect = GUI.Window (3, deathRect, DeathArea, "");
			deathRect.y = Mathf.MoveTowards (deathRect.y, deathBottom, 10);
		}
	}
}
