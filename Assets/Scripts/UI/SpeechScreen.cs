using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpeechScreen : MonoBehaviour {


	public GUISkin mySkin;
	public Texture image;

	// Singleton speech screen instance - assumed only assigned to one place
	private static SpeechScreen singleton;

	private static string dialogText = "";
	private static float waitTime = 1f;
	private static bool shown = false;
	
	private static Rect dialogRect;
	private static float dialogWidth = 200f;
	private static float dialogHeight = 600f;
	private static float dialogOut = Screen.width - dialogWidth + 10f;
	private static float dialogIn = Screen.width + 2*dialogWidth;

	// Static method to show dialog
	public static void ShowDialog(string text, float time)
	{
		// Ask singleton to show dialog
		singleton.DoDialog(text, time);
	}

	private void DoDialog(string text, float time)
	{
		dialogText = text;
		waitTime = time;
		this.StartCoroutine("Timeout");
	}

	IEnumerator Timeout()
	{
		shown = true;
		yield return new WaitForSeconds(waitTime);
		shown = false;
		//Stop this coroutine
		StopCoroutine("Timeout");
	}

	void DialogArea(int windowID) 
	{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal ();
		GUILayout.Box(image);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Box(dialogText,"OutlineText");
		GUILayout.EndHorizontal ();

		GUILayout.FlexibleSpace();

		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Done", "ShortButton")) {
			shown = false;
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	void Update () {
	}

	private void Start()
	{
		// Enfore singleton for dialog so we can use static ShowDialog
		if (singleton == null) {
			singleton = this;
		}
		dialogRect = new Rect(Screen.width, (Screen.height - dialogHeight)/2, dialogWidth, dialogHeight);
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.skin.textArea.wordWrap = true;
		dialogRect = GUI.Window (100, dialogRect, DialogArea, "");
		// If shown, slide out
        if (shown) {
			dialogRect.x = Mathf.MoveTowards(dialogRect.x, dialogOut, 10);
		} 
		// Otherwise 
		else {
			dialogRect.x = Mathf.MoveTowards (dialogRect.x, dialogIn, 10);
		}
	
	}

}