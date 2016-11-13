using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    //sound Effects
    public AudioSource sfxSource;
    //backgound music
    public AudioSource musicSource;

    public static SoundManager instance = null;

    public float lowPitch = .95f;
    public float highPitch = 1.05f;
	// Use this for initialization
	void Awake ()
    {
       
	if (instance == null)
        {
            instance = this;
        }
    else if(instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}

    //Play random effects at random pitch to add variety
    public void PlayingSingle(AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitch, highPitch);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clips[randomIndex];
        sfxSource.Play();

    }
	
	// Update is called once per frame

}
