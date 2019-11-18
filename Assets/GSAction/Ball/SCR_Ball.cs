using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SCR_Ball : MonoBehaviour {
	public const float CONTROL_AMPLIFIER = 8;
	public const float SPEED_X_MULTIPLIER = 0.4f;
	public const float MAX_X = 0.5f;
	public const float ATTACK_X = 3;
	public const float MIN_STEP = 0.01f;
	
	public float x;
	public float targetX;
	public bool attacking;
	
	private bool controlReady = true;
	private ParticleSystem ps1;
	private ParticleSystem ps2;
	
    private void Start() {
		ps1 = GetComponent<ParticleSystem>();
		ps2 = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
		attacking = false;
		targetX = 0;
    }
	
	public void SetColor (Color color) {
		if (ps1 == null) {
			ps1 = GetComponent<ParticleSystem>();
			ps2 = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
		}
		
		var main = ps1.main;
		main.startColor = color;
		main = ps2.main;
		main.startColor = color;
		
		Gradient grad = new Gradient();
        grad.SetKeys(
			new GradientColorKey[] { 
				new GradientColorKey(color, 0.0f), 
				new GradientColorKey(color, 1.0f) 
			}, 
			new GradientAlphaKey[] { 
				new GradientAlphaKey(0.3f, 0.0f), 
				new GradientAlphaKey(0.0f, 1.0f) 
			} 
		);
		
		var col = ps1.colorOverLifetime;
		col.color = grad;
		col = ps2.colorOverLifetime;
		col.color = grad;
		
		transform.GetChild(1).gameObject.GetComponent<Light>().color = color;
	}

    private void Update() {
        float dt = Time.deltaTime;
		bool mouseDown = false;
		float mouseX = 0;
		if (Input.touches.Length > 0) {
			mouseDown = true;
			mouseX = Input.touches[0].position.x / Screen.width - 0.5f;
		}
		if (Input.GetMouseButton(0)) {
			mouseDown = true;
			mouseX = Input.mousePosition.x / Screen.width - 0.5f;
		}
		
		if (mouseDown == true) {
			if (attacking == false) {
				targetX = mouseX * CONTROL_AMPLIFIER;
				targetX = Mathf.Clamp(targetX, -MAX_X, MAX_X);
			}
		}
		else {
			if (attacking == false) {
				targetX = 0;
			}
		}
		
		if (attacking == false) {
			if (Mathf.Abs(x - targetX) < MIN_STEP) {
				x = targetX;
			}
			else {
				x += (targetX - x) * SPEED_X_MULTIPLIER;
			}
			
			List<GameObject> cubes = SCR_Pool.GetObjectList(SCR_Action.instance.PFB_Cube);
			for (int i=0; i<cubes.Count; i++) {
				if (cubes[i].activeSelf) {
					if (cubes[i].transform.position.z < transform.position.z + SCR_Cube.SIZE_Z * 0.25f
					&&  cubes[i].transform.position.z > transform.position.z - SCR_Cube.SIZE_Z * 0.47f) {
						if (cubes[i].transform.position.x > 0 && targetX > 0) {
							targetX = ATTACK_X;
							attacking = true;
							break;
						}
						else if (cubes[i].transform.position.x < 0 && targetX < 0) {
							targetX = -ATTACK_X;
							attacking = true;
							break;
						}
					}
				}
			}
		}
		else if (attacking == true) {
			x += targetX * SPEED_X_MULTIPLIER;
			if (x < -ATTACK_X) {
				x = -ATTACK_X;
				attacking = false;
			}
			else if (x > ATTACK_X) {
				x = ATTACK_X;
				attacking = false;
			}
		}
		
		
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
