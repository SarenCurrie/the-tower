using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementPopup : MonoBehaviour {

	public Sprite sprite;
	private string text;
	private float waitTime;

	private bool shown = false;
	private float _MoveSpeed = 20f;

	private float dialogWidth;
	private float dialogOut;
	private float dialogIn;
	
	private RectTransform dialogArea;
	private Vector2 temp;

	// Singleton speech screen instance - assumed only assigned to one place
	private static AchievementPopup singleton;

	void Awake()
	{
		if (singleton == null) {
			singleton = this;
		}
		dialogArea = this.GetComponent<RectTransform>();
		dialogWidth = dialogArea.rect.width;
		dialogOut = 2 * Screen.width;
		dialogIn = dialogArea.anchoredPosition.x;
	}
	
	private void Start()
	{
		temp = dialogArea.anchoredPosition;
		temp.x = dialogOut;
		dialogArea.anchoredPosition = temp;
	}

	// Static method to show dialog
	public static void ShowAchievement(string name,string text, float time)
	{
		// Ask singleton to show dialog
		singleton.Show(name, text, time);
	}
	
	public void Show(string name, string text, float time)
	{
		GameObject.Find ("AchievementName").GetComponent<Text>().text = name;
		GameObject.Find ("AchievementText").GetComponent<Text>().text = text;
		waitTime = time;
		shown = true;
	}

	public void Update()
	{
		if(waitTime > 0)
			waitTime -= Time.deltaTime;
		
		if (!shown && waitTime > 0)
			waitTime = 0f;
		
		if(waitTime <= 0)
			shown = false;
		GUI.matrix = UIController.GetGUIMatrix();
		if (shown)
		{
			// Move achievement area
			temp = dialogArea.anchoredPosition;
			temp.x = Mathf.MoveTowards(dialogArea.anchoredPosition.x, dialogIn, _MoveSpeed);
			dialogArea.anchoredPosition = temp;
		}
		else
		{
			// Move achievement area
			temp = dialogArea.anchoredPosition;
			temp.x = Mathf.MoveTowards(dialogArea.anchoredPosition.x, dialogOut, _MoveSpeed);
			dialogArea.anchoredPosition = temp;
		}
	}

}