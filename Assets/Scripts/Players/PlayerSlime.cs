﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayerSlime : MonoBehaviour {
    public enum SlimeType { RED, BLUE, YELLOW, GREEN, NOCOLOR };

    #region InputSettings
    //this will setup the public inputSetting class
    [System.Serializable]
    public class InputSettings
    {
        public float delay = 0.3f; //delay for movement inputs
        public float horizontalInput = 0, verticalInput = 0, special = 0, fireRightInput = 0, horizontalAimAxis = 0, verticalAimAxis = 0; //sets up variables to hold inputs
        public string HORIZONTAL_AXIS, VERTICAL_AXIS, SPECIAL_AXIS, FIRERIGHT_AXIS, HORIZONTAL_AIM_AXIS, VERTICAL_AIM_AXIS, TRAP_AXIS, ULTIMATE_AXIS; //sets up variable to hold input_axis
        public string PAUSE_AXIS = "Pause"; //sets the pause input Axis

        //sets up booleans for btn_input and sets them to false
        public bool ultimate = false;
        public bool pause = false;

        public void ConfigureInput(int playerNum)
        {
            //sets up the input axes based on the players number
            switch (playerNum)
            {
                case 1:
                    HORIZONTAL_AXIS = "P1_Horizontal";
                    VERTICAL_AXIS = "P1_Vertical";
                    SPECIAL_AXIS = "P1_Special";
                    FIRERIGHT_AXIS = "P1_FireRight";
                    HORIZONTAL_AIM_AXIS = "P1_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P1_VerticalAim";
                    ULTIMATE_AXIS = "P1_Ultimate";
                    break;

                case 2:
                    HORIZONTAL_AXIS = "P2_Horizontal";
                    VERTICAL_AXIS = "P2_Vertical";
                    SPECIAL_AXIS = "P2_Special";
                    FIRERIGHT_AXIS = "P2_FireRight";
                    HORIZONTAL_AIM_AXIS = "P2_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P2_VerticalAim";
                    TRAP_AXIS = "P2_Trap";
                    ULTIMATE_AXIS = "P2_Ultimate";
                    break;

                case 3:
                    HORIZONTAL_AXIS = "P3_Horizontal";
                    VERTICAL_AXIS = "P3_Vertical";
                    SPECIAL_AXIS = "P3_Special";
                    FIRERIGHT_AXIS = "P3_FireRight";
                    HORIZONTAL_AIM_AXIS = "P3_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P3_VerticalAim";
                    TRAP_AXIS = "P3_Trap";
                    ULTIMATE_AXIS = "P3_Ultimate";
                    break;

                case 4:
                    HORIZONTAL_AXIS = "P4_Horizontal";
                    VERTICAL_AXIS = "P4_Vertical";
                    SPECIAL_AXIS = "P4_Special";
                    FIRERIGHT_AXIS = "P4_FireRight";
                    HORIZONTAL_AIM_AXIS = "P4_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P4_VerticalAim";
                    TRAP_AXIS = "P4_Trap";
                    ULTIMATE_AXIS = "P4_Ultimate";
                    break;
            }
        }

        public void ResetBtns()
        {
            //resets booleans for btn_input to false
            ultimate = false;
            pause = false;
        }
    }
    #endregion

    #region Attributes
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] private float deceleration = 1.5f;
    [SerializeField] protected int playerNum = 1;
    [SerializeField] protected int attack1Down;
    [SerializeField] protected int slimeShotDamage;
    [SerializeField] protected int slimeAttack2Damage, slimeUltimateDamage;
    [SerializeField] protected float slimeShotCooldown, slimeAttack2Cooldown, slimeUltimateCooldown;
    [SerializeField] protected SlimeType slimerType;
    [SerializeField] protected float slimeShotRange;
    [SerializeField] private GameObject reticleSprite; //The reticle sprite
    [SerializeField] private GameObject slimeShotGameObject; //The slime shot prefab
    [SerializeField] private float knockbackScale;
    protected bool slimeAttack2Available = true;
    protected bool slimeUltimateAvailable = true;
    private GameObject reticle; //The reticle
    private SpriteRenderer reticleSR; //The reticle's sprite renderer
    private SpriteRenderer playerSR; //The reticle's sprite renderer
    private Rigidbody2D rBody;
    private Vector3 fullScale;
    protected InputSettings input = new InputSettings();

    #endregion

    #region Properties
    //get set hp
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    //get set slime type
    public SlimeType SlimerType {
        get { return slimerType; }
        set
        {
            slimerType = value;
        }
    }

    public int PlayerNum
    {
        get { return playerNum; }
        set
        {
            if (value < 1 || value > 4)
            {
                Debug.LogError("Tried to set playerNum to an invalid value");
            }
            else { playerNum = value; }
        }
    }

    public int SlimeShotDamage
    {
        get { return slimeShotDamage; }
    }

    public GameObject Reticle //Reticle property
    {
        get
        {
            return reticle; //Return the reticle
        }
    }

    public int SlimeAttack2Damage
    {
        get { return slimeAttack2Damage; }
    }

    public int SlimeUltimateDamage
    {
        get { return slimeUltimateDamage; }
    }
    #endregion

    // Use this for initialization
    protected virtual void Start()
    {
        //Assign body
        rBody = GetComponent<Rigidbody2D>();

        //turn off gravity on our rigidbody
        rBody.gravityScale = 0;

        //configure InputManager
        input.ConfigureInput(playerNum);

        reticle = (GameObject)Instantiate(reticleSprite, transform.position, Quaternion.identity); //Instantiate the player's reticle
        reticleSR = reticle.GetComponent<SpriteRenderer>(); //Get the reticle's sprite renderer
        playerSR = GetComponent<SpriteRenderer>(); //Get the player's sprite renderer

        //save fullscale
        fullScale = transform.localScale;
    }
	
	//Update is called once per frame
	protected virtual void Update()
    {
        Aim(); //Aim
        SlimeShotCooldown(); //The slime shot's cooldown

        if (health <= 0) //If the player has no health
        {
            Destroy(gameObject); //He's dead, Jim!
        }

        if (health > 200) //If the player's health is over 200
        {
            health = 200; //Cap the health
        }
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        GetInput();
        Move();
        ScaleSlime();
	}

    protected void GetInput()
    {
        //gets all value based input checks
        input.horizontalInput = Input.GetAxis(input.HORIZONTAL_AXIS);
        input.verticalInput = Input.GetAxis(input.VERTICAL_AXIS);
        input.special = Input.GetAxis(input.SPECIAL_AXIS);
        input.fireRightInput = Input.GetAxis(input.FIRERIGHT_AXIS);
        input.horizontalAimAxis = Input.GetAxisRaw(input.HORIZONTAL_AIM_AXIS);
        input.verticalAimAxis = Input.GetAxisRaw(input.VERTICAL_AIM_AXIS);

        //button input checks
        if (!input.ultimate)
        {
            input.ultimate = Input.GetButtonDown(input.ULTIMATE_AXIS);
        }
        if (!input.pause)
        {
            input.pause = Input.GetButtonDown(input.PAUSE_AXIS);
        }
    }

    protected void Move()
    {
        if(Mathf.Abs(input.horizontalInput) > input.delay || Mathf.Abs(input.verticalInput) > input.delay)
        {
            rBody.AddForce(new Vector2(input.horizontalInput * 20, -input.verticalInput * 20));
            float velx = Mathf.Clamp(rBody.velocity.x, -speed, speed);
            float vely = Mathf.Clamp(rBody.velocity.y, -speed, speed);
            rBody.velocity = new Vector2(velx, vely);
            transform.up = rBody.velocity;
        }
        else
        {
            rBody.AddForce(new Vector2(-rBody.velocity.x * deceleration, -rBody.velocity.y * deceleration));
            float velx = Mathf.Clamp(rBody.velocity.x, -speed, speed);
            float vely = Mathf.Clamp(rBody.velocity.y, -speed, speed);
            rBody.velocity = new Vector2(velx, vely);
        }
        //Debug.Log(rBody.velocity);
    }

    protected virtual void Aim() //Aiming the slime's attack
    {
        if (input.horizontalAimAxis != 0.0f || input.verticalAimAxis != 0.0f) //If the player is aiming
        {
            float xLocation = Mathf.Cos(Mathf.Atan2(input.verticalAimAxis, input.horizontalAimAxis)) * slimeShotRange; //Set the reticle's x location
            float yLocation = Mathf.Cos(Mathf.Atan2(input.horizontalAimAxis, input.verticalAimAxis)) * slimeShotRange; //Set the reticle's y location

            reticleSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 1); //Turn up the reticle's alpha

            reticle.transform.position = new Vector2(gameObject.transform.position.x + xLocation, gameObject.transform.position.y - yLocation); //Update the reticle's position

            if (input.fireRightInput != 0 && slimeShotCooldown <= 0.0f) //If the player is firing
            {
                //SoundManager.instance.RandomizeSFx(shoot, shoot2);
                Instantiate(slimeShotGameObject, transform.position, Quaternion.identity); //Fire the slime shot
                health--; //Decrement the player's health
                slimeShotCooldown = 0.65f; //Set the damage cooldown
            }
        }
        else //If the player is not aiming
        {
            reticleSR.color = new Color(playerSR.color.r, playerSR.color.g, playerSR.color.b, 0); //Turn down the reticle's alpha

            reticle.transform.position = gameObject.transform.position; //Place the reticle at the player's position
        }
    }

    protected abstract void SlimeAttack2();

    protected abstract void SlimeUltimate();

    protected virtual void SlimeShotCooldown()
    {
        if (slimeShotCooldown > 0) //If the slime shot has been activated
        {
            slimeShotCooldown -= Time.deltaTime; //Decrement the cooldown
        }
    }

    protected virtual IEnumerator SlimeAttack2Cooldown()
    {
        slimeAttack2Available = false;

        yield return new WaitForSeconds(slimeAttack2Cooldown);
        slimeAttack2Available = true;
    }

    protected virtual IEnumerator SlimeUltimateCooldown()
    {
        slimeUltimateAvailable = false;

        yield return new WaitForSeconds(slimeUltimateCooldown);
        slimeUltimateAvailable = true;
    }

    protected void ModHealth(int mod)
    {
        health += mod; //modifies the health 
        ScaleSlime(); //rescales the playerSlime
    }

    protected void ScaleSlime()
    {//min size is 1 as scale, max is 3.58, range between is 2.58:: 1 hp = .0258 scale
        //find scale range
        float range = (fullScale.x - 1.000f) / 100.000f;

        //make scale be that percent
        float scaled = 1.0000f + (range * health);
        transform.localScale = new Vector3(scaled, scaled, 1);
      
    }

    /// <summary>
    /// detect if the player is colliding with anything
    /// </summary>
    public void OnTriggerEnter2D(Collider2D coll)
    {
        // knockback
        //Vector3 moment = rBody.velocity + coll.GetComponent<Rigidbody2D>().velocity;
        //Vector3 unitMoment = moment.normalized;
        
        if (coll.gameObject.tag == "Health") //If the player is picking up health
        {
            health += 5; //Give the player health

            Destroy(coll.gameObject); //Destroy the health pickup
        }
    }
}
