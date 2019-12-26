using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Lane : MonoBehaviour {
	public const float COLOR_CHANGE_SPEED = 5;
	public const float ALPHA_MULTIPLIER = 1;
	
	private Color majorColor;
	private Color minorColor;
	
	private float colorChangeCount = 1;
	private bool highlight = false;
	
    private void Start() {
        
    }

    private void Update() {
        float dt = Time.deltaTime;
		
		if (highlight) {
			colorChangeCount += dt * COLOR_CHANGE_SPEED;
			if (colorChangeCount > 1) colorChangeCount = 1;
		}
		else {
			colorChangeCount -= dt * COLOR_CHANGE_SPEED;
			if (colorChangeCount < 0) colorChangeCount = 0;
		}
		
		GetComponent<Renderer>().material.SetColor("_Color", SCR_Helper.ColorTween (majorColor, minorColor, colorChangeCount));
    }
	
	public void SetHighlight (bool val) {
		highlight = val;
	}
	
	public void SetColor (Color major, Color minor) {
		majorColor = major;
		minorColor = minor;
	}
}
