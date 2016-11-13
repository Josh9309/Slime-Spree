using UnityEngine;
using System.Collections;
using System;

public class GreenSlimePlayer : PlayerSlime
{
    private bool glutton; //Is the player a glutton

    // Use this for initialization
    new void Start()
    {
        base.Start(); //Call the base start method
        glutton = false; //The player is not a glutton
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); //Call the base update method
        SlimeAttack2();
        SlimeUltimate();
        input.ResetBtns();
    }

    protected override void SlimeAttack2() //The green player's special
    {
        if(input.special != 0 && health > 21 && slimeAttack2Available) //If the special can be used
        {
            
        }
    }

    protected override void SlimeUltimate() //The green player's ultimate
    {
        if (input.ultimate && health > 21 && slimeUltimateAvailable) //If the ultimate can be used
        {
            glutton = true; //The player is a glutton
            ModHealth(-20); //decrease players health by the cost of the attack
            StartCoroutine(SlimeUltimateCooldown()); //Enter cooldown
        }
    }

    void OnCollisionEnter2D(Collision2D coll) //If the player collides with something
    {
        Debug.Log("CAT");
        if (coll.gameObject.name == "GoalEnemy(Clone)" && glutton) //If the player collides with an enemy and is not glutton
        {
            Debug.Log("");
            health += 11; //Regain 10 health
            Destroy(coll.gameObject); //Destroy the enemy
        }
        if (coll.gameObject.name == "AttackEnemy(Clone)" && glutton) //If the player collides with an enemy and is not glutton
        {
            Debug.Log("");
            health += 12; //Regain 10 health
            Destroy(coll.gameObject); //Destroy the enemy
        }
    }
}