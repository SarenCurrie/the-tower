using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// This class is used to manage the weapon related elements for the Graphical User Interface HUD.
/// It is used to set and update the current sprites for the second weapon slot above the health meter
/// and highlight the weapon if it is currently selected/.
/// 
///  @author Jacob
/// 
/// </summary>
public class ArmourIconScript : MonoBehaviour {
	
	public string slot;
	public Sprite equipped;
	public Sprite unequipped;

	private Image armourIcon;
	private bool showWindow = false;
	private RectTransform rect;
	private RectTransform parentRect;
	private float startX;
	
	private GameObject myPopUp;
	
	public GUISkin mySkin;
	
	void Awake()
	{
		this.armourIcon = this.GetComponent<Image>();
		myPopUp = GameObject.Find("StatPopup" + this.name);
	}
	
	void Start()
	{
		rect = this.GetComponent<RectTransform>();
		parentRect = this.transform.parent.GetComponent<RectTransform>();
		startX = myPopUp.GetComponent<RectTransform>().anchoredPosition.x;
	}
	
	public void toggleArmour()
	{
		if (GameManager.GetPlayer ().GetComponent<Player>().GetGearDictionary().ContainsKey(slot)) {
			armourIcon.sprite = equipped;
		} else {
			armourIcon.sprite = unequipped;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		// Check we contain the mouse pointer
		if (RectTransformUtility.RectangleContainsScreenPoint (rect, Input.mousePosition)) {
			showWindow = true;
		} else {
			showWindow = false;
		}
	}

	// Moves the popup into, or out of, position
	private void togglePopup(bool show, string text)
	{
		Vector2 pos = myPopUp.GetComponent<RectTransform>().anchoredPosition;
		if (show) {
			pos.x = startX;
		} else {
			pos.x = Screen.width * 2;
		}
		myPopUp.GetComponent<RectTransform>().anchoredPosition = pos;
		myPopUp.GetComponentInChildren<Text>().text = text;
	}


	//Called every frame to check if the on hover will open a comparison popup for the armour
	void OnGUI()
	{
		GUI.skin = mySkin;
		if (showWindow) {
			if (GameManager.GetPlayer ().GetComponent<Player> ().GetGearDictionary ().ContainsKey (slot)) {
				Armour item = GameManager.GetPlayer ().GetComponent<Player> ().GetGearDictionary () [slot].GetComponent<Armour> ();
				if (item != null) {
					float strength = item.GetStrength ();
					float intelligence = item.GetIntelligence ();
					float dexterity = item.GetDexterity ();
					togglePopup (true, slot + ":\nStrength: " + strength + "\nDexterity: " + dexterity + "\nIntelligence: " + intelligence + "\n");
				}
			}
		} else {
			togglePopup (false,"");
		}
	}
	
}
