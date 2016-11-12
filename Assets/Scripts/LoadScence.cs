using UnityEngine;
using System.Collections;


public class LoadScence : MonoBehaviour {

    //Loads the scene depending on the name  of the scence
	public void LoadByIndex(string name)
    {
        Application.LoadLevel(name);
    }
}
