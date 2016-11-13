using UnityEngine;
using System.Collections;

public class FreezeShot : SlimeShot {

	// Use this for initialization
	protected override void Awake () {
        base.Awake();

        damage = 0;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void explode()
    {
        if (Mathf.Abs(slimeShotTarget.x - rBody.position.x) / 7 <= 0.05f && Mathf.Abs(slimeShotTarget.y - rBody.position.y) / 7 <= 0.05f)
        {
            Destroy(this.gameObject); //Destroy the slime shot
        }
    }
}
