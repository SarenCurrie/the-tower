using UnityEngine;
using System.Collections;

public class DeathMenu {

	private bool visible = false;

	private const float deathHeight = 200f;
	private const float deathWidth = 350f;
	private float deathTop = 0 - Screen.height;
	private float deathBottom = (Screen.height-deathHeight)/2;

	private Rect deathRect;

	public DeathMenu()
	{
		deathRect = new Rect((Screen.width - deathWidth) / 2, deathTop, deathWidth, deathHeight);
	}

	public void UI()
	{
		if (visible)
		{
			GUI.skin = UIController.GetUI().GetGui();
			// Stop pausing from happening
			PauseMenu.canPause = false;

			// Create death window
			deathRect = GUI.Window(3, deathRect, DeathArea, "");
			deathRect.y = Mathf.MoveTowards(deathRect.y, deathBottom, 10);
		}
	}

	public void Show()
	{
		visible = true;
	}

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

		//Pressed Restart
		if (GUILayout.Button("Restart", "ShortButton")) {
			visible = false;
			PauseMenu.canPause = true;
			// Restart the game
			GameManager.Restart();
			Application.LoadLevel(UIController.GetUI().firstLevelName);
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button ("Quit", "ShortButton")) {
			visible = false;
			PauseMenu.canPause = true;
			// Quit the game
			GameManager.Restart();
			Application.LoadLevel(UIController.GetUI().startScreenName);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal ();
		
		GUILayout.EndVertical();
		GUILayout.Space(10);
		GUILayout.EndHorizontal();
	}
}
