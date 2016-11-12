using UnityEngine;
using System.Collections;
using System;

public class AttackEnemy : Enemy {
    /// <summary>
    /// finds and returns the closest player relative
    /// to the enemy
    /// </summary>
    public void findPlayer()
    {

    }

    // Use this for initialization
    public override void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	public override void Update () {
        Move();
	}
}
