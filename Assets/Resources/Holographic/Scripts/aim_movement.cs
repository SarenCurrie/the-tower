using UnityEngine;
using System.Collections;

public class aim_movement : MonoBehaviour {
	private float rot;
	private float i;
	private float speed;
	
	// Use this for initialization
	void Start () {
	rot = Random.Range(-270f,270f);
		i = Random.Range(0f,360f);
		speed = Random.Range(0.6f,1f);
		this.transform.Rotate(Vector3.up,rot);
		
	}
	
	// Update is called once per frame
	void Update () {
		i+=speed*7f;
	this.transform.Rotate(Vector3.up,Mathf.Sin(i/180f)*3f);
	}
}
