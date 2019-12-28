using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SCR_Ball : MonoBehaviour {
	public GameObject PFB_Particle;
	
	public const float CONTROL_AMPLIFIER = 8;
	public const float SPEED_X_MULTIPLIER = 20.0f;
	public const float LANE_X = 0.75f;
	public const float MAX_X = 2;
	public const float MIN_STEP = 0.001f;
	
	public const float ALPHA_SPEED = 2.0f;
	public const float MIN_ALPHA = 0.1f;
	public const float MAX_ALPHA = 0.35f;
	public const float MAX_ALPHA_MIDDLE = 0.2f;
	
	public Material MAT_Ball;
	public GameObject SPR_White;
	public GameObject PAR_Trail;
	
	public float x;
	public float targetX;
	
	public int lane = 0;
	
	private bool live = true;
	private ParticleSystem ps1;
	private ParticleSystem ps2;
	
    private void Start() {
		ps1 = GetComponent<ParticleSystem>();
		ps2 = PAR_Trail.GetComponent<ParticleSystem>();
		targetX = 0;
    }
	
	public void SetColor (Color color) {
	
	}

    private void Update() {
        float dt = Time.deltaTime;
		bool mouseDown = false;
		float mouseX = 0;
		
		if (live == true) {
			if (Input.touches.Length > 0) {
				mouseDown = true;
				mouseX = Input.touches[0].position.x / Screen.width - 0.5f;
			}
			if (Input.GetMouseButton(0)) {
				mouseDown = true;
				mouseX = Input.mousePosition.x / Screen.width - 0.5f;
			}
			
			if (mouseDown == true) {
				targetX = mouseX * CONTROL_AMPLIFIER;
				targetX = Mathf.Clamp(targetX, -MAX_X, MAX_X);
			}
			
			if (Mathf.Abs(x - targetX) < MIN_STEP) {
				x = targetX;
			}
			else {
				x += (targetX - x) * SPEED_X_MULTIPLIER * dt;
			}
			
			if (x <= -LANE_X) {
				lane = -1;
			}
			else if (x >= LANE_X) {
				lane = 1;
			}
			else if (x > -LANE_X && x < LANE_X)  {
				lane = 0;
			}
			SCR_Action.instance.HighlightLane (lane);
			
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}
    }
	
	public void Die() {
		if (live == true) {
			GameObject tempParticle = SCR_Pool.GetFreeObject (PFB_Particle);
			tempParticle.transform.position = transform.position;
			
			var emit = ps1.emission;
			emit.rate = 0;
			emit = ps2.emission;
			emit.rate = 0;
			
			SPR_White.SetActive (false);
			live = false;
		}
	}
}
