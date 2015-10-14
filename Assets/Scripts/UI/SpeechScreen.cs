using UnityEngine;
using System.Collections;

public class SpeechScreen {

	private string dialogText;
	private float waitTime;

	private bool shown = false;
	
	private Rect dialogRect;
	private Rect dialogRectangle;
	private const float dialogWidth = 300f;
	private const float dialogHeight = 600f;
	private float dialogOut = Screen.width - dialogWidth - 30f;
	private float dialogIn = 2 * Screen.width;

	// Static method to show dialog
	public void Show(string text, float time)
	{
		dialogText = text;
		waitTime = time;
		shown = true;
	}

	public void Process()
	{
		if(waitTime > 0)
			waitTime -= Time.deltaTime;

		if (!shown && waitTime > 0)
			waitTime = 0f;

		if(waitTime <= 0)
			shown = false;
	}

	void DialogArea(int windowID) 
	{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal ();
		GUILayout.Box(UIController.GetUI().GetSpeechImage());
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

	public SpeechScreen()
	{
		dialogRect = new Rect(Screen.width, (Screen.height - dialogHeight)/2, dialogWidth, dialogHeight);
	}
	
	public void UI()
	{
		GUI.matrix = UIController.GetGUIMatrix();
		GUI.skin = UIController.GetUI().GetGui();
		GUI.skin.textArea.wordWrap = true;
		dialogRectangle = GUI.Window(100, dialogRect, DialogArea, "");
		if (shown)
		{
			dialogRectangle.x = Mathf.MoveTowards(dialogRectangle.x, dialogOut, 10);
		}
		else
		{
			dialogRectangle.x = Mathf.MoveTowards(dialogRectangle.x, dialogIn, 10);
		}
	}

}