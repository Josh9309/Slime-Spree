using UnityEngine;
using System.Collections;
using System;

public class YellowSlimePlayer : PlayerSlime {
    #region Attributes
    [SerializeField] private int slimeWallCost;
    [SerializeField] private GameObject slimeLightingPrefab;
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

        SlimeUltimate();
        input.ResetBtns();
    }

    protected override void SlimeAttack2()
    {
        throw new NotImplementedException();
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
