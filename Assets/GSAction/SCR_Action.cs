using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_Action : MonoBehaviour {
	public const float CONTROL_AMPLIFIER = 10;
	public const float PLANE_Y = -0.5f;
	public const float PLANE_LENGTH = 100;
	public const float SPEED_Z = 15;
	public const float MUSIC_OFFSET = 0.0f;
	public const float COLOR_CHANGE_SPEED = 0.6f;
	
	public static SCR_Action instance;
	
	public GameObject PFB_Plane;
	public GameObject PFB_Cube;
	public GameObject PFB_Explosion;
	
	public Material MAT_Plane;
	public Material MAT_Cube;
	public GameObject CTN_Replay;
	public AudioSource MUS_Unity;
	
	public GameObject ball;
	public GameObject[] planes;
	
	public Color[] globalColor;
	public Color   targetColor;
	public Color   oldColor;
	public Color   currentColor;
	
	
	private float spawnCount = 0;
	private int   spawnIndex = 0;
	private float musicDelay = 0;
	private float endDelay = 6.0f;
	private float changeColorInterval = 0;
	
	private void Start() {
		Application.targetFrameRate = 60;
#if UNITY_STANDALONE_WIN
		Screen.SetResolution(540, 960, false);
#endif

		SCR_Pool.Flush();
		
		instance = this;
		
		planes = new GameObject[5];
		for (int i=0; i<planes.Length; i++) {
			planes[i] = SCR_Pool.GetFreeObject (PFB_Plane);
			planes[i].transform.position = new Vector3(0, PLANE_Y, PLANE_LENGTH * i);
		}
		
		spawnCount = 0;
		spawnIndex = 0;
		musicDelay = SCR_Cube.SPAWN_Z / SPEED_Z - SCR_MusicData.data[0] + MUSIC_OFFSET;
		
		changeColorInterval = 0.5f;
		
		PickRandomColor();
		currentColor = targetColor;
		ApplyColor();
		
		CTN_Replay.SetActive (false);
    }

    private void Update() {
		float dt = Time.deltaTime;
		
		// =========================================================================================================================
		// Move bottom plane
		for (int i=0; i<planes.Length; i++) {
			planes[i].transform.position = new Vector3(0, PLANE_Y, planes[i].transform.position.z - SPEED_Z * dt);
			if (planes[i].transform.position.z < -PLANE_LENGTH) {
				planes[i].transform.position = new Vector3(0, PLANE_Y, planes[i].transform.position.z + PLANE_LENGTH * planes.Length);
			}
		}
		
		// =========================================================================================================================
		// Control ball
        if (Input.GetMouseButton(0)) {
			float percent = Input.mousePosition.x / Screen.width - 0.5f;
			ball.GetComponent<SCR_Ball>().SetTarget (percent * CONTROL_AMPLIFIER);
		}
		
		// =========================================================================================================================
		// Spawn cubes
		if (musicDelay > 0) {
			musicDelay -= dt;
			if (musicDelay <= 0) {
				MUS_Unity.Play();
			}
		}
		if (spawnIndex < SCR_MusicData.data.Length) {
			spawnCount += dt;
			if (spawnCount >= SCR_MusicData.data[spawnIndex]) {
				spawnIndex ++;
				GameObject tempCube = SCR_Pool.GetFreeObject (PFB_Cube);
				tempCube.GetComponent<SCR_Cube>().Spawn();
			}
		}
		else {
			endDelay -= dt;
			if (endDelay <= 0) {
				CTN_Replay.SetActive (true);
			}
		}
		
		// =========================================================================================================================
		// Change global color
		if (currentColor != targetColor) {
			float changeSpeed = dt * COLOR_CHANGE_SPEED;
			float amountR = (targetColor.r - oldColor.r) * changeSpeed;
			float amountG = (targetColor.g - oldColor.g) * changeSpeed;
			float amountB = (targetColor.b - oldColor.b) * changeSpeed;
			
			if ((currentColor.r < targetColor.r && currentColor.r + amountR > targetColor.r)
			||  (currentColor.r > targetColor.r && currentColor.r + amountR < targetColor.r)
			||  (currentColor.g < targetColor.g && currentColor.g + amountG > targetColor.g)
			||  (currentColor.g > targetColor.g && currentColor.g + amountG < targetColor.g)
			||  (currentColor.b < targetColor.b && currentColor.b + amountB > targetColor.b)
			||  (currentColor.b > targetColor.b && currentColor.b + amountB < targetColor.b)) {
				currentColor = targetColor;
			}
			else {
				currentColor.r += amountR;
				currentColor.g += amountG;
				currentColor.b += amountB;
			}
			
			ApplyColor();
		}
		
		if (musicDelay <= 0) {
			changeColorInterval += dt;
			if (changeColorInterval > 18.28f) {
				changeColorInterval -= 18.28f;
				PickRandomColor();
			}
		}
		// =========================================================================================================================
    }
	
	private void ApplyColor() {
		MAT_Cube.SetColor("_EmissionColor", currentColor); 
		
		ball.GetComponent<SCR_Ball>().SetColor (currentColor);
		
		List<GameObject> explosions = SCR_Pool.GetObjectList(PFB_Explosion);
		for (int i=0; i<explosions.Count; i++) {
			explosions[i].GetComponent<SCR_Explosion>().SetColor (currentColor);
		}
	}
	
	public void ChangePlaneColor(Color color) {
		MAT_Plane.SetColor("_EmissionColor", color); 
	}
	
	private void PickRandomColor() {
		int choose = 0;
		do {
			choose = Random.Range (0, globalColor.Length);
		}
		while (targetColor == globalColor[choose]);
		
		oldColor = targetColor;
		targetColor = globalColor[choose];
	}
	
	public void Replay() {
		SceneManager.LoadScene("GSAction/SCN_Action");
	}
}
