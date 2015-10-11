using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatSummaryText : MonoBehaviour {
	
	private Text text;

	void Start () {
		text = this.GetComponent<Text>();
	}

	public void updateStats(int str, int dex, int intl)
	{
		text.text = "STR:\t" + str.ToString () + "\n" + "DEX:\t" + dex.ToString () + "\n" + "INT: \t" + intl.ToString ();
	}

	void Update () {
	
	}
}
