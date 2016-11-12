﻿using UnityEngine;
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
    [SerializeField] private float shotSpeed;
    private Rigidbody2D rBody;
    private Vector3 shotDirection;
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

        damage = slimePlayerScript.SlimeShotDamage;
    }
	
	// Update is called once per frame
	void Update ()
    {
        shotDirection = (slimeShotTarget - transform.position).normalized;

        rBody.AddForce(shotDirection * 10); //Add forces to shoot the slime shot

        if (Mathf.Abs(slimeShotTarget.x - rBody.position.x) / 7 <= 0.05f && Mathf.Abs(slimeShotTarget.y - rBody.position.y) / 7 <= 0.05f)
        {
            Debug.Log(new Vector2(Mathf.Abs(slimeShotTarget.x - rBody.position.x), Mathf.Abs(slimeShotTarget.y - rBody.position.y)));
            Destroy(this.gameObject); //Destroy the slime shot
        }
    }

    void OnTriggerEnter2D(Collider2D coll) //Collisions with the slime shot
    {
        if (coll.gameObject.tag == "Enemy") //If the slime is colliding with the enemy
        {
            Enemy enemySlime = GetComponent<Enemy>();
            enemySlime.health -= damage;
            Destroy(this.gameObject); //Destroy the slime shot
        }
    }
}
