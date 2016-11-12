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
       
        wave = 1;

    }
	// Update is called once per frame
	void Update ()
    {
        //get number of attack enemies
        attackEnemies = LevelManager.Instance.attackEnemies.Count;
        //get number of goal enemies
        goalEnemies = LevelManager.Instance.goalEnemies.Count;
        //if no enemies left
        if (attackEnemies <= 0 && goalEnemies <= 0)
        {
            //go to the next wave
            wave += 1;
            LevelManager.Instance.SpawnEnemies();
        }

       // attackEnemies = wave * 3;
       // goalEnemies = wave * 4;

        //spawn attack enemies
       
	}
}
