using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float killTime = 2.0f;

    public void SetForce(Vector2 f)
    {
        GetComponent<Rigidbody2D>().AddForce(f);
    }

	// Use this for initialization
	void Awake ()
    {
        GameObject.Destroy(this, killTime);
    }
	
	// Update is called once per frame
	void Update () {
        CheckForCollision();
    }

    void CheckForCollision ()
    {

    }
}
