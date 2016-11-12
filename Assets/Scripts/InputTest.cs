using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour
{
	void Start() //Use this for initialization
    {
	
	}

	void Update() //Update is called once per frame
    {
	    if(Input.GetAxis("FireLeft") > 0.0f)
        {
            Debug.Log("Pew Left");
        }
        if (Input.GetAxis("FireRight") > 0.0f)
        {
            Debug.Log("Pew Right");
        }
        if (Input.GetAxis("HorizontalAim") != 0.0f || Input.GetAxis("VerticalAim") != 0.0f) //If the player is aiming
        {
            Debug.Log(Input.GetAxis("HorizontalAim") + Input.GetAxis("VerticalAim"));
            //Instantiate();
        }
        if (Input.GetButtonDown("Trap"))
        {
            Debug.Log("Trap");
        }
        if (Input.GetButtonDown("Ultimate"))
        {
            Debug.Log("Ultimate");
        }
    }
}