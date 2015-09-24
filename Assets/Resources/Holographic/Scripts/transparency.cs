using UnityEngine;
using System.Collections;

public class transparency : MonoBehaviour
{
	private float a=1f; //alpha control
	void Awake()
	{
		a= sliders.opacity;
		this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,.65f*a);
	}

	void Start ()
	{
		
	}
	
	
	

	void Update ()
	{
	
		if (a!=sliders.opacity){
			a= sliders.opacity;
this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,.65f*a);
		}
		
	}

  

}