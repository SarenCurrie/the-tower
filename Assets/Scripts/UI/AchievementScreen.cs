using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Achievements;

public class AchievementScreen : MonoBehaviour {
		
	private float _MoveSpeed = 20f;

	private float achievementUp;
	private float achievementDown;
	
	private RectTransform achievementArea;
	private Vector2 temp;
	private Text doneText;
	private Text nameText;
	private Text backingText;

	void Awake()
	{
		backingText = GameObject.Find("AchContent").GetComponent<Text> ();
		doneText = GameObject.Find("Done").GetComponent<Text> ();
		nameText = GameObject.Find("AchName").GetComponent<Text> ();
		achievementArea = this.GetComponent<RectTransform> ();
		achievementUp = 2*Screen.height;
		achievementDown = achievementArea.anchoredPosition.y;
	}

	public void Back()
	{
		WelcomeScreenImage.currentScreen = WelcomeScreenImage.SCREEN.startmenu;
	}
	
	private void Start()
	{
		temp = achievementArea.anchoredPosition;
		temp.y = achievementUp;
		achievementArea.anchoredPosition = temp;
	}

	void Update() {
		if (WelcomeScreenImage.currentScreen == WelcomeScreenImage.SCREEN.achievements)
		{
			// Move achievement area down
			temp = achievementArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(achievementArea.anchoredPosition.y, achievementDown,_MoveSpeed);
			achievementArea.anchoredPosition = temp;
		}
		else
		{
			// Move achievement area up
			temp = achievementArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(achievementArea.anchoredPosition.y, achievementUp,_MoveSpeed);
			achievementArea.anchoredPosition = temp;
		}
	}

	public void UpdateAchievments(List<Achievement> achievements){
		for (int i = 0; i < achievements.Count; i++){
			if (i == 0){
				backingText.text ="\n";
				if (achievements[i].hasAchieved){
					doneText.text = "✓";
				} else {
					doneText.text = "?";
				}
				nameText.text = achievements[i].name;
			} else {
				backingText.text = backingText.text + "\n";
				if (achievements[i].hasAchieved){
					doneText.text = "\n✓";
				} else {
					doneText.text = "\n?";
				}
				nameText.text = "\n" + achievements[i].name;
			}
		}
	}
}

