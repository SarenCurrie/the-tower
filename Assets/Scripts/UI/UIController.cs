﻿using UnityEngine;
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

	public Texture speechImage;

	//The names of the scenes in the game
	public string firstLevelName;
	public string startScreenName;

	//Objects for the various game menus
	private DeathMenu deathMenu;
	private ScoreManager scoreManager;
	private HealthManager healthManager;
	private SpeechScreen speechScreen;
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
		deathMenu = new DeathMenu();
		scoreManager = new ScoreManager();
		healthManager = new HealthManager();
		speechScreen = new SpeechScreen();
		damageFlash = new DamageFlash();
	}

	void OnGUI()
	{
		deathMenu.UI();
		speechScreen.UI();
	}

	void Update()
	{
		healthManager.Process();
		speechScreen.Process();
		damageFlash.Process();
	}

	public GUISkin GetGui()
	{
		return gui;
	}

	public Texture GetSpeechImage()
	{
		return speechImage;
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
		deathMenu.Show();
	}

	public void FlashDamage()
	{
		damageFlash.FlashDamage();
	}

	public void ShowDialog(string text, float time)
	{
		speechScreen.Show(text, time);
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