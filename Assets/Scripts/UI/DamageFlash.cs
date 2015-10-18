using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///
/// This class handles the 'damage flash', a red border to the screen
/// when the player is hit by an enemy
/// 
/// @Author Jacob
/// </summary>
public class DamageFlash {

	private Image image;

	private const float FADE_TIME = 0.5f;
	private float fadeTime;
	private bool isImageFading = false;

	public DamageFlash() {
		image = UIController.GetUI().GetDamageImage();
	}

	public void Process() {
		if (fadeTime > 0)
			fadeTime -= Time.deltaTime;

		if (fadeTime <= 0)
		{
			fadeTime = 0;
			isImageFading = false;
		}

		if (isImageFading) {
			Color newColor = new Color (255f, 255f, 255f, 255f);
			image.color = newColor;
		} else {
			Color newColor = new Color (255f, 255f, 255f, 0f);
			image.color = newColor;
		}
	}

	// This public method does the damage flashing
	public void FlashDamage()
	{
		fadeTime = FADE_TIME;
		isImageFading = true;
	}

}
