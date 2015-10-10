using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

    private float LastChecked=0;
    public float MaxHealth;
    public float ChangePercentage;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > LastChecked+1)
        {   //First Phase transition
           // if ((gameObject.GetComponent<UnitHealth>().GetHealth() / MaxHealth) < 0.5 * MaxHealth)
           // {
                foreach (Transform t in transform)
                {   
                    //if (t.gameObject.tag.Equals("BossWeapon"))
                       // t.gameObject.GetComponent<RangedEnemy>().Fire();
                }
            //}  
           
        }
    }
}
