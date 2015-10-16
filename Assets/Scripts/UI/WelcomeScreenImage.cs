using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WelcomeScreenImage : MonoBehaviour {
		
	public string firstLevelName;
	//The screen we are on
	public static SCREEN currentScreen;
	private float _MoveSpeed = 20f;

	private float startMenuUp;
	private float startMenuDown;
	
	private RectTransform startArea;
	private Vector2 temp;

	public enum SCREEN
	{
		startmenu = 0,
		credits = 1,
		highscores = 2,
	}

	void Awake()
	{
		startArea = this.GetComponent<RectTransform> ();
		startMenuUp = 2*Screen.height;
		startMenuDown = startArea.anchoredPosition.y;
	}

	public void StartGame(bool godmode)
	{
		if (godmode) {

		} else {

		}
		Application.LoadLevel(firstLevelName);
	}

	public void doCredits()
	{
		currentScreen = SCREEN.credits;
	}

	public void doHighScores()
	{
		currentScreen = SCREEN.highscores;
	}
	
	private void Start()
	{
		temp = startArea.anchoredPosition;
		temp.y = startMenuUp;
		startArea.anchoredPosition = temp;
		currentScreen = SCREEN.startmenu;
	}
	
	void Update() {
		if (currentScreen == SCREEN.startmenu)
		{
			// Move player area down
			temp = startArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(startArea.anchoredPosition.y, startMenuDown,_MoveSpeed);
			startArea.anchoredPosition = temp;
		}
		else
		{
			// Move player area up
			temp = startArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(startArea.anchoredPosition.y, startMenuUp,_MoveSpeed);
			startArea.anchoredPosition = temp;
		}
	}

}