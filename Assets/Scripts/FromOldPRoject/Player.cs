using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Dieble {

    public Vector2 shiftPower;

    private bool isAlive;
    private Rigidbody2D rb;
    public float summaryMass = 1;
    public float enemyMass;
    public int crashEnemyCount = 0;


    private GameManager gameManager;

    public void KillEnemy(float mass)
    {
        Debug.Log("Kill enemy: " + mass);

        //crashEnemyCount--;
        RemoveMass(mass);

        gameManager.AddScoreForKill();
    }

    public void AddMass(float mass)
    {
        //crashEnemyCount++;
        //summaryMass += mass;
        //Debug.Log("Add mass: " + summaryMass);
    }

    public void RemoveMass(float mass)
    {
        
        //summaryMass = Mathf.Clamp(summaryMass - mass, 1, 10);
        //Debug.Log("Remove mass: " + summaryMass);
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;

        gameManager = FindObjectOfType<GameManager>();
        
    }

    void Update () {

        //Debug.Log(summaryMass);
	}

    public void SetVelocity(Vector2 direction)
    {

        if (direction.x != 0 || direction.y != 0)
            direction *= Mathf.Sqrt(2) / 2;

        float shiftPowerY = shiftPower.y;

        if(direction.y < 0)
        {
            shiftPowerY *= 2;
        }
        else
        {
            shiftPowerY *= 1.5f;
        }

        int enemiesCount = GetComponentsInChildren<Enemy>().Length;
        crashEnemyCount = enemiesCount;
        float currentMass = summaryMass;
        if (enemiesCount > 0)
            currentMass = summaryMass + enemiesCount * enemyMass;
        rb.velocity = new Vector3(direction.x * shiftPower.x, direction.y * shiftPowerY) * (1 / currentMass);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Obstacle)))
        {
            Die();
        }
        else if (collision.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Enemy)))
        {
            
        }


    }

    protected override void Die()
    {
        /*rb.simulated = false;
        isAlive = false;*/
        gameManager.GameOver();
    }
}
