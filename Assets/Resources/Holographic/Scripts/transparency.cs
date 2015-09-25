using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class transparency : MonoBehaviour
{
	private float a=1f; //alpha control
	void Awake()
	{
		a++;
		this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.b,this.GetComponent<Image>().color.g,.65f*a);
	}

	void Start ()
	{
		
	}

	void Update ()
	{
	
		if (a!=sliders.opacity){
			a= sliders.opacity;
			this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.b,this.GetComponent<Image>().color.g,.65f*a);
		}
		
	}

  

}