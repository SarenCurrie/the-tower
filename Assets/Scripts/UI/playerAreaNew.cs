using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerAreaNew : MonoBehaviour 
{
	
	private float _MoveSpeed = 20f;
	private float _GunMoveSpeed = 12f;
	private bool _IsShown = true;
	
	private float _InventoryWindowHeight;
	private float _Offset = 5f;
	private float _InventoryWindowDown;
	private float _InventoryWindowUp;
	private float _Weapon1Down;
	private float _Weapon1Up;
	private float _Weapon2Down;
	private float _Weapon2Up;
	
	private RectTransform weapon1;
	private RectTransform weapon2;
	private RectTransform playerArea;
	private Vector2 temp1;
	private Vector2 temp2;
	private Vector2 temp3;
	
	void Awake()
	{
		weapon1 = GameObject.Find ("Weapon1").GetComponent<RectTransform>();
		weapon2 = GameObject.Find("Weapon2").GetComponent<RectTransform>();
		playerArea = GameObject.Find ("PlayerArea").GetComponent<RectTransform>();
		_InventoryWindowHeight = playerArea.rect.height;
		_InventoryWindowDown = 0 - _InventoryWindowHeight;
		_InventoryWindowUp = playerArea.anchoredPosition.y;
		_Weapon1Down = weapon1.anchoredPosition.y;
		_Weapon1Up = weapon1.anchoredPosition.y + _InventoryWindowHeight/2f + _InventoryWindowHeight/20f;
		_Weapon2Down = weapon2.anchoredPosition.y;
		_Weapon2Up = weapon2.anchoredPosition.y + _InventoryWindowHeight/2f + _InventoryWindowHeight/20f;

	}
	
	private void Start()
	{
		temp3 = playerArea.anchoredPosition;
		temp3.y = _InventoryWindowDown;
		playerArea.anchoredPosition = temp3;
	}
	
	private void Update()
	{
		if (Input.GetKey(KeyCode.Q))
			_IsShown = true;
		else
			_IsShown = false;
		
		if (_IsShown)
		{
			// Move player area
			temp3 = playerArea.anchoredPosition;
			temp3.y = Mathf.MoveTowards(playerArea.anchoredPosition.y, _InventoryWindowUp,_MoveSpeed);
			playerArea.anchoredPosition = temp3;
			// Move weapon 1
			temp1 = weapon1.anchoredPosition;
			temp1.y = Mathf.MoveTowards(weapon1.anchoredPosition.y, _Weapon1Up,_GunMoveSpeed);
			weapon1.anchoredPosition = temp1;
			// Move weapon 2
			temp2 = weapon2.anchoredPosition;
			temp2.y = Mathf.MoveTowards(weapon2.anchoredPosition.y, _Weapon2Up,_GunMoveSpeed);
			weapon2.anchoredPosition = temp2;
		}
		else
		{
			// Move player area
			temp3 = playerArea.anchoredPosition;
			temp3.y = Mathf.MoveTowards(playerArea.anchoredPosition.y, _InventoryWindowDown,_MoveSpeed);
			playerArea.anchoredPosition = temp3;
			// Move weapon 1
			temp1 = weapon1.anchoredPosition;
			temp1.y = Mathf.MoveTowards(weapon1.anchoredPosition.y, _Weapon1Down,_GunMoveSpeed);
			weapon1.anchoredPosition = temp1;
			// Move weapon 2
			temp2 = weapon2.anchoredPosition;
			temp2.y = Mathf.MoveTowards(weapon2.anchoredPosition.y, _Weapon2Down,_GunMoveSpeed);
			weapon2.anchoredPosition = temp2;
		}
	}
}