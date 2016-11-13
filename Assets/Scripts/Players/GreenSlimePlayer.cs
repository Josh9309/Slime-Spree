using UnityEngine;
using System.Collections;
using System;

public class GreenSlimePlayer : PlayerSlime {

    // Use this for initialization
    new void Start()
    {
        base.Start(); //Call the base start method
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); //Call the base update method
    }

    protected override void SlimeAttack2()
    {
        //if(input.ultimate) //
        //{
        //
        //}
    }


    protected override void SlimeUltimate()
    {
        throw new NotImplementedException();
    }

}
