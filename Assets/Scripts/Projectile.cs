using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float killTime = 2.0f;

	private float damage;

	public void SetDamage(float f)
	{
		damage = f;
	}

	// Use this for initialization
	void Start ()
    {
        GameObject.Destroy(GetComponent<GameObject>(), killTime);
    }
	
	// Update is called once per frame
	void Update () {
        CheckForCollision();
    }

    void CheckForCollision ()
    {

    }
}
