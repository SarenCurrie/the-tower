using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
///
/// This class handles the stat summary shown over the player area
/// 
/// </summary>
public class StatSummaryText : MonoBehaviour {
	
	private Text text;

	void Start () {
		text = this.GetComponent<Text>();
	}

	//Update the displayed stats
	public void updateStats(int str, int dex, int intl)
	{
		text.text = "STR:\t" + str.ToString () + "\n" + "DEX:\t" + dex.ToString () + "\n" + "INT:  \t" + intl.ToString ();
	}

	void Update () {
	
	}
}
