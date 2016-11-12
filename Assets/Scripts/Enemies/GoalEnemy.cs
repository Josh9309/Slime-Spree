using UnityEngine;
using System.Collections;
using System;

public class GoalEnemy : Enemy {

    //-----ATTRIBUTES-----
    
    //--------------------

	// Use this for initialization
	void Start () {
        base.Start();

        canTriggerGoal = true;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

}
