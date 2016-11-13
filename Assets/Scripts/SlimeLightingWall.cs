using UnityEngine;
using System.Collections;

public class SlimeLightingWall : MonoBehaviour {
    #region Attributes
    private int damage;
    private Enemy enemySlime;
    private YellowSlimePlayer yellowSlime;
    private bool active = true;
    [SerializeField] private float duration = 7;
    #endregion

    // Use this for initialization
    void Start () {
        yellowSlime = GameObject.Find("YellowSlime(Clone)").GetComponent<YellowSlimePlayer>();
        damage = yellowSlime.SlimeUltimateDamage;
        StartCoroutine(LightingWallActive());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator LightingWallActive()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy") //if enemy hits the wall
        {
            enemySlime = col.gameObject.GetComponent<Enemy>();
            Debug.Log(enemySlime.name + " has been hit for " + damage);
            enemySlime.health -= damage; //enemy takes damage

            Rigidbody2D enemyRBody = enemySlime.GetComponent<Rigidbody2D>();
            enemyRBody.AddForce(-enemyRBody.velocity * 3);
        }
    }
}
