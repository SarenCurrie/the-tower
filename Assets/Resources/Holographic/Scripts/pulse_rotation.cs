using UnityEngine;
using System.Collections;

public class pulse_rotation : MonoBehaviour {
	float rot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rot=this.transform.rotation.y-this.transform.parent.transform.parent.rotation.y;
		print (rot+" "+this.transform.parent.rotation.x+" "+this.transform.rotation.x);
	this.transform.Rotate(Vector3.forward, (rot)*10f);
	}
}
