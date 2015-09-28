using UnityEngine;
using System.Collections;

public class sliders : MonoBehaviour {
	
	public GUISkin mySkin;
	private Rect windowRect0 = new Rect (Screen.width-500, 10, 480, 490);
	private Rect windowRect1 = new Rect (50, 10, 450, 520);
	
	private bool ToggleBTN = false;
	private bool Window0 = false;
	private bool  Window1 = true;
	public static float health = 100f;
	public static float armor = 100f;
	public static float bullets = 1f;
	private float bul=1f;
	private float HorizSliderValue = 0.5f;
private float VertSliderValue = 0.5f;
public static float opacity=1.0f;
	public static bool roboGui=true;
	// Use this for initialization
	void Start () {
	
	}
	
	void MyWindow0 (int windowID) 
{
		
		GUILayout.BeginVertical();
		GUILayout.Space(2);
		   
        GUILayout.Label("Standard Label");
        GUILayout.Space(10);
        GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("Short Label", "ShortLabel");
		GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal ();
		GUILayout.Label("", "Divider");
		GUILayout.Button("Standard Button");
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Button("Short Button", "ShortButton");
		
			 GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal ();
		GUILayout.Label("", "Divider");
		ToggleBTN = GUILayout.Toggle(ToggleBTN, "This is a Toggle Button");
		GUILayout.Label("", "Divider");
		GUILayout.Box("This is a textbox\n this can be used for big texts");
		GUILayout.TextField("This is a textfield");
        HorizSliderValue = GUILayout.HorizontalSlider(HorizSliderValue, 0.0f, 1.0f);
        GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		VertSliderValue = GUILayout.VerticalSlider(VertSliderValue, 0.0f, 1.0f,GUILayout.Height(70f));
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
		
		// Make the windows be draggable.
		GUI.DragWindow ();
}
	
	void MyWindow1 (int windowID) 
{
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		GUILayout.Label("INTERFACE","ShortLabel");
		 GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal ();
		
		GUILayout.Space(15);
		
		GUILayout.Label("", "Divider");
		
		 GUILayout.Label("HEALTH: "+Mathf.Round(health).ToString());
   		 
		GUILayout.BeginHorizontal ();
		GUILayout.Space(80);
		health = GUILayout.HorizontalSlider ( health, 0f, 100f);
		 GUILayout.Space(80);
        GUILayout.EndHorizontal ();
		
		GUILayout.Label("", "Divider");
		
		GUILayout.Space(8);
		GUILayout.Label("ARMOR: " + Mathf.Round(armor).ToString());
		
		GUILayout.BeginHorizontal ();
		GUILayout.Space(80);
	armor = GUILayout.HorizontalSlider ( armor, 0f, 100f);
		 GUILayout.Space(80);
       GUILayout.EndHorizontal ();
		
		GUILayout.Label("", "Divider");
		
		bul = bullets*12f;
		GUILayout.Space(8);
		GUILayout.Label("Weapon: "+ bullets.ToString());
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Weapon 1", "ShortButton"))
			bullets=1f;
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Weapon 2", "ShortButton"))
			bullets=2f;
		GUILayout.FlexibleSpace();
		 GUILayout.EndHorizontal ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Weapon 3", "ShortButton"))
			bullets=3f;
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Weapon 4", "ShortButton"))
			bullets=4f;
		GUILayout.FlexibleSpace();
		 GUILayout.EndHorizontal ();
		
		GUILayout.Label("", "Divider");
		GUILayout.Label("OPACITY: "+ (Mathf.Round(opacity*10f)/10f).ToString());
		
		GUILayout.BeginHorizontal ();
		GUILayout.Space(80);
	opacity = GUILayout.HorizontalSlider ( opacity, 0f, 1f);
		 GUILayout.Space(80);
        GUILayout.EndHorizontal ();
		
		GUILayout.Label("", "Divider");		
		
		bullets= (int)bullets;
		GUILayout.Space(10);
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace();
		Window0 =GUILayout.Toggle(Window0, "SKIN PREVIEW");
		
		GUILayout.FlexibleSpace();
		roboGui = GUILayout.Toggle(roboGui, "ROBO-INTERFACE");
		GUILayout.FlexibleSpace();
		 GUILayout.EndHorizontal ();
	GUILayout.EndVertical();
					GUILayout.Space(10);
		GUILayout.EndHorizontal();
		
		GUI.DragWindow ();
	}
	
	
	// Update is called once per frame
	void OnGUI () {
	GUI.skin = mySkin;
	if (Window0)
	windowRect0 = GUI.Window (0, windowRect0, MyWindow0, "");	
		if (Window1)
	windowRect1 = GUI.Window (1, windowRect1, MyWindow1, "");	
		
	}
	

	
}
