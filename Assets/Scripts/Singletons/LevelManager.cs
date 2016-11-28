using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Loads all scenes as levels and stores their data
/// </summary>
public class LevelManager : Singleton<LevelManager>
{
	#region Fields
	public List<GameObject> levels = new List<GameObject>();
    public List<GameObject> goalEnemies = new List<GameObject>();
    public List<GameObject> attackEnemies = new List<GameObject>();
    public GameObject goalEnemyPrefab;
    public GameObject attackEnemyPrefab;
    private int level = 0;
    private float maxXSpawn = 35;
    private float maxYSpawn = 20;
	#endregion

	#region Properties

	#endregion

	protected LevelManager(){}

	void Awake()
	{
		
	}

    void Update()
    {
        //foreach (GameObject g in goalEnemies)
        //{
        //    if (g.transform.name.Contains("Missing"))
        //    {
        //        Debug.Log("AGEasgsg");
        //        goalEnemies.Remove(g); //Remove the gameobject from the list
        //    }
        //}
        //if (goalEnemies.Contains((GameObject)"Missing") && gameObject.name.Contains("Missing"))
        //{
        //    
        //}
    }

    /// <summary>
    /// starts round, and moves us to the next level
    /// </summary>
    public void NextLevel()
    {
        //turn off current level
        levels[level].SetActive(false);

        //up the current level
        level++;

        if (level == levels.Count) //check if we are at the end of the levels
        {
            level = 0; //goto beginign again
        }

        //set the new current level active
        levels[level].SetActive(true);
    }

    /// <summary>
    /// spawns all enemies in the current wave
    /// should be called at the end of each wave
    /// i.e when all enemies in the previous wave
    /// are killed
    /// </summary>
    public void SpawnEnemies(int attack, int goal)
    {
        // assign each goalEnemy to the goalEnemy list
        // makes 'attack' number of enemies

        for (int i = 0; i < attack; i++)
        {
            // calculate a random position around the centroid, assign each enemy a random pos
            Vector3 randPos = new Vector3(Random.Range(8.5f, maxXSpawn) * Mathf.Ceil(Random.Range(-1, 2)),
                                          Random.Range(5f, maxYSpawn) * Mathf.Ceil(Random.Range(-1, 2)),
                                          0);


            goalEnemies.Add((GameObject)Instantiate(goalEnemyPrefab, randPos, Quaternion.identity));
            goalEnemies[i].GetComponent<GoalEnemy>().target = GameObject.FindGameObjectWithTag("Goal");
        }

        // spawn enemies that attack the player, this will always be 
        for (int i = 0; i < goal; i++)
        {
            // calculate a random position around the centroid, assign each enemy a random pos
            Vector3 randPos = new Vector3(Random.Range(8.5f, maxXSpawn) * Mathf.Ceil(Random.Range(-1, 2)),
                                          Random.Range(5f, maxYSpawn) * Mathf.Ceil(Random.Range(-1, 2)),
                                          0);

            attackEnemies.Add((GameObject)Instantiate(attackEnemyPrefab, randPos, Quaternion.identity));
            attackEnemies[i].GetComponent<AttackEnemy>().target = GameManager.Instance.players[Random.Range(0, 4)];
        }
    }
}
