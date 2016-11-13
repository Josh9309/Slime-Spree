using UnityEngine;
using System.Collections;
using System;

public class BlueSlimePlayer : PlayerSlime {

    [SerializeField]
    private GameObject freezeShotObject;

    // Use this for initialization
    new void Start()
    {
        base.Start(); //Call the base start method
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); //Call the base update method

        SlimeAttack2();
    }

    /// <summary>
    /// Freeze shot, will shoot a projectile until it
    /// hits something or goes until its max range, distance
    /// from position to reticle, then will explode in 
    /// an AOE attack, freezing all enemies in range
    /// </summary>
    protected override void SlimeAttack2()
    {
        if (input.special != 0.0f && health > 10 && slimeAttack2Available)
        {
            health -= 10;
            Instantiate(freezeShotObject, transform.position, Quaternion.identity);
            StartCoroutine(SlimeAttack2Cooldown());
        }
    }

    protected override void SlimeUltimate()
    {
        throw new NotImplementedException();
    }

}
