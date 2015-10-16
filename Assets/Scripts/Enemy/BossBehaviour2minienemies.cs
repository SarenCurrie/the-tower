using UnityEngine;
using System.Collections;

public class BossBehaviour2minienemies : Enemy
{

    private float LastChecked = 0;
    private float ChangePercentage;
    private float attackTick = 0;
    public float attackTime;
    private float count = 0;
    private float bossLaserCount = 0;
    private float currentHealth;
    private int fireboundary = 85;
    private int fireboundaryreduction = 20;
    private int spawnBoundary = 80;
    private int spawnboundaryreduction = 20;
    private float laserBurst = 0.2f;
    private float warningShotTimer = 2f;
    public GameObject enemyToSpawn;
    public Vector3 enemyLocation1;
    public Vector3 enemyLocation2;

    private bool hasSeenPlayer = false;

    public int baseScore;

    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyLocation2 = transform.position - new Vector3(21, -5, 0);
        enemyLocation1 = transform.position - new Vector3(21, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlayer() == null)
            return;

        currentHealth = gameObject.GetComponent<UnitHealth>().GetHealth();
        //every 5% health the boss will do the laser thing

        RotateToFacePlayer();
        ShooterBlast();

    }

    private void ShooterBlast()
    {
        if (count == 0 && bossLaserCount == 0)
        {
            if (((currentHealth / GetComponent<UnitHealth>().maxHealth) * 100) < fireboundary)
            {
                count = laserBurst;
                bossLaserCount = warningShotTimer;
                fireboundary -= fireboundaryreduction;
            }
        }
        else if (bossLaserCount == 0)
        {
            gameObject.GetComponent<Boss3Weapon>().Fire();
            count -= Time.deltaTime;
        }
        if (bossLaserCount == warningShotTimer)
        {
            gameObject.GetComponent<Boss3Weapon2>().Fire();
        }
        if (bossLaserCount > 0)
        {
            bossLaserCount -= Time.deltaTime;
        }
        else
        {
            bossLaserCount = 0;
        }
        if (count < 0)
        {
            count = 0;
        }
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

