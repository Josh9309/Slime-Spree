using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {


    //-----ATTRIBUTES-----
    [SerializeField] protected float knockbackScale;
    public float moveSpeed;
    public int health;
    public int damage;
    protected Rigidbody2D rb;
    protected SpriteRenderer enemySprite;
    public GameObject target;
    public float maxSpeed;
    [SerializeField] protected PlayerSlime.SlimeType slimeType;
    protected bool canTriggerGoal;
    [SerializeField] private GameObject healthDrop; //Drop health for the player
    protected bool frozen = false;
    [SerializeField] protected float freezeDuration;
    protected float freezeTimer = 0.0f;
    [SerializeField] private GameObject freeze;
    //public Vector3 startPos = new Vector3(0, 0 ,0);
    //--------------------
    #region Properties
    public PlayerSlime.SlimeType SlimeType
    {
        get { return slimeType; }
    }
    public int Damage
    {
        get { return damage; }
    }

    public bool CanTriggerGoal
    {
        get { return canTriggerGoal; }
    }

    public bool Frozen
    {
        get { return frozen; }
        set { frozen = value; }
    }
    #endregion

    // Use this for initialization
    protected virtual void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        //transform.position = startPos;
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Goal");
        }
        if (frozen)
        {
            freeze.transform.position = transform.position;
        }
        Move();
        CheckIsAlive();
    }

    /// <summary>
    /// responsible for seeking the enemy's target.
    /// This can be either the player for an agressive
    /// enemy, or the goal for a standard one
    /// </summary>
    public Vector3 Seek()
    {
        Vector3 offset = target.transform.position - this.transform.position;
        Vector3 unitOffset = offset.normalized;
        return unitOffset;
    }

    /// <summary>
    /// responsible for moving the enemy
    /// </summary>
    public void Move()
    {
        if (frozen == false)
        {
            freezeTimer = 0.0f;
            Vector3 unitOffset = Seek();
            transform.up = unitOffset;
            Vector3 acceleration = unitOffset * moveSpeed;
            rb.AddForce(acceleration);
            float velX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            float velY = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);

            rb.velocity = new Vector3(velX, velY, 0);
        } 
    }

    public IEnumerator freezeEnemy()
    {
        GameObject freeze2 = GameObject.Instantiate(freeze, transform.position, Quaternion.identity) as GameObject;
        freeze2.transform.position = transform.position;
        rb.velocity = Vector3.zero;
        frozen = true;

        yield return new WaitForSeconds(freezeDuration);

        Destroy(freeze2);
        frozen = false;
    }

    private void CheckIsAlive()
    {
        if( health <= 0) //If the enemy has no health
        {
            if(50 >= Random.Range(0, 101)) //If the enemy should drop health
            {
                Instantiate(healthDrop, transform.position, Quaternion.identity); //Drop health
            }

            Destroy(gameObject); //This kills the enemy
        }
    }

    /// <summary>
    /// to check if the enemy is colliding with a player, or projectile
    /// </summary>
    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
    }
}
