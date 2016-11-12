using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlayerSlime : MonoBehaviour {
    public enum SlimeType { RED, BLUE, YELLOW, GREEN };

    #region InputSettings
    //this will setup the public inputSetting class
    [System.Serializable]
    public class InputSettings
    {
        public float delay = 0.3f; //delay for movement inputs
        public float horizontalInput = 0, verticalInput = 0, fireLeftInput = 0, fireRightInput = 0, horizontalAimAxis = 0, verticalAimAxis = 0; //sets up variables to hold inputs
        public string HORIZONTAL_AXIS, VERTICAL_AXIS, FIRELEFT_AXIS, FIRERIGHT_AXIS, HORIZONTAL_AIM_AXIS, VERTICAL_AIM_AXIS, TRAP_AXIS, ULTIMATE_AXIS; //sets up variable to hold input_axis
        public string PAUSE_AXIS = "Pause"; //sets the pause input Axis

        //sets up booleans for btn_input and sets them to false
        public bool trap = false;
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
                    FIRELEFT_AXIS = "P1_FireLeft";
                    FIRERIGHT_AXIS = "P1_FireRight";
                    HORIZONTAL_AIM_AXIS = "P1_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P1_VerticalAim";
                    TRAP_AXIS = "P1_Trap";
                    ULTIMATE_AXIS = "P1_Ultimate";
                    break;

                case 2:
                    HORIZONTAL_AXIS = "P2_Horizontal";
                    VERTICAL_AXIS = "P2_Vertical";
                    FIRELEFT_AXIS = "P2_FireLeft";
                    FIRERIGHT_AXIS = "P2_FireRight";
                    HORIZONTAL_AIM_AXIS = "P2_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P2_VerticalAim";
                    TRAP_AXIS = "P2_Trap";
                    ULTIMATE_AXIS = "P2_Ultimate";
                    break;

                case 3:
                    HORIZONTAL_AXIS = "P3_Horizontal";
                    VERTICAL_AXIS = "P3_Vertical";
                    FIRELEFT_AXIS = "P3_FireLeft";
                    FIRERIGHT_AXIS = "P3_FireRight";
                    HORIZONTAL_AIM_AXIS = "P3_HorizontalAim";
                    VERTICAL_AIM_AXIS = "P3_VerticalAim";
                    TRAP_AXIS = "P3_Trap";
                    ULTIMATE_AXIS = "P3_Ultimate";
                    break;

                case 4:
                    HORIZONTAL_AXIS = "P4_Horizontal";
                    VERTICAL_AXIS = "P4_Vertical";
                    FIRELEFT_AXIS = "P4_FireLeft";
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
            trap = false;
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
    [SerializeField] protected int slimeAttack2Damage;
    [SerializeField] protected float slimeShotCooldown, slimeAttack2Cooldown;
    [SerializeField] protected float damage1Cooldown, damage2Cooldown;
    [SerializeField] protected SlimeType slimerType;
    [SerializeField] protected float slimeShotRange;
    [SerializeField] private GameObject reticleSprite; //The reticle sprite
    [SerializeField] private GameObject slimeShotGameObject; //The slime shot prefab
    private GameObject reticle; //The reticle
    private SpriteRenderer reticleSR; //The reticle's sprite renderer
    private SpriteRenderer playerSR; //The reticle's sprite renderer
    private Rigidbody2D rBody;
    private InputSettings input = new InputSettings();
    #endregion

    #region Properties
    //get set hp
    public int Health
    {
        get { return health; }
    }

    //get set slime type
    public SlimeType getSlimeType {
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

    public SpriteRenderer PlayerSR //PlayerSR property
    {
        get
        {
            return playerSR; //Return the player's sprite renderer
        }
    }

    public GameObject Reticle //Reticle property
    {
        get
        {
            return reticle; //Return the reticle
        }
    }
    #endregion

    // Use this for initialization
    protected void Start()
    {
        //Assign body
        rBody = GetComponent<Rigidbody2D>();

        //turn off gravity on our rigidbody
        rBody.gravityScale = 0;

        //configure InputManager
        input.ConfigureInput(playerNum);

        reticle = (GameObject)Instantiate(reticleSprite, gameObject.transform.position, Quaternion.identity); //Instantiate the player's reticle
        reticleSR = reticle.GetComponent<SpriteRenderer>(); //Get the reticle's sprite renderer
        playerSR = GetComponent<SpriteRenderer>(); //Get the player's sprite renderer
    }
	
	//Update is called once per frame
	protected void Update()
    {
        Aim(); //Aim
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        GetInput();
        Move();
	}

    protected void GetInput()
    {
        //gets all value based input checks
        input.horizontalInput = Input.GetAxis(input.HORIZONTAL_AXIS);
        input.verticalInput = Input.GetAxis(input.VERTICAL_AXIS);
        input.fireLeftInput = Input.GetAxis(input.FIRELEFT_AXIS);
        input.fireRightInput = Input.GetAxis(input.FIRERIGHT_AXIS);
        input.horizontalAimAxis = Input.GetAxisRaw(input.HORIZONTAL_AIM_AXIS);
        input.verticalAimAxis = Input.GetAxisRaw(input.VERTICAL_AIM_AXIS);

        //button input checks
        if (!input.trap)
        {
            input.trap = Input.GetButtonDown(input.TRAP_AXIS);
        }
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

            if (input.fireLeftInput != 0 || input.fireRightInput != 0) //If the player is firing
            {
                Instantiate(slimeShotGameObject, transform.position, Quaternion.identity); //Fire the slime shot
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

    }

    protected abstract void SlimeAttack2Cooldown();

    protected abstract void SlimeUltimateCooldown();

    protected void ModHealth(int mod)
    {
        health += mod; //modifies the health 
        ScaleSlime(); //rescales the playerSlime
    }

    protected void ScaleSlime()
    {

    }
}
