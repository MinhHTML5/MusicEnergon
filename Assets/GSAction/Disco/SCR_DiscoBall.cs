using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DiscoBall : MonoBehaviour {
	public const float ROTATE_SPEED = 8;
	
	public Material MAT_Disco;
	
	private float angle = 0;
	
    private void Start() {
        
    }

    private void Update() {
        float dt = Time.deltaTime;
		
		angle += ROTATE_SPEED * dt;
		if (angle > 360) angle -= 360;
		
		transform.localEulerAngles = new Vector3(0, angle, 0);
    }
	
	public void SetColor (Color color) {
		MAT_Disco.SetColor("_Color", color);
	}
}
