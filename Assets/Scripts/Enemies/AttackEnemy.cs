using UnityEngine;
using System.Collections;
using System;

public class AttackEnemy : Enemy {

    [SerializeField] private float knockbackScale;

    // Use this for initialization
    public override void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	public override void Update () {
        Move();
	}

    /// <summary>
    /// to check if the enemy is colliding with a player
    /// </summary>
    public void OnCollisionEnter2D(Collision2D coll)
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
