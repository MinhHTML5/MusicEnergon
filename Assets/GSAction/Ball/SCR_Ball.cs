using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SCR_Ball : MonoBehaviour {
	public GameObject PFB_Particle;
	public GameObject SPR_GlowLeft;
	public GameObject SPR_GlowMiddle;
	public GameObject SPR_GlowRight;
	
	public const float CONTROL_AMPLIFIER = 8;
	public const float SPEED_X_MULTIPLIER = 0.2f;
	public const float MAX_X = 0.7f;
	public const float ATTACK_X = 3.5f;
	public const float MIN_STEP = 0.001f;
	
	public const float ALPHA_SPEED = 2.0f;
	public const float MIN_ALPHA = 0.1f;
	public const float MAX_ALPHA = 0.35f;
	public const float MAX_ALPHA_MIDDLE = 0.2f;
	
	public float x;
	public float targetX;
	public bool attacking;
	
	private float alphaLeft = MIN_ALPHA;
	private float alphaMiddle = MAX_ALPHA_MIDDLE;
	private float alphaRight = MIN_ALPHA;
	
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
				targetX = Mathf.Clamp(targetX, -MAX_X * 2, MAX_X * 2);
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
			
			if (x <= -MAX_X) alphaLeft += dt * ALPHA_SPEED;
			else alphaLeft -= dt * ALPHA_SPEED;
			
			if (x >= MAX_X)  alphaRight += dt * ALPHA_SPEED;
			else alphaRight -= dt * ALPHA_SPEED;
			
			if (x > -MAX_X && x < MAX_X)  alphaMiddle += dt * ALPHA_SPEED;
			else alphaMiddle -= dt * ALPHA_SPEED;
			
			
			alphaLeft = Mathf.Clamp (alphaLeft, MIN_ALPHA, MAX_ALPHA);
			alphaMiddle = Mathf.Clamp (alphaMiddle, MIN_ALPHA, MAX_ALPHA_MIDDLE);
			alphaRight = Mathf.Clamp (alphaRight, MIN_ALPHA, MAX_ALPHA);
			
			List<GameObject> cubes = SCR_Pool.GetObjectList(SCR_Action.instance.PFB_Cube);
			for (int i=0; i<cubes.Count; i++) {
				if (cubes[i].activeSelf) {
					if (cubes[i].transform.position.z < transform.position.z + SCR_Cube.SIZE_Z * 0.25f
					&&  cubes[i].transform.position.z > transform.position.z - SCR_Cube.SIZE_Z * 0.47f) {
						if (cubes[i].transform.position.x > 0 && targetX >= MAX_X) {
							targetX = ATTACK_X;
							attacking = true;
							break;
						}
						else if (cubes[i].transform.position.x < 0 && targetX <= -MAX_X) {
							targetX = -ATTACK_X;
							attacking = true;
							break;
						}
					}
				}
			}
			
			List<GameObject> bricks = SCR_Pool.GetObjectList(SCR_Action.instance.PFB_Brick);
			for (int i=0; i<bricks.Count; i++) {
				if (bricks[i].activeSelf) {
					if (bricks[i].transform.position.z < transform.position.z + SCR_Cube.SIZE_Z * 0.2f
					&&  bricks[i].transform.position.z > transform.position.z - SCR_Cube.SIZE_Z * 0.4f) {
						if (bricks[i].transform.position.x > 0 && targetX >= MAX_X) {
							targetX = ATTACK_X;
							attacking = true;
							break;
						}
						else if (bricks[i].transform.position.x < 0 && targetX <= -MAX_X) {
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
		
		Color leftLaneColor = SCR_Action.instance.currentColor;
		leftLaneColor.a = alphaLeft;
		SPR_GlowLeft.GetComponent<Renderer>().material.SetColor("_Color", leftLaneColor); 
		
		Color middleLaneColor = SCR_Action.instance.currentColor;
		middleLaneColor.a = alphaMiddle;
		SPR_GlowMiddle.GetComponent<Renderer>().material.SetColor("_Color", middleLaneColor); 
		
		Color rightLaneColor = SCR_Action.instance.currentColor;
		rightLaneColor.a = alphaRight;
		SPR_GlowRight.GetComponent<Renderer>().material.SetColor("_Color", rightLaneColor); 
		
		transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
	
	public void Die() {
		GameObject tempParticle = SCR_Pool.GetFreeObject (PFB_Particle);
		tempParticle.transform.position = transform.position;
		tempParticle.GetComponent<SCR_Explosion>().SetColor (SCR_Action.instance.currentColor);
		if (x < 0) {
			tempParticle.transform.localEulerAngles = new Vector3(0, 30, 0);
		}
		else {
			tempParticle.transform.localEulerAngles = new Vector3(0, -30, 0);
		}
		
		var main = tempParticle.GetComponent<ParticleSystem>().main;
		main.startColor = SCR_Action.instance.currentColor;
		
		Gradient grad = new Gradient();
        grad.SetKeys(
			new GradientColorKey[] { 
				new GradientColorKey(SCR_Action.instance.currentColor, 0.0f), 
				new GradientColorKey(SCR_Action.instance.currentColor, 1.0f) 
			}, 
			new GradientAlphaKey[] { 
				new GradientAlphaKey(1.0f, 0.0f), 
				new GradientAlphaKey(1.0f, 0.8f), 
				new GradientAlphaKey(0.0f, 1.0f) 
			} 
		);
		
		var col = tempParticle.GetComponent<ParticleSystem>().colorOverLifetime;
		col.color = grad;
		
		var emit = ps1.emission;
		emit.rate = 0;
		emit = ps2.emission;
		emit.rate = 0;
	}
}
