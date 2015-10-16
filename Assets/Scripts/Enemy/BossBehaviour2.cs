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
        enemyLocation4 = transform.position - new Vector3(19, -5, 0);
        enemyLocation3 = transform.position - new Vector3(19, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetPlayer() == null)
            return;
        
        currentHealth = gameObject.GetComponent<UnitHealth>().GetHealth();
        
		//Constantly rotates to face the player
        RotateToFacePlayer();
		//Calls to shoot, which handles whether it should shoot the laser/warning shot
        ShooterBlast();
		//Calls enemyspawn which will spawn enemies if below a threshold, resetting it as needed
        EnemySpawn();
		//Calls managehealth which decided whether the main boss should take damage based on enemy count
		ManageHealth();
    }
	
	//Enemy count is 1 at the time the boss turrets die, meaning it can then destroy the "forcefield" and
	//take damage. Also changes the shotgun bullets as the boss turrets die
	private void ManageHealth(){
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
	
	//Spawns enemies if below 80%, stuttering down 20% each spawn
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
	
	//Shoots the laser and warning shot as needed
    private void ShooterBlast()
    {
		//If the laser isnt to be shot and warning shot not counting down, and the health is below threshold, do this
        if (count == 0 && bossLaserCount == 0)
        {
            if (((currentHealth / GetComponent<UnitHealth>().maxHealth) * 100) < fireboundary)
            {
                count = laserBurst;
                bossLaserCount = warningShotTimer;
                fireboundary -= fireboundaryreduction;
            }
        }
		//If laserCount is 0, which means if the laser has started shooting. Counts down for a set burst
		//equals to laserBurst variable
        else if (bossLaserCount == 0)
        {
            gameObject.GetComponent<Boss3Weapon>().Fire();
            count -= Time.deltaTime;
        }
		//If just set laserCounter, fire the warning shot
        if (bossLaserCount == warningShotTimer)
        {
            gameObject.GetComponent<Boss3Weapon2>().Fire();
        }
		//Manage the variables to handle the non-int changes
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
