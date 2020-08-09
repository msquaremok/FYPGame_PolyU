using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthCTRL : MonoBehaviour {
  
    /// <summary>
    /// Makes the loot bounce up and down
    /// </summary>


    
    public bool killThisMonster = false; // a value to change in the inspector to test the spawning of loot

    public bool spawnHeartOnDeath; // can be checked on if you want the monster to spawn a heart on death

    public int maxEnemyHealth; // variables to change in the inspector
    public GameObject Heart;


    private bool hasSpawnedHeart;
    private int curenemyHealth;

    private CircleCollider2D[] circleCol; // all the components that needs to be turned off when enemy dies
    private BoxCollider2D[] boxCol;


    private SpriteRenderer sr; // refrences
    private Rigidbody2D rb;

	void Start () // on startup
    {
        hasSpawnedHeart = false;
        curenemyHealth = maxEnemyHealth;
        circleCol = GetComponents<CircleCollider2D>();
        boxCol = GetComponents<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if(killThisMonster == true) // kills monster through the inspector
        {
            Die();
        }

		if(curenemyHealth <= 0) // kills monster if health reaches 0
        {
            Die();
        }
	}
    void OnCollisionEnter2D(Collision2D col) // checks for kollision to take damage from different kinds of attacks
    {
        if(col.gameObject.tag == "Bullet_1")
        {
            curenemyHealth = curenemyHealth - 1;
        }
        if (col.gameObject.tag == "Bullet_2")
        {
            curenemyHealth = curenemyHealth - 2;
        }
    }

    void Die() // when enemy has to die
    {
        foreach (CircleCollider2D collider in circleCol) // disables all corcle and box colliders on enemy
        {
            collider.enabled = false;
        }
        foreach (BoxCollider2D collider in boxCol)
        {
            collider.enabled = false;
        }

        sr.enabled = false;

        if(spawnHeartOnDeath == true && hasSpawnedHeart == false) // spawns loot when dead
        {
            hasSpawnedHeart = true;
            Instantiate(Heart, gameObject.transform.position, Quaternion.identity);
        }
        rb.gravityScale = 0;
    }

}
