using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

    private int playerScore = 0;
	// Use this for initialization
	void Start () {
        this.transform.Find("Score").GetComponent<Text>().text = playerScore.ToString();

	}
	
	// Update is called once per frame
	void Update () {
        playerScore++;
        this.transform.Find("Score").GetComponent<Text>().text = playerScore.ToString();

       
	
	}

    void changeToWeapon1() { }

    void changeToWeapon2() { }

}
