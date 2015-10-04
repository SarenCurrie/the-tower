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
		// NOTE: Position of guns is hard-coded currently - want to make it depend later on their starting position
		weapon1 = GameObject.Find ("Weapon1").GetComponent<RectTransform> ();
		weapon2 = GameObject.Find("Weapon2").GetComponent<RectTransform> ();
		playerArea = GameObject.Find ("PlayerArea").GetComponent<RectTransform> ();
		_InventoryWindowHeight = playerArea.rect.height;
		_InventoryWindowDown = 0 - 2 * _Offset;
		_InventoryWindowUp = -_InventoryWindowHeight - _Offset;
		_Weapon1Down = (_InventoryWindowHeight) + _Offset;
		_Weapon1Up = 200f;
		_Weapon2Down = (_InventoryWindowHeight) + _Offset + 70f;
		_Weapon2Up = 270f;

	}
	
	private void Start()
	{
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
	}
	
	private void Update()
	{
		if (Input.GetKey(KeyCode.Q))
			_IsShown = false;
		else
			_IsShown = true;
		
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