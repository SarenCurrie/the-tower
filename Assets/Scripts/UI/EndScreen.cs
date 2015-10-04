using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	public GUISkin mySkin;
	public string firstLevelName;
	
	private static float winHeight = 200f;
	private static float winWidth = 350f;
	private static float winTop = 0 - Screen.height;
	private static float winBottom = (Screen.height)/2;
	private Rect winRect = new Rect((Screen.width - winWidth)/2, winTop, winWidth, winHeight);
	private Image image;
	
	void WinArea(int windowID) 
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("VICTORY","Shortlabel");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.Label("", "Divider");
		GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Restart", "ShortButton")) {
			// Restart the game
			Application.LoadLevel(firstLevelName);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button ("Quit", "ShortButton")) {
			// Quit the game
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
	
	void OnGUI () {
		// Stop pausing from happening
		PauseMenu.canPause = false;
		GUI.skin = mySkin;
		// Create win window
		winRect = GUI.Window (3, winRect, WinArea, "");
		winRect.y = Mathf.MoveTowards(winRect.y, winBottom, 10);
	}
}
