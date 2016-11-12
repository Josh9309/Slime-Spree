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
        public float horizontalInput = 0, verticalInput = 0, attack1Input = 0, attack2Input = 0; //sets up variables to hold inputs
        public string HORIZONTAL_AXIS, VERTICAL_AXIS, ATTACK1_AXIS, ATTACK2_AXIS; //sets up variable to hold input_axis
        public string PAUSE_AXIS = "Pause"; //sets the pause input Axis

        //sets up booleans for btn_input and sets them to false
        public bool attack1 = false;
        public bool attack2 = false;
        public bool pause = false;

        public void ConfigureInput(int playerNum)
        {
            //sets up the input axes based on the players number
            switch (playerNum)
            {
                case 1:
                    HORIZONTAL_AXIS = "P1_Horizontal";
                    VERTICAL_AXIS = "P1_Vertical";
                    ATTACK1_AXIS = "P1_Attack1";
                    ATTACK2_AXIS = "P1_Attack2";
                    break;

                case 2:
                    HORIZONTAL_AXIS = "P2_Horizontal";
                    VERTICAL_AXIS = "P2_Vertical";
                    ATTACK1_AXIS = "P2_Attack1";
                    ATTACK2_AXIS = "P2_Attack2";
                    break;

                case 3:
                    HORIZONTAL_AXIS = "P3_Horizontal";
                    VERTICAL_AXIS = "P3_Vertical";
                    ATTACK1_AXIS = "P3_Attack1";
                    ATTACK2_AXIS = "P3_Attack2";
                    break;

                case 4:
                    HORIZONTAL_AXIS = "P4_Horizontal";
                    VERTICAL_AXIS = "P4_Vertical";
                    ATTACK1_AXIS = "P4_Attack1";
                    ATTACK2_AXIS = "P4_Attack2";
                    break;
            }
        }

        public void ResetBtns()
        {
            //resets booleans for btn_input to false
            attack1 = false;
            attack2 = false;
            pause = false;
        }
    }
    #endregion

    #region Attributes
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected int playerNum = 1;
    [SerializeField] protected int attack1Down;
    [SerializeField] protected int slimeShotDamage;
    [SerializeField] protected int slimeAttack2Damage;
    [SerializeField] protected float slimeShotCooldown, slimeAttack2Cooldown;
    [SerializeField] protected float damage1Cooldown, damage2Cooldown;
    [SerializeField] protected SlimeType slimerType;
    [SerializeField] protected float slimeShotRange;
    [SerializeField] private GameObject reticleSprite; //The reticle sprite
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

        Instantiate(reticleSprite, gameObject.transform.position, Quaternion.identity); //Instantiate the player's reticle
    }
	
	//Update is called once per frame
	protected void Update()
    {
        Aim(); //Aim
	}

	// Update is called once per frame
	void FixedUpdate () {
        GetInput();
        Move();

	}

    protected void GetInput()
    {
        //gets all value based input checks
        input.horizontalInput = Input.GetAxis(input.HORIZONTAL_AXIS);
        input.verticalInput = Input.GetAxis(input.VERTICAL_AXIS);
        input.attack1Input = Input.GetAxis(input.ATTACK1_AXIS);
        input.attack2Input = Input.GetAxis(input.ATTACK2_AXIS);

        //button input checks
        if (!input.attack1)
        {
            input.attack1 = Input.GetButtonDown(input.ATTACK1_AXIS);
        }
        if (!input.attack2)
        {
            input.attack2 = Input.GetButtonDown(input.ATTACK2_AXIS);
        }
        if (!input.attack2)
        {
            input.attack2 = Input.GetButtonDown(input.ATTACK2_AXIS);
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
            rBody.velocity = new Vector2(input.horizontalInput * speed, input.verticalInput * speed);
        }
        else
        {
            rBody.velocity = Vector2.zero;
        }
    }

    protected virtual void SlimeShotAttack()
    {

    }

    protected virtual void Aim() //Aiming the slime's attack
    {
        if (Input.GetAxisRaw("HorizontalAim") != 0.0f || Input.GetAxisRaw("VerticalAim") != 0.0f) //If the player is aiming
        {
            reticleSprite.transform.position = new Vector2((gameObject.transform.position.x + Input.GetAxisRaw("HorizontalAim")) * slimeShotRange, (gameObject.transform.position.y - Input.GetAxisRaw("VerticalAim")) * slimeShotRange); //Update the reticle's position
        }
        else //If the player is not aiming
        {
            reticleSprite.transform.position = gameObject.transform.position; //Place the reticle at the player's position
        }
    }

    protected abstract void SlimeAttack2();

    protected abstract void SlimeTrap();
}
