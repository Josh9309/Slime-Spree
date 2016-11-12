using UnityEngine;
using System.Collections;
using System;

public class RedSlimePlayer : PlayerSlime {
    #region Attributes
    [SerializeField] private float SlimeBurstRange = 2;
    [SerializeField] private int SlimeBurstCost = 20;
    #endregion

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
        input.ResetBtns();
	}

    //Red Slime SlimeBurst AOE Attack
    protected override void SlimeAttack2()
    {
        if (input.trap)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, SlimeBurstRange);
            Debug.DrawLine(gameObject.transform.position, new Vector3(transform.position.x + SlimeBurstRange, transform.position.y, transform.position.z));

            foreach (Collider2D thing in cols)
            {
                if (thing.tag == "Enemy")// the slime burst has hit a enemy
                {
                    Enemy slimeEnemy = thing.GetComponent<Enemy>();

                    //check to see if enemy slime is not same type as player
                    if (slimeEnemy.SlimeType != PlayerSlime.SlimeType.RED)
                    {
                        slimeEnemy.health = 0; //kill enemy
                    }
                }
            }
        }
    }

    protected override void SlimeAttack2Cooldown()
    {
        throw new NotImplementedException();
    }

    protected override void SlimeUltimate()
    {
        throw new NotImplementedException();
    }

    protected override void SlimeUltimateCooldown()
    {
        throw new NotImplementedException();
    }
}
