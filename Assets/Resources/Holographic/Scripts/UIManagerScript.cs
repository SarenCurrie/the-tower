using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

    private int playerScore = 0;
    SpriteRenderer spriteRenderer;

    private Object[] objects;
    private Sprite[] sprites;
    private Image weapon;
	// Use this for initialization

    void Awake()
    {
        this.weapon = this.GetComponent<Image>();

       // Debug.Log("(((OBJECT )" + objects[0]);


    }
	void Start () {
        this.objects = new Object[2];
        this.objects[0] = Resources.Load("Holographic/output/main/bg/Baxia_S", typeof(Sprite));
        this.objects[0] = Resources.Load("Holographic/output/main/bg/gun4.png", typeof(Sprite));
        Debug.Log("OBJECT )"+objects[0]);


        //Initialize the array of sprites with the same size as the objects array
        this.sprites = new Sprite[objects.Length];

        //Cast each Object to Sprite and store the result inside the Sprites array
        for (int i = 0; i < objects.Length; i++)
        {
            this.sprites[i] = (Sprite)this.objects[i];
        }

       

	}
	
	// Update is called once per frame
	void Update () {
        //playerScore++;
        //this.transform.Find("Score").GetComponent<Text>().text = playerScore.ToString();

        if (Input.GetMouseButtonDown(0)){
            weapon.sprite = sprites[0];
        }

        if (Input.GetMouseButtonDown(0)){
            weapon.sprite = sprites[1];
        }
       
       
	
	}

   

}
