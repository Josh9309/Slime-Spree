using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Enemy" && coll.GetComponent<Enemy>().CanTriggerGoal == true)
        {
            // decrement health by amount of damage that enemy does
            health -= coll.GetComponent<Enemy>().Damage;
        }
    }
}
