using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechScreen : MonoBehaviour {


	public GUISkin mySkin;

	private bool _IsShown = false;

    private Rect _InventoryWindowRect;

	
	void Awake()
	{
	}
	
	private void Start()
	{
		_InventoryWindowRect = new Rect( Screen.width -200, Screen.height/2-150, 300, 300);
	}
	
	private void Update()
	{
		GUI.depth = 10;
		if (Input.GetKey(KeyCode.C)) {
			_IsShown = false;
        }
		else{
			_IsShown = true;
    }
		
	}
	
	void OnGUI()
	{
        GUI.depth = 10;
		GUI.skin = mySkin;
        if (_IsShown)
		{
          _InventoryWindowRect = GUI.Window(2, _InventoryWindowRect, InventoryWindow,"");

		} 
	
	}

	private void InventoryWindow(int iD)
	{
	}
	
}