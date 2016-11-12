using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	//quit game
    public void Quit()
    {
        //ends the scence
#if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
#else
        //quits the exe
        Application.Quit();
#endif
    }
}
