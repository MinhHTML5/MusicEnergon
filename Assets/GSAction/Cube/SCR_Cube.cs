using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Cube : MonoBehaviour {
	public static SCR_Cube instance;
	
	public GameObject PFB_Explosion;
	
	public const float SIZE_X = 0.75f;
	public const float SIZE_Z = 0.75f;
	
	public const float SPAWN_X = 1.5f;
	public const float SPAWN_Z = 80;
	public const float SPAWN_Y = 10;
	public const float STAY_Y = 0;
	public const float GRAVITY = 100;
	public const float MIN_X = 0.5f;
	
	
	public float x;
	public float y;
	public float z;
	
	public Material[] MAT_Cube;
	
	
	public int lane = 0;
	public float speedY = 0;
	
    private void Start() {
        
    }
	
	public void Spawn() {
		instance = this;
		
		int random = Random.Range (0, 99);
		if (random % 3 == 0) {
			lane = -1;
		}
		else if (random % 3 == 1) {
			lane = 0;
		}
		else if (random % 3 == 2) {
			lane = 1;
		}
		
		x = lane * SPAWN_X;
		
		y = SPAWN_Y;
		z = SPAWN_Z;
		
		speedY = 0;
		
		transform.position = new Vector3(x, y, z);
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		speedY += GRAVITY * dt * SCR_Action.instance.loseDelay;
		
		if (y > STAY_Y) {
			y -= speedY * dt;
			if (y < STAY_Y) y = STAY_Y;
		}
		
        z -= SCR_Action.SCROLL_SPEED * dt * SCR_Action.instance.loseDelay;
		
		transform.position = new Vector3(x, y, z);
		
		if (z < 0) {
			gameObject.SetActive (false);
		}
		
		if (SCR_Action.instance.lose == false) {
			Transform ball = SCR_Action.instance.ball.transform;
			if (ball.position.z < transform.position.z + SIZE_Z * 1.3f && ball.position.z > transform.position.z - SIZE_Z * 1.3f
			&&  ball.position.x < transform.position.x + SIZE_X * 1.3f && ball.position.x > transform.position.x - SIZE_X * 1.3f) {
				gameObject.SetActive (false);
				
				SCR_Camera.Brighten();
				SCR_Action.instance.Score();
				
				GameObject tempParticle = SCR_Pool.GetFreeObject (PFB_Explosion);
				tempParticle.transform.position = transform.position;
				tempParticle.GetComponent<SCR_CubeExplosion>().SetColor (SCR_Action.instance.majorColor);
				
				/*
				if (x < 0) {
					tempParticle.transform.localEulerAngles = new Vector3(0, 20, 0);
				}
				else {
					tempParticle.transform.localEulerAngles = new Vector3(0, -20, 0);
				}
				*/
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
