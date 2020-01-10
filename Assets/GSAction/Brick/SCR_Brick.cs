using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Brick : MonoBehaviour {
	public const float SIZE_Z = 0.75f;
	public const float SIZE_X = 0.75f;
	
	public const float SPAWN_X = 1.5f;
	public const float SPAWN_Z = 60;
	public const float SPAWN_Y = 10;
	public const float GRAVITY = 100;
	
	
	public float x;
	public float y;
	public float z;
	
	
	public float speedY = 0;
	
    private void Start() {
        
    }
	
	public void Spawn(float spawnX) {
		x = spawnX;
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
			if (ball.position.z < transform.position.z + SIZE_Z * 1.0f && ball.position.z > transform.position.z - SIZE_Z * 1.0f
			&&  ball.position.x < transform.position.x + SIZE_X * 1.0f && ball.position.x > transform.position.x - SIZE_X * 1.0f) {
				ball.gameObject.GetComponent<SCR_Ball>().Die();
				SCR_Action.instance.lose = true;
			}
		}
    }
}
