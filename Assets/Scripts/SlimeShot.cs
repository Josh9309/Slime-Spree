using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlimeShot : MonoBehaviour {
    #region Attributes
    protected int damage;
    protected Vector3 slimeShotTarget;
    protected GameObject player;
    protected PlayerSlime slimePlayerScript;
    [SerializeField] protected PlayerSlime.SlimeType slimeType;
    [SerializeField] protected float shotSpeed;
    [SerializeField] protected float knockbackScale;
    protected Rigidbody2D rBody;
    protected Vector3 shotDirection;
    #endregion

    // Use this for initialization
    protected virtual void Awake ()
    {
        switch (slimeType)
        {
            case PlayerSlime.SlimeType.BLUE:
                player = GameObject.Find("BlueSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.RED:
                player = GameObject.Find("RedSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.GREEN:
                player = GameObject.Find("GreenSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.YELLOW:
                player = GameObject.Find("YellowSlime(Clone)");
                break;
        }

        slimePlayerScript = player.GetComponent<PlayerSlime>(); //Get the player's script
        slimeShotTarget = slimePlayerScript.Reticle.transform.position; //The target of the slime shot
        rBody = GetComponent<Rigidbody2D>(); //Get the slime shot's rigidbody

        damage = slimePlayerScript.SlimeShotDamage;
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        shotDirection = (slimeShotTarget - transform.position).normalized;

        rBody.AddForce(shotDirection * shotSpeed); //Add forces to shoot the slime shot

        explode();
    }

    /// <summary> 
    /// calculates when to "explode" the shot, all this
    /// is is tells it what to do when it hits the reticle
    /// </summary>
    protected virtual void explode()
    {
        if (Mathf.Abs(slimeShotTarget.x - rBody.position.x) / 7 <= 0.05f && Mathf.Abs(slimeShotTarget.y - rBody.position.y) / 7 <= 0.05f)
        {
            Destroy(this.gameObject); //Destroy the slime shot
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll) //Collisions with the slime shot
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            Enemy enemySlime = coll.gameObject.GetComponent<Enemy>();
            if (enemySlime.SlimeType != slimePlayerScript.SlimerType)
            {
                enemySlime.health -= damage;
                // apply knockback
                coll.transform.GetComponent<Rigidbody2D>().AddForce(shotDirection * knockbackScale);
               
            }

            Destroy(gameObject); //Destroy the slime shot
        }
    }
}
