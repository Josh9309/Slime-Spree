using UnityEngine;
using System.Collections;
using System;

public class RedSlimePlayer : PlayerSlime {
    

    // Use this for initialization
    void Start()
    {
        base.Start(); //Call the base start method
	}
	
	// Update is called once per frame
	void Update()
    {
        base.Update(); //Call the base update method
	}

    protected override void SlimeAttack2()
    {
        throw new NotImplementedException();
    }

    protected override void SlimeTrap()
    {
        throw new NotImplementedException();
    }
}
