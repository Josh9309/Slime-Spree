using UnityEngine;
using System.Collections;
using System;

public class GoalEnemy : Enemy {

    //-----ATTRIBUTES-----
    
    //--------------------
	// Use this for initialization
	protected override void Start () {
        base.Start();

        canTriggerGoal = true;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

}
