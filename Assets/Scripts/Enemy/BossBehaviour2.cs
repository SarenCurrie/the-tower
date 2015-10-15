using UnityEngine;
using System.Collections;

public class BossBehaviour2 : Enemy
{

    private float LastChecked = 0;
    public float MaxHealth;
    public float ChangePercentage;
    private float attackTick = 0;
    public float attackTime;
    public int count = 0;
    public float currentHealth;
    public int boundary = 100;

    private bool hasSeenPlayer = false;

    public int baseScore;

    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlayer() == null)
            return;
        RotateToFacePlayer();
        Debug.Log(GameManager.currentFloor.currentRoom);
        currentHealth = gameObject.GetComponent<UnitHealth>().GetHealth();
        if (((int)(currentHealth*100)/MaxHealth)< boundary)
        {
            
            gameObject.GetComponent<SpreadShotEnemy>().Fire();
            boundary = boundary - 5;
        }
        count++;
    }

    private Player GetPlayer()
    {
        return GameObject.FindObjectOfType<Player>();
    }

    private Vector3 GetRelativePlayerPosition()
    {
        return GetPlayer().GetComponent<Transform>().position - transform.position;
    }

    private void RotateToFacePlayer()
    {
        Vector3 relativePlayerPos = GetRelativePlayerPosition();
        float angle = Mathf.Atan2(relativePlayerPos.y, relativePlayerPos.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

   
}
