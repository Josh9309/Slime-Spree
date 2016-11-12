using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlimeShot : MonoBehaviour {
    #region Attributes
    private int damage;
    private Vector3 slimeShotTarget;
    private GameObject player;
    private PlayerSlime slimePlayerScript;
    [SerializeField] private PlayerSlime.SlimeType slimeType;
    private Rigidbody2D rBody;
    private SpriteRenderer shotSR; //The slime shot's sprite renderer
    #endregion

    // Use this for initialization
    void Awake ()
    {
        switch (slimeType)
        {
            case PlayerSlime.SlimeType.BLUE:
                player = GameObject.Find("BlueSlime");
                break;

            case PlayerSlime.SlimeType.RED:
                player = GameObject.Find("RedSlime");
                break;

            case PlayerSlime.SlimeType.GREEN:
                player = GameObject.Find("GreenSlime");
                break;

            case PlayerSlime.SlimeType.YELLOW:
                player = GameObject.Find("BlueSlime");
                break;
        }
        damage = slimePlayerScript.SlimeShotDamage;
        //slimeShotTarget = player.transform.Find("Reticle").transform.position;

        rBody = GetComponent<Rigidbody2D>(); //Get the slime shot's rigidbody
        slimePlayerScript = player.GetComponent<PlayerSlime>(); //Get the player's script
        shotSR = GetComponent<SpriteRenderer>(); //Get the slime shot's sprite renderer
        shotSR.color = slimePlayerScript.PlayerSR.color; //Set the shot's color to be the same as the player's color
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(slimeShotTarget);
        rBody.velocity = transform.position - player.GetComponentInChildren<GameObject>().transform.position; //Add forces to shoot the slime shot
    }

    void OnTriggerEnter2D(Collider2D coll) //Collisions with the slime shot
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            Debug.Log("AWWWWW NO");
        }
    }
}
