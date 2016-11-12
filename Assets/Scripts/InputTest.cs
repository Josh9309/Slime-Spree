using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetAxis("FireLeft") > 0.0f)
        {
            Debug.Log("Pew Left");
        }
        if (Input.GetAxis("FireRight") > 0.0f)
        {
            Debug.Log("Pew Right");
        }
        if (Input.GetAxis("Aim") != 0.0f)
        {
            Debug.Log(Input.GetAxis("Aim"));
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
