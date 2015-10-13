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

	//The names of the scenes in the game
	public string firstLevelName;
	public string startScreenName;

	//Objects for the various game menus
	private DeathMenu deathMenu;
	private ScoreManager scoreManager;
	private HealthManager healthManager;

	//Singleton Pattern
	private static UIController ui;

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
			if (r.gameObject.tag == Tags.HealthElement)
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
			if (r.gameObject.tag == Tags.Score)
			{
				r.gameObject.GetComponent<Text>().text = score.ToString();
			}
		}
	}

	void Start()
	{
		deathMenu = new DeathMenu();
		scoreManager = new ScoreManager();
		healthManager = new HealthManager();
	}

	void Update()
	{
		healthManager.Process();
	}

	public GUISkin getGui()
	{
		return gui;
	}

	public void showDeathMenu()
	{
		deathMenu.Show();
	}

	public void UpdateHealth()
	{
		healthManager.UpdateHealth();
	}

	public ScoreManager GetScoreManager()
	{
		return scoreManager;
	}
}
