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
            Destroy(coll.gameObject); //Destroy the enemy
            Destroy(gameObject); //Destroy this gameobject
        }
    }
}
