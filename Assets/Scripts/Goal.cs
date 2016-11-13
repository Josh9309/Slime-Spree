using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public int health;
    private Vector3 fullScale;

    // Use this for initialization
    void Start () {
        fullScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// for calculating physics stuff
    /// </summary>
    private void FixedUpdate()
    {
        ScaleGoal();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("Colliding");
        if (coll.gameObject.tag == "Enemy" && coll.gameObject.GetComponent<Enemy>().CanTriggerGoal == true)
        {
            //Debug.Log("Enemy " + coll.transform.name);
            // decrement health by amount of damage that enemy does
            health -= coll.transform.GetComponent<Enemy>().Damage;

            // delete enemy that runs into it
            Destroy(coll.gameObject);
        }
    }

    protected void ScaleGoal()
    {//min size is 1 as scale, max is 3.58, range between is 2.58:: 1 hp = .0258 scale
        //find scale range
        float range = (fullScale.x - 1.000f) / 100.000f;

        //make scale be that percent
        float scaled = 1.0000f + (range * health);
        transform.localScale = new Vector3(scaled, scaled, 1);

    }
}
