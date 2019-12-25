using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Plane : MonoBehaviour {
	public static SCR_Plane instance;
	
	public const float 	PLANE_NUMBER = 1;
	public const float 	PLANE_Y = -0.5f;
	public const float 	PLANE_LENGTH = 100;
	
	public Material 	MAT_Plane;
	
	private int index;
	
	public void Spawn (int i) {
		instance = this;
		index = i;
		transform.position = new Vector3(0, PLANE_Y, PLANE_LENGTH * i);
	}
	
    private void Start() {
        instance = this;
    }

    private void Update() {
		float dt = Time.deltaTime;
		
		/*
        transform.position = new Vector3(0, PLANE_Y, transform.position.z - SCR_Action.SCROLL_SPEED * dt * SCR_Action.instance.loseDelay);
		if (transform.position.z < -PLANE_LENGTH) {
			transform.position = new Vector3(0, PLANE_Y, transform.position.z + PLANE_LENGTH * PLANE_NUMBER);
		}
		*/
    }
	
	public static void SetColor (Color color) {
		if (instance != null) {
			instance.MAT_Plane.SetColor("_Color", color); 
		}
	}
}
