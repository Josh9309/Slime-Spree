﻿using UnityEngine;
using System.Collections;

public class FreezeShot : SlimeShot {

    [SerializeField] private float freezeRadius;

    // Use this for initialization
    protected override void Awake () {
        base.Awake();

        damage = 0;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void explode()
    {
        if (Mathf.Abs(slimeShotTarget.x - rBody.position.x) / 7 <= 0.05f && Mathf.Abs(slimeShotTarget.y - rBody.position.y) / 7 <= 0.05f)
        {
            // get all colliders
            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, freezeRadius);

            // check each collider to see if its an enemy
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].transform.tag == "Enemy")
                {
                    
                    StartCoroutine( colls[i].transform.GetComponent<Enemy>().freezeEnemy(this.gameObject));
                    coroutineRunning = true;
                }
            }
            if (!coroutineRunning)
            {
                Destroy(gameObject);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            Enemy enemySlime = coll.gameObject.GetComponent<Enemy>();
            if (enemySlime.SlimeType != slimePlayerScript.SlimerType)
            {
                explode();
            }
        }
    }
}
