using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SCR_Ball : MonoBehaviour {
	public const float SPEED_X_MULTIPLIER = 0.3f;
	public const float MAX_X = 2;
	public const float MIN_STEP = 0.1f;
	
	public float x;
	public float targetX;

	private ParticleSystem ps1;
	private ParticleSystem ps2;
	
    private void Start() {
		ps1 = GetComponent<ParticleSystem>();
		ps2 = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }
	
	public void SetColor (Color color) {
		var main = ps1.main;
		main.startColor = color;
		main = ps2.main;
		main.startColor = color;
		
		Gradient grad = new Gradient();
        grad.SetKeys(
			new GradientColorKey[] { 
				new GradientColorKey(color, 0.0f), 
				new GradientColorKey(color, 1.0f) 
			}, 
			new GradientAlphaKey[] { 
				new GradientAlphaKey(0.3f, 0.0f), 
				new GradientAlphaKey(0.0f, 1.0f) 
			} 
		);
		
		var col = ps1.colorOverLifetime;
		col.color = grad;
		col = ps2.colorOverLifetime;
		col.color = grad;
		
		transform.GetChild(1).gameObject.GetComponent<Light>().color = color;
	}

    private void Update() {
        float dt = Time.deltaTime;
		
		if (Mathf.Abs(x - targetX) < MIN_STEP) {
			x = targetX;
		}
		else {
			x += (targetX - x) * SPEED_X_MULTIPLIER;
		}
		
		x = Mathf.Clamp(x, -MAX_X, MAX_X);
		
		
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
	
	public void SetTarget(float target) {
		targetX = Mathf.Clamp(target, -MAX_X, MAX_X);
	}
}
