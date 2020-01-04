using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LightSpawner : MonoBehaviour {
	public const float SPAWN_LATENCY = 0.4f;
	public const float SPAWN_VARIABLE = 0.2f;
	
	public static SCR_LightSpawner instance;
	
    public GameObject PFB_LightRay;
	public Material MAT_LightRay;
	
	private float leftSpawnCounter;
	private float rightSpawnCounter;
	
    private void Start() {
		instance = this;
        leftSpawnCounter = SPAWN_LATENCY + Random.Range(-1.0f, 1.0f) * SPAWN_VARIABLE;
		rightSpawnCounter = SPAWN_LATENCY + Random.Range(-1.0f, 1.0f) * SPAWN_VARIABLE;
    }

    private void Update() {
        float dt = Time.deltaTime;
		
		leftSpawnCounter -= dt;
		if (leftSpawnCounter <= 0) {
			leftSpawnCounter = SPAWN_LATENCY + Random.Range(-1, 1) * SPAWN_VARIABLE;
			
			GameObject light = SCR_Pool.GetFreeObject (PFB_LightRay);
			light.GetComponent<SCR_LightRay>().Spawn (-1);
		}
		
		rightSpawnCounter -= dt;
		if (rightSpawnCounter <= 0) {
			rightSpawnCounter = SPAWN_LATENCY + Random.Range(-1, 1) * SPAWN_VARIABLE;
			
			GameObject light = SCR_Pool.GetFreeObject (PFB_LightRay);
			light.GetComponent<SCR_LightRay>().Spawn (1);
		}
    }
	
	public static void SetColor (Color color) {
		if (instance != null) {
			//instance.MAT_LightRay.SetColor("_Color", color); 
		}
	}
}
