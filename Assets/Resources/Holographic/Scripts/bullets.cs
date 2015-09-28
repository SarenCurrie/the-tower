using UnityEngine;
using System.Collections;

public class bullets : MonoBehaviour {
	private Quaternion rot;
	public static float bullets_n=24f;
	public static float bullets_max=64f;
	GameObject bullet;
	private float b_slider=2f;
	private string name="";
	private float a=1; //alpha control
	
	// Use this for initialization
	
	void OnGUI () {
		}
	
	void Start() {
		createBullets ();
		drawBullets();
		weaponChange();
	}
	void createBullets () {
		print (this.transform.rotation);
		for (int i=0;i<bullets_max;i++){
	 bullet = (GameObject)Instantiate(Resources.Load("Holographic/output/main/bullets/bullet_prefab"),this.transform.position,Quaternion.identity);
		bullet.transform.eulerAngles = GameObject.Find("bullets").transform.rotation.eulerAngles;
			bullet.name="bullet"+i.ToString();
			bullet.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,0);
		
		bullet.transform.parent = this.transform;
			}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (a!=sliders.opacity){
			a= sliders.opacity;
this.GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,.65f*a);
			drawBullets();
		}
		b_slider=sliders.bullets;
	if (bullets_n!= b_slider*12f){
		
			bullets_n=b_slider*12f;
			
			drawBullets();
			
			weaponChange();
		}
	}
	
	void drawBullets(){
		
		for (int i=0;i<bullets_n;i++){
		this.transform.Find("bullet"+i).transform.eulerAngles = GameObject.Find("bullets").transform.rotation.eulerAngles;
		
		this.transform.Find("bullet"+i).transform.localScale = new Vector3(16f/bullets_n,this.transform.Find("bullet"+i).transform.localScale.y,this.transform.Find("bullet"+i).transform.localScale.z);
				//1f*.081f,1f*0.081f);
		
			
			this.transform.Find("bullet"+i).transform.Rotate(Vector3.up,-39.5f-i*9f*16f/bullets_n+(1f-16f/bullets_n)*4f);
			this.transform.Find("bullet"+i).GetComponent<Renderer>().material.color = new Color(1,1,1,.65f*a);
		}
		for (int i1=(int)bullets_n;i1<bullets_max;i1++){
			this.transform.Find("bullet"+i1).GetComponent<Renderer>().material.color = new Color(this.GetComponent<Renderer>().material.color.r,this.GetComponent<Renderer>().material.color.b,this.GetComponent<Renderer>().material.color.g,0);
		}
	}
	
	
	void weaponChange(){
		name ="Holographic/output/main/bg/gun"+Mathf.Round(b_slider);
		GameObject.Find("weapon").GetComponent<Renderer>().material.mainTexture= Resources.Load(name) as Texture;	
		
	}
}
