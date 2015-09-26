using UnityEngine;
using System.Collections;

public class roboGui : MonoBehaviour {
	private float a=1f; //alpha control
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (sliders.roboGui && this.GetComponent<Renderer>().material.color.a==0f){
		this.GetComponent<Renderer>().material.color= new Color(1,1,1,1f*a);	
		}
		
	if (!sliders.roboGui && this.GetComponent<Renderer>().material.color.a==1f*a){
		this.GetComponent<Renderer>().material.color= new Color(1,1,1,0f);	
		}
		if (a!=sliders.opacity &&sliders.roboGui){
			a= sliders.opacity;
this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,1f*a);
		}
	}
}
