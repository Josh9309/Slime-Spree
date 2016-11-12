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
        Vector3 unitOffset = Seek();
        transform.up = unitOffset;
        Vector3 acceleration = unitOffset * moveSpeed;
        rb.AddForce(acceleration);
        float velX = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        float velY = Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed);

        rb.velocity = new Vector3(velX, velY, 0);
        //rb.velocity = unitOffset * moveSpeed;
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }

    private void CheckIsAlive()
    {
        if( health <= 0)
        {
            Debug.Log("Enemy has died");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// to check if the enemy is colliding with a player, or projectile
    /// </summary>
    public virtual void OnCollisionEnter2D(Collision2D coll)
    {
        // if enemies collide with a projectile decrement their health
        // and destroy the prjectile
        if (coll.transform.tag == "Projectile")
        {
            rb.AddForce(coll.transform.up * knockbackScale);

            health--;
        }
    }
}
