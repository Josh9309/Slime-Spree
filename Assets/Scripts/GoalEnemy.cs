using UnityEngine;
using System.Collections;
using System;

public class GoalEnemy : Enemy {

    //-----ATTRIBUTES-----
    
    //--------------------

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        Seek();
	}

    /// <summary>
    /// Goal seeker should always be seeking the goal
    /// </summary>
    public override void Seek()
    {
        Vector3 offset = target.transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        transform.up = unitOffset;
        rb.velocity = unitOffset * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    /// <summary>
    /// calculate movement based on seek
    /// </summary>
    public override void Move()
    {
        Seek();
    }
}
