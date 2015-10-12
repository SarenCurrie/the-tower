using UnityEngine;
using System.Collections;

public class BossBehaviour : Enemy {

    private float LastChecked=0;
    public float MaxHealth;
    public float ChangePercentage;
    private bool isCharging = false;
    public float chargeDamage;

    private float attackTick = 0;
    public float attackTime;

    public float preferedDistance;
    public float preferedDistanceRange;
    public float movementSpeed;
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
        
        if (Time.time > attackTick + attackTime)
        {
            isCharging = true;
        }

        if (isCharging)
        {
            MoveToPlayer();
            gameObject.GetComponent<SpreadShotEnemy>().Fire();
        }
        else RotateToFacePlayer();
    }

    private void AdjustDistanceFromPlayer()
    {
        Vector3 relativePlayerPosition = GetRelativePlayerPosition();
        if (relativePlayerPosition.magnitude - preferedDistanceRange > preferedDistance)
        {
            MoveToPlayer();
        }
        else if (relativePlayerPosition.magnitude + preferedDistance < preferedDistanceRange)
        {
            MoveAwayFromPlayer();
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

    private void MoveToPlayer()
    {
        rigidBody.AddForce(transform.up * movementSpeed * Time.deltaTime);
    }

    private void FleePlayer()
    {

    }

    private void MoveAwayFromPlayer()
    {
        rigidBody.AddForce(transform.up * -1 * movementSpeed);
    }

    private void RotateToFacePlayer()
    {
        Vector3 relativePlayerPos = GetRelativePlayerPosition();
        float angle = Mathf.Atan2(relativePlayerPos.y, relativePlayerPos.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "PlayerProjectile")
        {
            attackTick = Time.time;
            isCharging = false;
            gameObject.GetComponent<SpreadShotEnemy>().Fire();
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            CircleCollider2D player = GameObject.FindWithTag("Player").GetComponent<CircleCollider2D>();
            if (body.IsTouching(player))
            {
                UnitHealth playerHealth = GameObject.FindWithTag("Player").GetComponent<UnitHealth>();
                playerHealth.LoseHealth(chargeDamage);
            }
        }
    }

   
    }
