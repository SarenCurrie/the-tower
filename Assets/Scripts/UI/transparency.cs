using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
///
/// This class handles the variable transparency of UI components
/// 
/// </summary>
public class transparency : MonoBehaviour
{
	// Initialise alpha to max across all UI
	private static float a= 1f;

	void Awake()
	{
		if (this.GetComponent<Image> ()) {
			this.GetComponent<Image> ().color = new Color (this.GetComponent<Image> ().color.r, this.GetComponent<Image> ().color.b, this.GetComponent<Image> ().color.g, a);
	
		} else if (this.GetComponent<Text> ()) {
			this.GetComponent<Text> ().color = new Color (this.GetComponent<Text> ().color.r, this.GetComponent<Text> ().color.b, this.GetComponent<Text> ().color.g, a);
		}
	}

	// Update transparency alpha based on value (a %)
	public static void UpdateTransparency(int value){
		// Set alpha to percentage of max
		a = (value / 100f);
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		if (this.GetComponent<Image> ()) {
			this.GetComponent<Image> ().color = new Color (this.GetComponent<Image> ().color.r, this.GetComponent<Image> ().color.b, this.GetComponent<Image> ().color.g, a);
			
		} else if (this.GetComponent<Text> ()) {
			this.GetComponent<Text> ().color = new Color (this.GetComponent<Text> ().color.r, this.GetComponent<Text> ().color.b, this.GetComponent<Text> ().color.g, a);
		}
	}

  

}