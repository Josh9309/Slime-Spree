using UnityEngine;
using System.Collections;

public class GoalEnemy : Enemy {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

    /// <summary>
    /// Goal seeker should always be seeking the goal
    /// </summary>
    public override void Seek()
    {

    }
}
