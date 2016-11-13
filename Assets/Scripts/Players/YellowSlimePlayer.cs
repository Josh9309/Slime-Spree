using UnityEngine;
using System.Collections;
using System;

public class YellowSlimePlayer : PlayerSlime {
    #region Attributes
    [SerializeField] private int slimeWallCost;
    [SerializeField] private GameObject slimeLightingPrefab;
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
            Debug.DrawLine(transform.position, reticle.transform.position, Color.yellow, 5);
            Vector3 wallRightVector = reticle.transform.position - transform.position;
            Instantiate(slimeLightingPrefab, transform.position, Quaternion.identity);
        }
    }

}
