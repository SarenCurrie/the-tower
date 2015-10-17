using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is used to manage the health related elements for the Graphical User Interface HUD.
/// It is used to set and update the Health meter of the player and also to update the heartRate
/// monitor and heartrate (Not yet implemented).
///
///  @author Harry
///
/// </summary>
public class HealthManager
{
	//An integer to advance frames
	private int frameCounter = 0;
	private float delay = 0.04f;
	private int t=85;
	private Object[] objects;
	private Sprite[] sprites;
	private Sprite flatline;
	private float health = 100f;

	public HealthManager()
	{
		//Load all textures found on the Sequence folder, that is placed inside the resources folder
		objects = Resources.LoadAll("Holographic/output/pulse", typeof(Sprite));

		//Initialize the array of sprites with the same size as the objects array
		sprites = new Sprite[objects.Length];

		//Cast each Object to Sprite and store the result inside the Sprites array
		for(int i=0; i < objects.Length;i++)
		{
			sprites[i] = (Sprite)objects[i];
		}

		//Get flatline sprite
		Object obj = Resources.Load("Holographic/output/flatline", typeof(Sprite));
		flatline = (Sprite)obj;
	}

	public void UpdateHealth()
	{
		health = GameManager.GetPlayer().GetComponent<UnitHealth>().health;

		// Cast to int to stop decimal display
		UIController.GetUI().transform.Find("health_text").GetComponent<Text>().text = ((int)health).ToString();

		// Delay depends on health - speeds up as player loses health
		delay = 0.5f * health / 4000f;
	}

	public void Process()
	{
		delay -= Time.deltaTime;

        if (delay <= 0)
		{
			if (health > 0){
				//Advance one frame
				frameCounter = (++frameCounter) % sprites.Length;

				UIController.GetUI().SetHealthSprite(sprites[frameCounter]);
				// Delay depends on health - speeds up as player loses health
				delay = 0.5f * health / 4000f;
			} else {
				// Flatline health
				UIController.GetUI().SetHealthSprite(flatline);
			}
		}
    }

}