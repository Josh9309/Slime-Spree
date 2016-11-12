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
        colorTimer += (Time.deltaTime / 6); //Increment the color timer
        healthSR.color = new Color(colorTimer, 0.0f, 0.0f); //Change the color of the health
	}
}