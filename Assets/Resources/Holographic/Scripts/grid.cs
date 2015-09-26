using UnityEngine;
using System.Collections;

public class grid : MonoBehaviour {
	private float a=1f; //alpha control
	private GameObject m_cam;
	// Use this for initialization
	void Start () {
	m_cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		//print (this.transform.parent.transform.parent.transform.parent.transform.parent.transform.eulerAngles);
		this.transform.eulerAngles = new Vector3(-m_cam.transform.eulerAngles.x,-m_cam.transform.eulerAngles.y,-m_cam.transform.eulerAngles.z);
		//this.transform.eulerAngles = new Vector3(0f,0f,0f);
		this.transform.rotation = new Quaternion(-m_cam.transform.rotation.x,-m_cam.transform.rotation.y,-m_cam.transform.rotation.z,m_cam.transform.rotation.w);
		
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
