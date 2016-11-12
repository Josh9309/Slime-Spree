using UnityEngine;
using System.Collections;

public class CameraSize : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
        GetComponent<Camera>().orthographicSize = 20;
	}
	
	
}
