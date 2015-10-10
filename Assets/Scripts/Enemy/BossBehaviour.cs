using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {

    private float LastChecked;
    public float MaxHealth;
    public float ChangePercentage;
    // Update is called once per frame
    void Update()
    {
        if (Time.time > LastChecked+1)
        {   //First Phase transition
            if ((gameObject.GetComponent<UnitHealth>().GetHealth() / MaxHealth) < 0.5 * MaxHealth)
            {
                gameObject.GetComponent<SpreadShotEnemy>().enabled = true;
                //gameObject.GetComponent<SingleShotEnemy>().enabled = true;
            }
                
        }
    }
}
