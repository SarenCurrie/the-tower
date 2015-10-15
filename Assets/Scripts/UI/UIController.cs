using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 
/// The top level UI controller class. All UI elements should be accessed
/// through this class.
/// 
/// @Author Tate
/// </summary>
public class UIController : MonoBehaviour {

	//The Gui
	public GUISkin gui;

	//Variables used for resizing
	public static float targetHeight = 768f;
	public static float targetWidth = 1024f;
	//private static float screenHeight = Screen.height;
	//private static float screenWidth = Screen.width;

	//The names of the scenes in the game
	public string firstLevelName;
	public string startScreenName;

	//Objects for the various game items
	private ScoreManager scoreManager;
	private HealthManager healthManager;
	private DamageFlash damageFlash;

	//Singleton Pattern
	public static UIController ui;

	//How to reference all UI components
	public static UIController GetUI()
	{
		if(ui == null)
		{
			ui = Canvas.FindObjectOfType<UIController>();
		}
		return ui;
	}

	/**
	 * Changes the pulse image
	 */
	public void SetHealthSprite(Sprite sprite)
	{
		foreach (RectTransform r in gameObject.GetComponentInChildren<RectTransform>())
		{
			if (r.gameObject.tag == Tags.HEALTH_ELEMENT)
			{
				r.gameObject.GetComponentInChildren<Image>().sprite = sprite;
			}
		}
	}

	/**
	 * Updates the score text to reflect a player's new score
	 */
	public void SetScore(int score)
	{
		foreach (RectTransform r in gameObject.GetComponentInChildren<RectTransform>())
		{
			if (r.gameObject.tag == Tags.SCORE)
			{
				r.gameObject.GetComponent<Text>().text = score.ToString();
			}
		}
	}

	void Awake()
	{
		scoreManager = new ScoreManager();
		healthManager = new HealthManager();
		damageFlash = new DamageFlash();
	}

	void Update()
	{
		healthManager.Process();
		damageFlash.Process();
	}

	public GUISkin GetGui()
	{
		return gui;
	}

	public Image GetDamageImage()
	{
		foreach (RectTransform r in gameObject.GetComponentInChildren<RectTransform>())
		{
			if (r.gameObject.tag == Tags.DAMAGE_FLASH)
			{
				return r.GetComponent<Image>();
			}
		}
		return null;
	}

	public void ShowDeathMenu()
	{
		DeathMenu.visible = true;
	}

	public void FlashDamage()
	{
		damageFlash.FlashDamage();
	}

	public void ShowDialog(string text, float time)
	{
		SpeechScreen.ShowDialog(text, time);
	}

	public void ShowAchievement(string name, string text, float time)
	{
		AchievementPopup.ShowAchievement(name, text, time);
	}

	public void UpdateHealth()
	{
		healthManager.UpdateHealth();
	}

	public ScoreManager GetScoreManager()
	{
		return scoreManager;
	}

	public void TogglePause()
	{
		if (PauseMenu.canPause && PauseMenu.paused == false) {
			PauseMenu.paused = true;
		} else {
			PauseMenu.paused = false;
		}
	}

	public void forceUnpause()
	{
		PauseMenu.paused = false;
		PauseMenu.canPause = true;
	}

	public void Restart()
	{
		forceUnpause();
		GameManager.Restart();
		Application.LoadLevel (firstLevelName);
	}

	public void Quit()
	{
		forceUnpause();
		GameManager.Restart();
		Application.LoadLevel (startScreenName);
	}


	public static Matrix4x4 GetGUIMatrix()
	{
		float rx = Screen.width / targetWidth;
		float ry = Screen.height / targetHeight;
		return Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3 (rx, ry, 1)); 
	}
}
