using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CubeExplosion : MonoBehaviour {
	public ParticleSystem[] particleSystem;
	
    private void Start() {
        
    }

    private void Update() {
		float dt = Time.deltaTime;
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		
        z -= SCR_Action.SCROLL_SPEED * dt;
		
		transform.position = new Vector3(x, y, z);
    }
	
	public void SetColor (Color color) {
		for (int i=0; i<particleSystem.Length; i++) {
			ParticleSystem ps = particleSystem[i];
			var main = ps.main;
			main.startColor = color;
		
			Gradient grad = new Gradient();
			grad.SetKeys(
				new GradientColorKey[] { 
					new GradientColorKey(color, 0.0f), 
					new GradientColorKey(color, 1.0f) 
				}, 
				new GradientAlphaKey[] { 
					new GradientAlphaKey(0.0f, 0.0f), 
					new GradientAlphaKey(1.0f, 0.1f), 
					new GradientAlphaKey(0.0f, 1.0f) 
				} 
			);
			
			var col = ps.colorOverLifetime;
			col.color = grad;
		}
	}
}
