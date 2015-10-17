using UnityEngine;
using System.Collections;

public class BossBehaviour2minienemies : BossBehaviour
{

	private float LastChecked = 0;
	private float ChangePercentage;
	private float attackTick = 0;
	public float attackTime;
	private float count = 0;
	private float bossLaserCount = 0;
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
		if (GameManager.GetPlayer() == null)
			return;
		//every 20% health the boss will do the laser thing
		//rotate to player
		RotateToFacePlayer();
		ShooterBlast();

	}

	//Shoots the laser and warning shot as needed
	private void ShooterBlast()
	{
		//If the laser isnt to be shot and warning shot not counting down, and the health is below threshold, do this
		if (count == 0 && bossLaserCount == 0)
		{
			if (((gameObject.GetComponent<UnitHealth>().GetHealth() / GetComponent<UnitHealth>().maxHealth) * 100) < fireboundary)
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
		if (count < 0)
		{
			count = 0;
		}
	}


	private Vector3 GetRelativePlayerPosition()
	{
		return GameManager.GetPlayer().GetComponent<Transform>().position - transform.position;
	}

}

