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
                player = GameObject.Find("BlueSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.RED:
                player = GameObject.Find("RedSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.GREEN:
                player = GameObject.Find("GreenSlime(Clone)");
                break;

            case PlayerSlime.SlimeType.YELLOW:
                player = GameObject.Find("BlueSlime(Clone)");
                break;
        }

        slimePlayerScript = player.GetComponent<PlayerSlime>(); //Get the player's script
        slimeShotTarget = slimePlayerScript.Reticle.transform.position; //The target of the slime shot
        rBody = GetComponent<Rigidbody2D>(); //Get the slime shot's rigidbody
        shotSR = GetComponent<SpriteRenderer>(); //Get the slime shot's sprite renderer
        shotSR.color = slimePlayerScript.PlayerSR.color; //Set the shot's color to be the same as the player's color

        damage = slimePlayerScript.SlimeShotDamage;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rBody.velocity = transform.position + slimeShotTarget; //Add forces to shoot the slime shot
    }

    void OnTriggerEnter2D(Collider2D coll) //Collisions with the slime shot
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            Debug.Log("AWWWWW NO");
        }
    }
}
