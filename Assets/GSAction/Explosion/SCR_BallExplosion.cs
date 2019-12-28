using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_BallExplosion : MonoBehaviour {
	private void Start() {
    
    }

    private void Update() {
		float dt = Time.deltaTime;
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		
        z -= SCR_Action.SCROLL_SPEED * dt * 0.5f;
		
		transform.position = new Vector3(x, y, z);
    }
	
	public void SetColor (Color color) {
		
	}
}
