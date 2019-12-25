using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Lane : MonoBehaviour {
	public const float COLOR_CHANGE_SPEED = 5;
	public const float ALPHA_MULTIPLIER = 1;
	
	private Color oldColor;
	private Color targetColor;
	private float colorChangeCount = 1;
	
    private void Start() {
        
    }

    private void Update() {
        float dt = Time.deltaTime;
		if (colorChangeCount < 1) {
			colorChangeCount += dt * COLOR_CHANGE_SPEED;
			if (colorChangeCount >= 1) {
				colorChangeCount = 1;
				
				GetComponent<Renderer>().material.SetColor("_Color", targetColor);
				oldColor = targetColor;
			}
			else {
				GetComponent<Renderer>().material.SetColor("_Color", SCR_Helper.ColorTween (oldColor, targetColor, colorChangeCount));
			}
		}
    }
	
	public void SetColor (Color color, bool fullAlpha, bool rightNow) {
		if (!fullAlpha) {
			color.a = color.a * ALPHA_MULTIPLIER;
		}
		if (rightNow) {
			oldColor = color;
			targetColor = color;
			colorChangeCount = 1;
			GetComponent<Renderer>().material.SetColor("_Color", targetColor);
		}
		else {
			oldColor = SCR_Helper.ColorTween (oldColor, targetColor, colorChangeCount);
			targetColor = color;
			colorChangeCount = 0;
		}
	}
}
