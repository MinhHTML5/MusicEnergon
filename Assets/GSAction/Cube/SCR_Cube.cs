using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Cube : MonoBehaviour {
	public static SCR_Cube instance;
	
	public GameObject PFB_Explosion;
	
	public const float SIZE_Z = 3.2f;
	public const float SIZE_X = 0.5f;
	
	public const float SPAWN_Z = 65;
	public const float SPAWN_Y = 10;
	public const float GRAVITY = 100;
	public const float MIN_X = 0.5f;
	public const float SPAWN_X = 3.5f;
	
	public float x;
	public float y;
	public float z;
	
	public Material[] MAT_Cube;
	
	
	public float speedY = 0;
	
    private void Start() {
        
    }
	
	public void Spawn() {
		instance = this;
		
		if (Random.Range (-10, 10) > 0) {
			x = -SPAWN_X;
		}
		else {
			x = SPAWN_X;
		}
		y = SPAWN_Y;
		z = SPAWN_Z;
		
		speedY = 0;
		
		transform.position = new Vector3(x, y, z);
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		speedY += GRAVITY * dt * SCR_Action.instance.loseDelay;
		
		if (y > 0) {
			y -= speedY * dt;
			if (y < 0) y = 0;
		}
		
        z -= SCR_Action.SCROLL_SPEED * dt * SCR_Action.instance.loseDelay;
		
		transform.position = new Vector3(x, y, z);
		
		if (z < 0) {
			gameObject.SetActive (false);
		}
		
		if (SCR_Action.instance.lose == false) {
			Transform ball = SCR_Action.instance.ball.transform;
			if (ball.position.z < transform.position.z + SIZE_Z && ball.position.z > transform.position.z - SIZE_Z * 0.6f
			&&  ball.position.x < transform.position.x + SIZE_X && ball.position.x > transform.position.x - SIZE_X * 0.6f) {
				gameObject.SetActive (false);
				
				SCR_Camera.Brighten();
				SCR_Action.instance.Score();
				
				GameObject tempParticle = SCR_Pool.GetFreeObject (PFB_Explosion);
				tempParticle.transform.position = transform.position;
				tempParticle.GetComponent<SCR_CubeExplosion>().SetColor (SCR_Action.instance.majorColor);
				
				if (x < 0) {
					tempParticle.transform.localEulerAngles = new Vector3(0, 20, 0);
				}
				else {
					tempParticle.transform.localEulerAngles = new Vector3(0, -20, 0);
				}
			}
		}
    }
	
	public static void SetColor (Color color) {
		if (instance != null) {
			for (int i=0; i<instance.MAT_Cube.Length; i++) {
				instance.MAT_Cube[i].SetColor("_Color", color); 
			}
		}
	}
}
