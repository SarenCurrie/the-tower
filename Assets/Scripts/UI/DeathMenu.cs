using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour {
	
	public static bool visible = false;
	
	private float deathHeight;
	private RectTransform deathArea;
	private float deathUp;
	private float deathDown;
	private Vector2 temp;
	private float _MoveSpeed = 20f;
	
	void Awake()
	{
		deathArea = this.GetComponent<RectTransform>();
		deathHeight = deathArea.rect.width;
		deathUp = 2 * Screen.height;
		deathDown = deathArea.anchoredPosition.y;
	}
	
	void Start () {
		Vector2 outvect = deathArea.anchoredPosition;
		outvect.y = deathUp;
		deathArea.anchoredPosition = outvect;
		visible = false;
	}
	
	void Update() {;
		if (visible) {
			temp = deathArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(deathArea.anchoredPosition.y, deathDown, _MoveSpeed);
			deathArea.anchoredPosition = temp;
		} else {
			temp = deathArea.anchoredPosition;
			temp.y = Mathf.MoveTowards(deathArea.anchoredPosition.y, deathUp, _MoveSpeed);
			deathArea.anchoredPosition = temp;
		}
	}
}
