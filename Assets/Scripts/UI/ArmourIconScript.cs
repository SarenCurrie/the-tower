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
	private float startX;
	private float startY;
	private float popupHeight = 80;
	private float popupWidth = 150;
	
	public GUISkin mySkin;
	
	void Awake()
	{
		this.armourIcon = this.GetComponent<Image>();
	}
	
	void Start()
	{
		rect = this.GetComponent<RectTransform> ();
		startX = rect.anchoredPosition.x + rect.rect.width;
		startY = rect.anchoredPosition.y + popupHeight/2;
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

	//Called every frame to check if the on hover will open a comparison popup for the armour
	void OnGUI()
	{
		GUI.skin = mySkin;
		if (showWindow) {
			if (GameManager.GetPlayer ().GetComponent<Player> ().GetGearDictionary ().ContainsKey(slot)) {
				Armour item = GameManager.GetPlayer ().GetComponent<Player> ().GetGearDictionary () [slot].GetComponent<Armour> ();
				if (item != null) {
					float strength = item.GetStrength ();
					float intelligence = item.GetIntelligence();
					float dexterity = item.GetDexterity ();
					GUI.TextField (new Rect (Input.mousePosition.x, Screen.height - Input.mousePosition.y - popupHeight, popupWidth, popupHeight), slot + ":\nStrength: " + strength + "\nDexterity: " + dexterity + "\nIntelligence: " + intelligence + "\n", "OutlineText");
				}
			}
		}
	}
	
}
