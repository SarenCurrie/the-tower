using UnityEngine;
using System.Collections;

public class BossBehaviour2 : Enemy
{
    private bool turretsDestroyed = false;
    private float LastChecked = 0;
    private float ChangePercentage;
    private float attackTick = 0;
    public float attackTime;
    private float count = 0;
    private float bossLaserCount = 0;
    private float currentHealth;
    private int fireboundary = 85;
    private int fireboundaryreduction = 10;
    private int spawnBoundary = 80;
    private int spawnboundaryreduction = 20;
    private float laserBurst = 0.4f;
    private float warningShotTimer = 1.4f;
    public GameObject enemyToSpawn;
    public GameObject forceField;
    public Vector3 enemyLocation1;
    public Vector3 enemyLocation2;
    public Vector3 enemyLocation4;
    public Vector3 enemyLocation3;

    private bool hasSeenPlayer = false;

    public int baseScore;

    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enemyLocation2 = transform.position - new Vector3(21, -5, 0);
        enemyLocation1 = transform.position - new Vector3(21, 5, 0);
        enemyLocation2 = transform.position - new Vector3(19, -5, 0);
        enemyLocation1 = transform.position - new Vector3(19, 5, 0);
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
        EnemySpawn();
        if(GameManager.currentFloor.currentRoom.EnemiesLeft() > 1 && !turretsDestroyed)
        {
            gameObject.GetComponent<UnitHealth>().ResetHealth();
        }else if (GameManager.currentFloor.currentRoom.EnemiesLeft() == 1 && !turretsDestroyed)
        {
            turretsDestroyed = true;
            forceField.SetActive(false);
            gameObject.GetComponent<SpreadShotEnemy>().fireFrequency = 1;
            gameObject.GetComponent<SpreadShotEnemy>().spread = 39;
        }
    }

    private void EnemySpawn()
    {
        if (((currentHealth / GetComponent<UnitHealth>().maxHealth) * 100) < spawnBoundary)
        {
            spawnBoundary -= spawnboundaryreduction;
            Instantiate(enemyToSpawn, enemyLocation1, new Quaternion());
            Instantiate(enemyToSpawn, enemyLocation2, new Quaternion());
            Instantiate(enemyToSpawn, enemyLocation3, new Quaternion());
            Instantiate(enemyToSpawn, enemyLocation4, new Quaternion());
        }
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
        if(count < 0)
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
