using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
	public static bool paused = false;
	public static bool canPause = true;

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
		Vector2 outvect = pauseArea.anchoredPosition;
		outvect.y = pauseUp;
		pauseArea.anchoredPosition = outvect;
		UpdateTransparency();
	}

	public void UpdateTransparency() {
		int value = (int)(GameObject.Find ("TransSlider").GetComponent<Slider> ().value);
		transparency.UpdateTransparency(value);
		GameObject.Find ("TransPercent").GetComponent<Text>().text = value.ToString()+"%";
	}

	public void UpdateVolume() {
		int value = (int)(GameObject.Find ("VolumeSlider").GetComponent<Slider> ().value);
		//Do the actual update
		GameObject.Find ("VolumePercent").GetComponent<Text>().text = value.ToString()+"%";
	}
	
	void Update() {;
		// On ESCAPE, if allowed, pause the game
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UIController.GetUI().TogglePause();
		}
		if (paused) {
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseDown, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		} else {
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseUp, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		}
	}
}
