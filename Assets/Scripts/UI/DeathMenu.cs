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
	private Text title;
	public bool setScore = false;
	public bool beatGame = false;
	
	void Awake()
	{
		title = GameObject.Find ("DeathTitle").GetComponent<Text>();
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
		beatGame = false;
		GameObject.Find ("SubmitText").GetComponent<Text>().text = "SUBMIT";
		GameObject.Find ("DeathTitle").GetComponent<Text>().text = "YOU HAVE DIED";
	}

	public void postHighScore()
	{
		int score = UIController.GetUI ().GetScoreManager ().score;;
		string name = GameObject.Find("HighScoreInput").GetComponent<InputField>().text;
		foreach (Button b in this.GetComponentsInChildren<Button>()){
			b.interactable = false;
		}
		UIController.GetUI().postHighScore(name, score);
	}

    public void sendChallengeEmail()
    {
        string name = GameObject.Find("HighScoreInput").GetComponent<InputField>().text;
        if (name == "")
        {
            name = "A friend";
        }
        int score = UIController.GetUI().GetScoreManager().score;
        string emailAddress = GameObject.Find("EmailInput").GetComponent<InputField>().text;
        string body = name + " has challenged you to play the brand new Cyberpunk shooter: The Tower, they played and achieved a whopping " + score + " points!! Copy and paste this link into your browser to play and try to beat their score! http://studio-scur.github.io. Note: this game requires the Unity Web Player to play.')";
        string URL = "window.open('mailto:" + emailAddress + "?subject=The Tower: New Challenge from " + name + "&body=" + body;
        Application.ExternalEval(URL);

    }


	public void donePosting()
	{
		foreach (Button b in this.GetComponentsInChildren<Button>()){
			if (b.gameObject.name != "Submit"){
				b.interactable = true;
			} else {
				GameObject.Find ("SubmitText").GetComponent<Text>().text = "✓";
			}
		}

	}
	
	void Update() {;
		if (visible) {
			// Set score text if we haven't already
			if (!setScore){
				if (beatGame){
					title.text = "YOU HAVE WON";
				}
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
