using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LightRay : MonoBehaviour {
	public const float SPAWN_X = 11;
	public const float SPAWN_Y = -2;
	public const float SPAWN_Z = 80;
	
	public const float SPAWN_X_VAR = 4;
	public const float SPAWN_Y_VAR = 0;
	public const float TERMINATE_Z = 0;
	
	public const float SPEED_MULTIPLIER = 4.5f;
	public const float SPEED_VARIABLE = 1.5f;
	
	
	
	public float x;
	public float y;
	public float z;
	public float speed;
	
    private void Start() {
        
    }
	
	public void Spawn (int side) {
		x = side * SPAWN_X;
		x += Random.Range (-1.0f, 1.0f) * SPAWN_X_VAR;
		
		y = SPAWN_Y;
		y += Random.Range (-1.0f, 1.0f) * SPAWN_Y_VAR;
		
		z = SPAWN_Z;
		
		speed = SPEED_MULTIPLIER + Random.Range (-1.0f, 1.0f) * SPEED_VARIABLE;
		
		transform.position = new Vector3(x, y, z);
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, speed);
		
		gameObject.SetActive (true);
	}

    private void Update() {
        float dt = Time.deltaTime;
		
		z -= SCR_Action.SCROLL_SPEED * dt * SCR_Action.instance.loseDelay * speed;
		
		if (z <= TERMINATE_Z) {
			gameObject.SetActive (false);
		}
		
		transform.position = new Vector3(x, y, z);
    }
}
