using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Camera : MonoBehaviour {
	public static SCR_Camera instance;
	
	public static bool bloomEnabled = true;
	
	public GameObject SPR_Background;
	public GameObject BTN_BloomOn;
	public GameObject BTN_BloomOff;
	
	private float darkColorMultiplier = 0.1f;
	private float brightColorMultiplier = 0.2f;
	private float currentColorMultiplier = 0;
	
    private void Start() {
        instance = this;
		currentColorMultiplier = darkColorMultiplier;
		GetComponent<Kino.Bloom>().enabled = bloomEnabled;
		
		BTN_BloomOn.SetActive (bloomEnabled);
		BTN_BloomOff.SetActive (!bloomEnabled);
    }
	
	public static void Brighten() {
		instance.currentColorMultiplier = instance.brightColorMultiplier;
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		if (currentColorMultiplier > darkColorMultiplier) {
			currentColorMultiplier -= (brightColorMultiplier - darkColorMultiplier) * dt * 2;
			if (currentColorMultiplier < darkColorMultiplier) {
				currentColorMultiplier = darkColorMultiplier;
			}
		}
		
		Color color = new Color(
			SCR_Action.instance.majorColor.r * currentColorMultiplier,
			SCR_Action.instance.majorColor.g * currentColorMultiplier,
			SCR_Action.instance.majorColor.b * currentColorMultiplier
		);
		GetComponent<Camera>().backgroundColor = color;
		RenderSettings.fogColor = color;
		
		color *= new Vector4(5, 5, 5, 1);
		SPR_Background.GetComponent<SCR_Background>().SetColor (color);
    }
	
	public void ToggleBloom() {
		bloomEnabled = !bloomEnabled;
		GetComponent<Kino.Bloom>().enabled = bloomEnabled;
		BTN_BloomOn.SetActive (bloomEnabled);
		BTN_BloomOff.SetActive (!bloomEnabled);
	}
}
