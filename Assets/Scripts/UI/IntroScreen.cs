using UnityEngine;

public class IntroScreen : MonoBehaviour {
		
	public string firstLevelName;

	public void StartGame()
	{
		Application.LoadLevel(firstLevelName);
	}

}