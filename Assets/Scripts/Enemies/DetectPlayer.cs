using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// detect if a player entrs the collider and assign it
    /// </summary>
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            this.GetComponentInParent<AttackEnemy>().target = coll.gameObject;
        }
    }
}
