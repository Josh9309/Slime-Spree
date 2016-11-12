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
        Move();
	}

}
