using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour {

	private static DamageFlash singleton;
	private Image image;

	private float fadeTime = 0.5f;
	private bool isImageFading = false;

	// Use this for initialization
	void Start () {
		if (singleton == null) {
			singleton = this;
		}
		image = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isImageFading) {
			Color newColor = new Color (255f, 255f, 255f, 255f);
			image.color = newColor;
		} else {
			Color newColor = new Color (255f, 255f, 255f, 0f);
			image.color = newColor;
		}

	}

	// Call this method for default flash
	public static void flashDamage(){
		singleton.StartCoroutine("Flash");
	}

	IEnumerator Flash()
	{
		isImageFading = true;
		yield return new WaitForSeconds(fadeTime);
		isImageFading = false;
		StopCoroutine ("Flash");
	}

}
