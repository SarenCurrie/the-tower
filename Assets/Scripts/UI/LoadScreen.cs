using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	private Image[] myImages;
	private Text subText;

	// Use this for initialization
	void Start () {
		myImages = this.GetComponentsInChildren<Image> ();
		subText = this.GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isLoadingLevel) {
			foreach (Image i in myImages){
				i.enabled = true;
			}
			subText.enabled = true;
		} else {
			foreach (Image i in myImages){
				i.enabled = false;
			}
			subText.enabled = false;
		}
	}
}
