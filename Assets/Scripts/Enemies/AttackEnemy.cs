using UnityEngine;
using System.Collections;
using System;

public class AttackEnemy : Enemy {

    // Use this for initialization
    protected override void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
	}

    /// <summary>
    /// to check if the enemy is colliding with a player
    /// </summary>
    public override void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {

            //Debug.Log(name + " is hitting " + coll.transform.name);
            //health -= coll.GetComponent<Enemy>().Damage;
            rb.AddForce(-1 * transform.up * knockbackScale);
            coll.transform.GetComponent<Rigidbody2D>().AddForce(transform.up * knockbackScale);
            //coll.GetComponent<PlayerSlime>().Health--;
        }
    }
}
