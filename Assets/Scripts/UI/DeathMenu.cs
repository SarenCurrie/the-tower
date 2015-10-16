using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {
	
	public static bool visible = false;
	
	private float deathHeight;
	private RectTransform deathArea;
	private float deathUp;
	private float deathDown;
	private Vector2 temp;
	private float _MoveSpeed = 20f;
	private Text text;
	public bool setScore = false;
	
	void Awake()
	{
		text = GameObject.Find ("HighScoreText").GetComponent<Text>();
		deathArea = this.GetComponent<RectTransform>();
		deathHeight = deathArea.rect.width;
		deathUp = 2 * Screen.height;
		deathDown = deathArea.anchoredPosition.y;
	}
	
	void Start () {
		Vector2 outvect = deathArea.anchoredPosition;
		outvect.y = deathUp;
		deathArea.anchoredPosition = outvect;
		visible = false;
		setScore = false;
	}
	
	void Update() {;
		if (visible) {
			// Set score text if we haven't already
			if (!setScore){
				text.text = "Your final score was " + UIController.GetUI().GetScoreManager().score.ToString();
				setScore = true;
			}
			temp = deathArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(deathArea.anchoredPosition.y, deathDown, _MoveSpeed);
			deathArea.anchoredPosition = temp;
		} else {
			temp = deathArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(deathArea.anchoredPosition.y, deathUp, _MoveSpeed);
			deathArea.anchoredPosition = temp;
		}
	}
}
