using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is used to manage the score related elements for the Graphical User Interface HUD.
/// It is used to set and update the current score of the player 
/// 
///  @author Harry
/// 
/// </summary>
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
