using UnityEngine;

/// <summary>
///
/// This class handles the intro screen shown after the player
/// starts the game via the start menu
/// 
/// </summary>
public class IntroScreen : MonoBehaviour {
		
	public string firstLevelName;

	public void StartGame()
	{
		Application.LoadLevel(firstLevelName);
	}

}