using UnityEngine;
using System.Collections;
using System;

public class BlueSlimePlayer : PlayerSlime {

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

    /// <summary>
    /// Freeze shot, will shoot a projectile until it
    /// hits something
    /// </summary>
    protected override void SlimeAttack2()
    {
        throw new NotImplementedException();
    }

    protected override void SlimeUltimate()
    {
        throw new NotImplementedException();
    }

}
