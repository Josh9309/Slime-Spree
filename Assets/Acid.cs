using UnityEngine;
using System.Collections;

public class Acid : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll) //Collisions with the slime shot
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            if (LevelManager.Instance.goalEnemies.Remove(coll.gameObject)) //Remove the gameobject from the list
            {
                Destroy(coll.gameObject); //This kills the enemy
            }
            else if (LevelManager.Instance.attackEnemies.Remove(coll.gameObject)) //Remove the gameobject from the list
            {
                Destroy(coll.gameObject); //This kills the enemy
            }

            Destroy(gameObject); //Destroy this gameobject
        }
    }
}
