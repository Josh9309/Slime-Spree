using UnityEngine;
using System.Collections;
using System;

public class RedSlimePlayer : PlayerSlime {
    #region Attributes
    [SerializeField] private float SlimeBurstRange = 2;
    [SerializeField] private int SlimeBurstCost = 20;
    [SerializeField] private GameObject SlimeBurstArea;
    [SerializeField] private GameObject SlimeBurstSprite;
    [SerializeField] private Animator slimeBurstAnim;
    //2 clips for each effect
    public AudioClip redUlt;
    public AudioClip redUlt2;
    public AudioClip redSpec;
    public AudioClip redSpec2;
    private Behaviour halo; //Ultimate halo

    #endregion

    // Use this for initialization
    new void Start()
    {
        base.Start(); //Call the base start method
        halo = (Behaviour)GetComponent("Halo"); //Get the halo
        halo.enabled = false;
    }
	
	// Update is called once per frame
	new void Update()
    {
        base.Update(); //Call the base update method
        SlimeUltimate();
        SlimeAttack2();
        input.ResetBtns();
	}

    
    protected override void SlimeAttack2()
    {
        if (input.special != 0 && health > SlimeBurstCost && slimeAttack2Available)
        {
            //sound effect
            SoundManager.instance.RandomizeSFx(redSpec, redSpec2);
            slimeBurstAnim.Play("SlimeBurst");
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, SlimeBurstRange);
            Debug.DrawLine(gameObject.transform.position, new Vector3(transform.position.x + SlimeBurstRange, transform.position.y, transform.position.z), Color.black, 4);

            foreach (Collider2D thing in cols)
            {
                if (thing.tag == "Player")// the slime burst has hit a player
                {
                    PlayerSlime slimePlayer = thing.GetComponent<PlayerSlime>();

                    if (slimePlayer.SlimerType != PlayerSlime.SlimeType.RED)
                    {
                        slimePlayer.Health += 100; //kill enemy
                    }

                }
            }

            ModHealth(-SlimeBurstCost); //decrease players health by the cost of the attack
            StartCoroutine(SlimeAttack2Cooldown());
        }
    }

    //Red Slime SlimeBurst AOE Attack
    protected override void SlimeUltimate()
    {
        if (input.ultimate && health > SlimeBurstCost && slimeUltimateAvailable)
        {
            SoundManager.instance.RandomizeSFx(redUlt, redUlt2);

            StartCoroutine(DisplaySlimeBurst());

            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, SlimeBurstRange);
            Debug.DrawLine(gameObject.transform.position, new Vector3(transform.position.x + SlimeBurstRange, transform.position.y, transform.position.z), Color.black, 4);

            foreach (Collider2D thing in cols)
            {
                if (thing.tag == "Enemy")// the slime burst has hit a enemy
                {
                    Enemy slimeEnemy = thing.GetComponent<Enemy>();

                    //check to see if enemy slime is not same type as player
                    if (slimeEnemy.SlimeType != PlayerSlime.SlimeType.RED)
                    {
                        slimeEnemy.health = 0; //kill enemy
                    }
                }
            }

            ModHealth(-SlimeBurstCost); //decrease players health by the cost of the attack
            StartCoroutine(SlimeUltimateCooldown());
        }
    }

    private IEnumerator DisplaySlimeBurst()
    {
        halo.enabled = true;

        yield return new WaitForSeconds(0.5f);

        halo.enabled = false;
    }

}
