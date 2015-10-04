using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	// Use this for initialization
    private float score = 0f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        int updatedScore = GameManager.GetPlayer().GetComponent<Player>().score;
        Debug.Log(updatedScore);
        this.transform.Find("Score").GetComponent<Text>().text = updatedScore.ToString();
        score = updatedScore;

	
	}
}
