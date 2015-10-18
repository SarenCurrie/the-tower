using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using HighScores;

/// <summary>
///
/// This class handles the high score summary shown via the start menu,
/// including getting the high scores and keeping them up to date.
/// 
/// @Author Jacob
/// </summary>
public class HighScoreScreen : MonoBehaviour {
		
	private float _MoveSpeed = 20f;
	private HighScoreGetter getter;

	private float highScoreUp;
	private float highScoreDown;
	
	private RectTransform highScoreArea;
	private Vector2 temp;
	private Text indexText;
	private Text nameText;
	private Text scoreText;
	private Text backingText;

	void Awake()
	{
		getter = this.GetComponent<HighScoreGetter>();
		getter.setParent(this);
		backingText = GameObject.Find("Content").GetComponent<Text> ();
		indexText = GameObject.Find("Index").GetComponent<Text> ();
		nameText = GameObject.Find("Name").GetComponent<Text> ();
		scoreText = GameObject.Find("Score").GetComponent<Text> ();
		highScoreArea = this.GetComponent<RectTransform> ();
		highScoreUp = 2*Screen.height;
		highScoreDown = highScoreArea.anchoredPosition.y;
	}

	public void Back()
	{
		WelcomeScreenImage.currentScreen = WelcomeScreenImage.SCREEN.startmenu;
	}
	
	private void Start()
	{
		temp = highScoreArea.anchoredPosition;
		temp.y = highScoreUp;
		highScoreArea.anchoredPosition = temp;
		// Start refreshing the list every 10 seconds
		InvokeRepeating("Refresh", 0, 10f);
	}

	void Refresh() {
		getter.Refresh();
	}

	void Update() {
		if (WelcomeScreenImage.currentScreen == WelcomeScreenImage.SCREEN.highscores)
		{
			// Move high score area down
			temp = highScoreArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(highScoreArea.anchoredPosition.y, highScoreDown,_MoveSpeed);
			highScoreArea.anchoredPosition = temp;
		}
		else
		{
			// Move high score area up
			temp = highScoreArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(highScoreArea.anchoredPosition.y, highScoreUp,_MoveSpeed);
			highScoreArea.anchoredPosition = temp;
		}
	}

	public void UpdateHighScores(List<HighScore> scores){
		for (int i = 0; i < scores.Count; i++){
			if (i == 0){
				backingText.text ="\n";
				indexText.text = (i+1).ToString();
				nameText.text = scores[i].GetName();
				scoreText.text = scores[i].GetScore().ToString();
			} else {
				backingText.text = backingText.text+"\n";
				indexText.text = indexText.text + "\n"+(i+1).ToString();
				nameText.text = nameText.text + "\n"+scores[i].GetName();
				scoreText.text = scoreText.text + "\n"+scores[i].GetScore().ToString();
			}
		}
	}
}

