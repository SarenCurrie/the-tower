using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class transparency : MonoBehaviour
{
	private float a=0.01f; //alpha control
	void Awake()
	{
		a = a + 0.01f;
		if (this.GetComponent<Image> ()) {
			this.GetComponent<Image> ().color = new Color (this.GetComponent<Image> ().color.r, this.GetComponent<Image> ().color.b, this.GetComponent<Image> ().color.g, .65f * a);
	
		} else if (this.GetComponent<Text> ()) {
			this.GetComponent<Text> ().color = new Color (this.GetComponent<Text> ().color.r, this.GetComponent<Text> ().color.b, this.GetComponent<Text> ().color.g, .65f * a);
		}
	}

	void Start ()
	{
		
	}

	void Update ()
	{
	
		a=a + 0.01f;
		if (this.GetComponent<Image> ()) {
			this.GetComponent<Image> ().color = new Color (this.GetComponent<Image> ().color.r, this.GetComponent<Image> ().color.b, this.GetComponent<Image> ().color.g, .65f * a);
			
		} else if (this.GetComponent<Text> ()) {
			this.GetComponent<Text> ().color = new Color (this.GetComponent<Text> ().color.r, this.GetComponent<Text> ().color.b, this.GetComponent<Text> ().color.g, .65f * a);
		}
	}

  

}