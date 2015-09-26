using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerAreaShow : MonoBehaviour 
{
	public GUISkin mySkin;

	private float _MoveSpeed = 20f;
	private float _GunMoveSpeed = 12f;
	private bool _IsShown = false;
	
	private float _InventoryWindowHeight = 400f;
	private float _Offset = 5f;
	private float _InventoryWindowWidth;
	private float _InventoryWindowDown;
	private float _InventoryWindowUp;
	private float _Weapon1Down;
	private float _Weapon1Up;
	private float _Weapon2Down;
	private float _Weapon2Up;

	private const int _InventoryWindowID = 1;
	private Rect _InventoryWindowRect;
	private RectTransform weapon1;
	private RectTransform weapon2;
	private Vector2 temp1;
	private Vector2 temp2;
	
	void Awake()
	{
		// NOTE: Position of guns is hard-coded currently - want to make it depend later on their starting position
		_InventoryWindowDown = Screen.height - 5.0f;
		_InventoryWindowUp = Screen.height - (_InventoryWindowHeight) - _Offset;
		_Weapon1Down = Screen.height - (_InventoryWindowHeight) - _Offset + 50f;
		_Weapon1Up = Screen.height - (_InventoryWindowHeight) - 200f;
		_Weapon2Down = Screen.height - (_InventoryWindowHeight) - _Offset + 50f + 60f;
		_Weapon2Up = Screen.height - (_InventoryWindowHeight) - 200f + 60f;
		weapon1 = GameObject.Find ("Weapon1").GetComponent<RectTransform> ();
		weapon2 = GameObject.Find("Weapon2").GetComponent<RectTransform> ();
	}
	
	private void Start()
	{
		_InventoryWindowRect = new Rect( 10, _InventoryWindowDown, 300, _InventoryWindowHeight);
	}
	
	private void Update()
	{
		GUI.depth = 10;
		if (Input.GetKey(KeyCode.Q))
			_IsShown = false;
		else
			_IsShown = true;
		
		if (_IsShown)
		{
			_InventoryWindowRect.y = Mathf.MoveTowards(_InventoryWindowRect.y, _InventoryWindowDown,_MoveSpeed);
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
			_InventoryWindowRect.y = Mathf.MoveTowards(_InventoryWindowRect.y,_InventoryWindowUp,_MoveSpeed);
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
	
	void OnGUI()
	{
		GUI.depth = 10;
		GUI.skin = mySkin;
		_InventoryWindowRect = GUI.Window(_InventoryWindowID, _InventoryWindowRect, InventoryWindow,"");
	}

	private void InventoryWindow(int iD)
	{
	}
	
}