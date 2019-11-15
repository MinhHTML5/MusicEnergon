using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Cube : MonoBehaviour {
	public GameObject PFB_Particle;
	
	public const float SIZE_Z = 0.5f;
	public const float SIZE_X = 1.0f;
	
	public const float SPAWN_Z = 75;
	public const float SPAWN_Y = 10;
	public const float GRAVITY = 100;
	public const float MIN_X = 0.5f;
	public const float MAX_X = 2;
	
	public float x;
	public float y;
	public float z;
	
	public float speedY = 0;
	
    private void Start() {
        
    }
	
	public void Spawn() {
		x = Random.Range (MIN_X, MAX_X);
		if (Random.Range (-10, 10) > 0) {
			x = -x;
		}
		y = SPAWN_Y;
		z = SPAWN_Z;
		
		speedY = 0;
		
		transform.position = new Vector3(x, y, z);
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		speedY += GRAVITY * dt;
		
		if (y > 0) {
			y -= speedY * dt;
			if (y < 0) y = 0;
		}
		
        z -= SCR_Action.SPEED_Z * dt;
		
		transform.position = new Vector3(x, y, z);
		
		if (z < 0) {
			gameObject.SetActive (false);
		}
		
		Transform ball = SCR_Action.instance.ball.transform;
		if (ball.position.z < transform.position.z + SIZE_Z && ball.position.z > transform.position.z - SIZE_Z
		&&  ball.position.x < transform.position.x + SIZE_X && ball.position.x > transform.position.x - SIZE_X) {
			gameObject.SetActive (false);
			
			SCR_Camera.Brighten();
			
			GameObject tempParticle = SCR_Pool.GetFreeObject (PFB_Particle);
			tempParticle.transform.position = transform.position;
			tempParticle.GetComponent<SCR_Explosion>().SetColor (SCR_Action.instance.currentColor);
		}
    }
}
