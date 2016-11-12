using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {


    //-----ATTRIBUTES-----
    public float moveSpeed;
    public int health;
    public int damage;
    protected Rigidbody2D rb;
    public float knockback;
    protected SpriteRenderer enemySprite;
    public GameObject target;
    //public Vector3 startPos = new Vector3(0, 0 ,0);
    //--------------------

    // Use this for initialization
    public virtual void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        //transform.position = startPos;
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public abstract void Update();

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
        rb.velocity = unitOffset * moveSpeed;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
    }
}
