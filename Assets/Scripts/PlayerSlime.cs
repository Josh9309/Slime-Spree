﻿using UnityEngine;
using System.Collections;

public abstract class PlayerSlime : MonoBehaviour {
    public enum SlimeType { RED, BLUE, YELLOW, GREEN};

    #region InputSettings
    //this will setup the public inputSetting class
    [System.Serializable]
    public class InputSettings
    {
        public float delay = 0.3f; //delay for movement inputs
        public float horizontalInput = 0, verticalInput = 0,  attack1Input = 0, attack2Input = 0; //sets up variables to hold inputs
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
    protected int health = 100;
    [SerializeField] protected SlimeType slimerType;
    #endregion

    #region Properties

    #endregion

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected virtual void SlimeShotAttack()
    {

    }

    protected abstract void SlimeAttack2();

    protected abstract void SlimeTrap();
}