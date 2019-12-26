using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DiscoBall : MonoBehaviour {
	public const float ROTATION_SPEED = 5;
	public const float NORMAL_SCALE = 30;
	public const float BREATH_SPEED = 90;
	public const float BREATH_AMOUNT = 0.5f;
	public const float EXPAND_SCALE = 32f;
	
	public const float SCALE_CHANGE_MULTIPLIER = 50;
	public const float SCALE_CHANGE_MIN = 1;
	
	public GameObject PFB_OuterLayer;
	public GameObject LGT_DiscoLight;
	
	public Material MAT_DiscoGlow;
	public Material MAT_DiscoInner;
	
	
	
	private float breathCount = 0;
	private float targetScale = NORMAL_SCALE;
	private float currentScale = NORMAL_SCALE;
	private bool  expanding = false;
	
	private float rotation = 0;
	
    // Start is called before the first frame update
    private void Start() {
        PFB_OuterLayer.GetComponent<MeshRenderer>().material.renderQueue = 3102;
    }

    // Update is called once per frame
    private void Update() {
        float dt = Time.deltaTime;
		
		rotation += ROTATION_SPEED * dt;
		if (rotation > 360) rotation -= 360;
		PFB_OuterLayer.transform.localEulerAngles = new Vector3(0, -rotation, 0);
		
		breathCount += dt * BREATH_SPEED;
		if (breathCount > 360) breathCount -= 360;
		
		if (!expanding) {
			targetScale = NORMAL_SCALE + SCR_Helper.Sin (breathCount) * BREATH_AMOUNT;
		}
		
		if (Mathf.Abs (currentScale - targetScale) * SCALE_CHANGE_MULTIPLIER < SCALE_CHANGE_MIN) {
			currentScale = targetScale;
			expanding = false;
		}
		else {
			currentScale += (targetScale - currentScale) * dt * SCALE_CHANGE_MULTIPLIER;
		}
		
		transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
    }
	
	public void SetColor (Color color) {
		MAT_DiscoInner.SetColor("_Color", color); 
		MAT_DiscoGlow.SetColor("_Color", color); 
		LGT_DiscoLight.GetComponent<Light>().color = color;
	}
	
	public void Expand() {
		//expanding = true;
		//targetScale = EXPAND_SCALE;
	}
}
