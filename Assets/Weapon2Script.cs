using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Weapon2Script : MonoBehaviour
{

    private Object[] objects;
    private Sprite[] sprites;
    private Image weapon;

    void Awake()
    {
        this.weapon = this.GetComponent<Image>();

        // Debug.Log("(((OBJECT )" + objects[0]);


    }
    void Start()
    {
        this.objects = new Object[2];
        this.objects[0] = Resources.Load("Holographic/output/main/bg/Insanity'sTeardrop_U", typeof(Sprite));
        this.objects[1] = Resources.Load("Holographic/output/main/bg/Insanity'sTeardrop_S", typeof(Sprite));
        Debug.Log("OBJECT )" + objects[0]);


        //Initialize the array of sprites with the same size as the objects array
        this.sprites = new Sprite[objects.Length];

        //Cast each Object to Sprite and store the result inside the Sprites array
        for (int i = 0; i < objects.Length; i++)
        {
            this.sprites[i] = (Sprite)this.objects[i];
        }
        weapon.sprite = sprites[0];



    }

    // Update is called once per frame
    void Update()
    {
        //playerScore++;
        //this.transform.Find("Score").GetComponent<Text>().text = playerScore.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            weapon.sprite = sprites[0];
        }

        if (Input.GetMouseButtonDown(1))
        {
            weapon.sprite = sprites[1];
        }



    }

}
