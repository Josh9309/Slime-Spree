using UnityEngine;
using System.Collections;
using System;

public class GreenSlimePlayer : PlayerSlime
{
    private bool glutton; //Is the player a glutton
    private bool acid; //Can the player drop acid
    [SerializeField] private GameObject acidShot; //I SPIT FIRE LIKE I DROP ACID... HARD
    private Behaviour halo; //Ultimate halo

    // Use this for initialization
    new void Start()
    {
        base.Start(); //Call the base start method
        glutton = false; //The player is not a glutton
        halo = (Behaviour)GetComponent("Halo"); //Get the halo
        halo.enabled = false; //Disable the halo
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); //Call the base update method
        SlimeAttack2(); //Call the special
        SlimeUltimate(); //Call the ULTIMATE ATTACK
        input.ResetBtns(); //Reset buttons

        halo.transform.position = transform.position; //Update the position of the halo
    }

    IEnumerator SpecialTimer(float time) //Ultimate timer co-routine
    {
        yield return new WaitForSeconds(time); //Timer
    }

    IEnumerator UltimateTimer(float time) //Ultimate timer co-routine
    {
        glutton = true; //The player becomes a glutton
        halo.enabled = true; //Enable the halo's sprite renderer

        yield return new WaitForSeconds(time); //Timer
        glutton = false; //The player is not a glutton
        halo.enabled = false; //Disable the halo's sprite renderer
    }

    protected override void SlimeAttack2() //The green player's special
    {
        if(input.special != 0 && health > 11 && slimeAttack2Available) //If the special can be used
        {
            health -= 10; //Decrement the player's health
            Instantiate(acidShot, transform.position, Quaternion.identity); //Spawn the acid
            StartCoroutine(SlimeAttack2Cooldown()); //Enter cooldown
        }
    }

    protected override void SlimeUltimate() //The green player's ultimate
    {
        if (input.ultimate && health > 21 && slimeUltimateAvailable) //If the ultimate can be used
        {
            StartCoroutine(UltimateTimer(5));
            ModHealth(-20); //decrease players health by the cost of the attack
            StartCoroutine(SlimeUltimateCooldown()); //Enter cooldown
        }
    }

    void OnCollisionEnter2D(Collision2D coll) //If the player collides with something
    {
        if (coll.gameObject.name.Contains("GoalEnemy(Clone)") && glutton) //If the player collides with an enemy and is not glutton
        {
            health += 11; //Regain 10 health
            Destroy(coll.gameObject); //Destroy the enemy
        }
        if (coll.gameObject.name.Contains("AttackEnemy(Clone)") && glutton) //If the player collides with an enemy and is not glutton
        {
            health += 12; //Regain 10 health
            Destroy(coll.gameObject); //Destroy the enemy
        }
    }
}