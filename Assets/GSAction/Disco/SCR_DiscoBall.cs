using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DiscoBall : MonoBehaviour {
	public const float ROTATION_SPEED = 5;
	
	public GameObject PFB_OuterLayer;
	public GameObject LGT_DiscoLight;
	
	public Material MAT_DiscoGlow;
	public Material MAT_DiscoInner;
	
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
    }
	
	public void SetColor (Color color) {
		//MAT_DiscoInner.SetColor("_Color", color); 
		MAT_DiscoGlow.SetColor("_Color", color); 
		LGT_DiscoLight.GetComponent<Light>().color = color;
	}
}
