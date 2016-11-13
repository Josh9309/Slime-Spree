using UnityEngine;
using System.Collections;
using System;

public class YellowSlimePlayer : PlayerSlime {
    #region Attributes
    [SerializeField] private int slimeWallCost;
    [SerializeField] private GameObject slimeLightingPrefab;
    [SerializeField]
    private GameObject electricShotObject;
    private GameObject slimeLightingWall;

    public AudioClip yellowLightning;
    public AudioClip yellowLightning2;
    #endregion

    // Use this for initialization
    protected override void Start()
    {
        base.Start(); //Call the base start method
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); //Call the base update method
        SlimeAttack2();
        SlimeUltimate();
        input.ResetBtns();
    }

    protected override void SlimeAttack2()
    {
        if (input.special != 0.0f && health > 11 && slimeAttack2Available) //Don't die
        {
            health -= 10;
            Instantiate(electricShotObject, transform.position, Quaternion.identity);
            StartCoroutine(SlimeAttack2Cooldown());
        }
    }

    protected override void SlimeUltimate()
    {
        if(input.ultimate && health > slimeWallCost && slimeUltimateAvailable)
        {
            SoundManager.instance.RandomizeSFx(yellowLightning, yellowLightning2);
            Debug.DrawLine(transform.position, Reticle.transform.position, Color.yellow, 5);
            Vector3 wallRightVector = Reticle.transform.position - transform.position;
            slimeLightingWall = Instantiate(slimeLightingPrefab, transform.position, Quaternion.identity) as GameObject;
            slimeLightingWall.transform.right = wallRightVector;

            StartCoroutine(SlimeUltimateCooldown());
        }
    }

}
