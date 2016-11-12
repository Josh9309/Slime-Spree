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
    //--------------------

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();

        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public abstract void Update();

    /// <summary>
    /// responsible for seeking the enemy's target.
    /// This can be either the player for an agressive
    /// enemy, or the goal for a standard one
    /// </summary>
    public abstract void Seek();
}
