using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Cue : MonoBehaviour {
	public const float OFFSET_X = 1.7f;
	public const float OFFSET_Z = -1.5f;
	
	public static SCR_Cue instance;
		
	public GameObject partner;
	
	private float z = 0;
	
	private void Start() {
        
    }
	
	public void Spawn(GameObject cube) {
		instance = this;
		
		partner = cube;
		
		z = partner.transform.position.z + OFFSET_Z;
		
		if (partner.transform.position.x > 0) {
			transform.position = new Vector3(partner.transform.position.x - OFFSET_X, 0, z);
			transform.localEulerAngles = new Vector3(0, 0, 180);
		}
		else {
			transform.position = new Vector3(partner.transform.position.x + OFFSET_X, 0, z);
		}
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		z -= SCR_Action.SCROLL_SPEED * dt * SCR_Action.instance.loseDelay;
		transform.position = new Vector3(transform.position.x, 0, z);
		
		if (z < 0) {
			gameObject.SetActive (false);
		}
    }
	
	public static void SetColor (Color color) {
		if (instance != null) {
			//instance.MAT_Cue.SetColor("_Color", color); 
		}
	}
}
