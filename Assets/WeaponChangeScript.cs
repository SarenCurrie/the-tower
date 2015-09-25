using UnityEngine;
using System.Collections;

public class WeaponChangeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Button was pressed");
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == Resources.Load<Sprite>("gun3"))
        {
            print("gun3");
            spriteRenderer.sprite = Resources.Load<Sprite>("gun4");
        }
        if (spriteRenderer.sprite == Resources.Load<Sprite>("gun4"))
        {

            spriteRenderer.sprite = Resources.Load<Sprite>("gun3");
        }
    }
}
