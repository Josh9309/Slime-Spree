using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour {

    public int attackEnemies;
    public int goalEnemies;
    public int wave;
    int numPlayers;
    public Text text;
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
        //attackEnemies = LevelManager.Instance.attackEnemies.Count;
        //get number of goal enemies
        goalEnemies = LevelManager.Instance.goalEnemies.Count;
        //check number of players
        numPlayers = GameManager.Instance.players.Count;

        //if no enemies left
        if (goalEnemies <= 0)
        {
            //go to the next wave
            wave += 1;
            attackEnemies = (wave * numPlayers) + 5;
            goalEnemies = (wave * numPlayers) + 2 ;
            //spawn
            LevelManager.Instance.SpawnEnemies(attackEnemies, goalEnemies);
        }
        UpdateWave();
    }

    void UpdateWave()
    {
        text.text = "Wave: " + wave;
    }

  
}
