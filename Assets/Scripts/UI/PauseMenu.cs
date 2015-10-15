using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public GUISkin mySkin;
	public static bool paused = false;
	public static bool canPause = true;
	public string startScreenName;
	public string firstLevelName;

	private float pauseHeight;
	private RectTransform pauseArea;
	private float pauseUp;
	private float pauseDown;
	private Vector2 temp;
	private float _MoveSpeed = 20f;

	void Awake()
	{
		pauseArea = this.GetComponent<RectTransform>();
		pauseHeight = pauseArea.rect.width;
		pauseUp = 2 * Screen.height;
		pauseDown = pauseArea.anchoredPosition.y;
	}

	void Start () {
	}

	void Resume(){
		paused = false;
	}

	void Restart(){
		paused = false;
	}

	void Quit(){
		//Application.LoadLevel ()
	}
	
	void Update() {;
		// On ESCAPE, if allowed, pause the game
		if (Input.GetKey(KeyCode.Escape) && canPause) {
			paused = true;
		}
		if (paused) {
			// Pause time
			Time.timeScale = 0.0f;
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseDown, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		} else {
			// Unpause the game.
			Time.timeScale = 1.0f;
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseUp, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		}
	}
}
