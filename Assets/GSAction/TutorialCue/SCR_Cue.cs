using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Cue : MonoBehaviour {
	public const float HOVER_SPEED = 240.0f;
	public const float ROTATE_SPEED = 180.0f;
	public const float OFFSET_X = 1.7f;
	public const float OFFSET_Z = -1.5f;
	
	public static SCR_Cue instance;
		
	public GameObject partner;
	public GameObject pyramid;
	public Material MAT_Cue;
	
	
	private float counter = 0;
	private float angle = 0;
	
	private void Start() {
        
    }
	
	public void Spawn(GameObject cube) {
		instance = this;
		
		partner = cube;
		
		if (partner.transform.position.x > 0) {
			transform.position = new Vector3(partner.transform.position.x - OFFSET_X, 0, partner.transform.position.z + OFFSET_Z);
		}
		else {
			transform.position = new Vector3(partner.transform.position.x + OFFSET_X, 0, partner.transform.position.z + OFFSET_Z);
		}
	}

    private void Update() {
		float dt = Time.deltaTime;
		
		counter += HOVER_SPEED * dt;
		if (counter > 360) counter -= 360;
		pyramid.transform.localPosition = new Vector3(0, SCR_Helper.Sin(counter) * 0.005f + 0.02f, 0);
		
		angle += HOVER_SPEED * dt;
		if (angle > 360) angle -= 360;
		//pyramid.transform.localEulerAngles = new Vector3(0, angle, 0);
		
		
		if (partner.transform.position.x > 0) {
			transform.position = new Vector3(partner.transform.position.x - OFFSET_X, partner.transform.position.y, partner.transform.position.z + OFFSET_Z);
		}
		else {
			transform.position = new Vector3(partner.transform.position.x + OFFSET_X, partner.transform.position.y, partner.transform.position.z + OFFSET_Z);
		}
		
		if (transform.position.z < 0) {
			gameObject.SetActive (false);
		}
    }
	
	public static void SetColor (Color color) {
		if (instance != null) {
			instance.MAT_Cue.SetColor("_Color", color); 
		}
	}
}
