using UnityEngine;
using UnityEngine.UI;

public class CreditsScreen : MonoBehaviour {
		
	private float _MoveSpeed = 20f;

	private float creditsUp;
	private float creditsDown;
	
	private RectTransform creditsArea;
	private Vector2 temp;

	void Awake()
	{
		creditsArea = this.GetComponent<RectTransform> ();
		creditsUp = 2*Screen.height;
		creditsDown = creditsArea.anchoredPosition.y;
	}

	public void Back()
	{
		WelcomeScreenImage.currentScreen = WelcomeScreenImage.SCREEN.startmenu;
	}
	
	private void Start()
	{
		temp = creditsArea.anchoredPosition;
		temp.y = creditsUp;
		creditsArea.anchoredPosition = temp;
	}

	void Update() {
		if (WelcomeScreenImage.currentScreen == WelcomeScreenImage.SCREEN.credits)
		{
			// Move credits area down
			temp = creditsArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(creditsArea.anchoredPosition.y, creditsDown,_MoveSpeed);
			creditsArea.anchoredPosition = temp;
		}
		else
		{
			// Move credits area up
			temp = creditsArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(creditsArea.anchoredPosition.y, creditsUp,_MoveSpeed);
			creditsArea.anchoredPosition = temp;
		}
	}
}

