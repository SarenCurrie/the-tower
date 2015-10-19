using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///
/// This class handles all the actions of the in game pause menu, 
/// including updating transparency of the HUD and God Mode.
/// 
/// </summary>
public class PauseMenu : MonoBehaviour {

	// Public flags for in game processes
	public static bool paused = false;
	public static bool canPause = true;

	// Internal references for movement and alteration
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

	public void toggleGodMode() {
		GameManager.GetPlayer ().GetComponent<Player> ().godMode = GameObject.Find ("GodModeToggle").GetComponent<Toggle> ().isOn;
	}

	public void UpdateTransparency() {
		int value = (int)(GameObject.Find ("TransSlider").GetComponent<Slider> ().value);
		transparency.UpdateTransparency(value);
		GameObject.Find ("TransPercent").GetComponent<Text>().text = value.ToString()+"%";
	}
	
	void Update() {;
		// On ESCAPE, if allowed, pause the game
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UIController.GetUI().TogglePause();
		}
		if (paused) {
			Time.timeScale = 0.0f;
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseDown, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		} else {
			Time.timeScale = 1.0f;
			temp = pauseArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(pauseArea.anchoredPosition.y, pauseUp, _MoveSpeed);
			pauseArea.anchoredPosition = temp;
		}
	}
}
