using UnityEngine;
using System.Collections;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer healthSR; //Health sprite renderer
    float colorTimer; //Timer for the color

	// Use this for initialization
	void Start()
    {
        healthSR = GetComponent<SpriteRenderer>(); //Get the health's sprite renderer	
	}
	
	// Update is called once per frame
	void Update()
    {
        colorTimer += Time.deltaTime; //Increment the color timer

        if (colorTimer >= 0 && colorTimer < 1) //For the first color
        {
            healthSR.color = new Color(0.0f, 1.0f, 0.0f); //Change the color of the health to green
        }
        else if (colorTimer >= 1 && colorTimer < 2) //For the second color
        {
            healthSR.color = new Color(1.0f, 0.0f, 0.0f); //Change the color of the health to red
        }
        else if (colorTimer >= 2 && colorTimer < 3) //For the third color
        {
            healthSR.color = new Color(0.0f, 0.0f, 1.0f); //Change the color of the health to blue
        }
        else if (colorTimer >= 3 && colorTimer < 4) //For the fourth color
        {
            healthSR.color = new Color(1.0f, 1.0f, 0.0f); //Change the color of the health to yellow
        }
        else //If all colors have been cycled through
        {
            colorTimer = 0; //Reset the color timer
        }
    }
}