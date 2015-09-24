using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;

    public void SetForce(Vector2 f)
    {
        GetComponent<Rigidbody2D>().AddForce(f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckForCollision();
    }

    void CheckForCollision ()
    {

    }
}
