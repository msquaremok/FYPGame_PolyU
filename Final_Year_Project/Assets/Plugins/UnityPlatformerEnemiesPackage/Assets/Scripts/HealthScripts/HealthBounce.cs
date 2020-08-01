using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBounce : MonoBehaviour {

    /// <summary>
    /// Makes the loot bounce up and down
    /// </summary>

    public float jumpForce; // values to change in the inspector
    public float maxTimeBetweenJumps;

    private float currTimeBetweenJumps;
    private Rigidbody2D rb; // refrence

    void Start () // on startup
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, jumpForce * Time.deltaTime); // makes loot jump on startup
    }
	

	void Update ()
    {
		if(currTimeBetweenJumps <= 0) // counts down and jumps between inervals
        {
            rb.velocity = new Vector2(0, jumpForce * Time.deltaTime);
            currTimeBetweenJumps = maxTimeBetweenJumps;
        }
        else if(currTimeBetweenJumps > 0)
        {
            currTimeBetweenJumps = currTimeBetweenJumps - Time.deltaTime; // counts down the timer
        }
	}
}
