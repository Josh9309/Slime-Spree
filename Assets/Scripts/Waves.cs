using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour {

    public int attackEnemies;
    public int goalEnemies;
    public int wave;

    //spawn types 
    // Use this for initialization
    void Start()
    {

        //attackEnemies = GameObject.FindGameObjectsWithName("AttackEnemy");
        goalEnemies = 0;
        wave = 1;

    }
	// Update is called once per frame
	void Update ()
    {
        //if no enemies left
	    if(attackEnemies <= 0 && goalEnemies <= 0)
        {
            //go to the next wave
            wave += 1;
        }

        attackEnemies = wave * 3;
        goalEnemies = wave * 4;
	}
}
