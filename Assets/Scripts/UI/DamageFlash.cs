using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageFlash {

	private Image image;

	private const float FADE_TIME = 0.5f;
	private float fadeTime;
	private bool isImageFading = false;

	// Use this for initialization
	public DamageFlash() {
		image = UIController.GetUI().GetDamageImage();
	}
	
	// Update is called once per frame
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

	public void FlashDamage()
	{
		fadeTime = FADE_TIME;
		isImageFading = true;
	}

}
