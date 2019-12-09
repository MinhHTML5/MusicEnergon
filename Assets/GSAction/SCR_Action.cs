using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCR_Action : MonoBehaviour {
	public const float PLANE_Y = -0.5f;
	public const float PLANE_LENGTH = 100;
	public const float SPEED_Z = 15;
#if UNITY_WEBGL
	public const float MUSIC_OFFSET = 0.1f;
#else
	public const float MUSIC_OFFSET = 0.0f;
#endif
	public const float COLOR_CHANGE_SPEED = 0.6f;
	public const float BRICK_SPAWN_LATENCY = 1.0f;
	public const float BRICK_BLOCK_LATENCY = 0.06f;
	public const float OBSTACLE_CHANCE = 15;
	
	public static SCR_Action instance;
	
	public GameObject PFB_Plane;
	public GameObject PFB_Cube;
	public GameObject PFB_Brick;
	public GameObject PFB_Obstacle;
	public GameObject PFB_Explosion;
	
	
	public Material MAT_Plane;
	public Material[] MAT_Cube;
	public GameObject CTN_Replay;
	public GameObject IMG_Tutorial;
	public AudioSource SND_Music;
	public Text TXT_Score;
	
	public GameObject ball;
	public GameObject[] planes;
	
	public Color[] globalColor;
	public Color   targetColor;
	public Color   oldColor;
	public Color   currentColor;
	
	
	public  int   score = 0;
	public  bool  lose = false;
	public  float loseDelay = 1;
	
	private float brickCount = 0;
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
		
		planes = new GameObject[4];
		for (int i=0; i<planes.Length; i++) {
			planes[i] = SCR_Pool.GetFreeObject (PFB_Plane);
			planes[i].transform.position = new Vector3(0, PLANE_Y, PLANE_LENGTH * i);
		}
		
		spawnCount = 0;
		spawnIndex = 0;
		musicDelay = SCR_Cube.SPAWN_Z / SPEED_Z - SCR_MusicData.instance.GetData()[0] + MUSIC_OFFSET;
		brickCount = musicDelay;
		
		changeColorInterval = 0.5f;
		
		PickRandomColor();
		currentColor = targetColor;
		ApplyColor();
		
		brickCount = BRICK_SPAWN_LATENCY * 3;
		
		CTN_Replay.SetActive (false);
    }

    private void Update() {
		float dt = Time.deltaTime;
		
		// =========================================================================================================================
		// Move bottom plane
		for (int i=0; i<planes.Length; i++) {
			planes[i].transform.position = new Vector3(0, PLANE_Y, planes[i].transform.position.z - SPEED_Z * dt * loseDelay);
			if (planes[i].transform.position.z < -PLANE_LENGTH) {
				planes[i].transform.position = new Vector3(0, PLANE_Y, planes[i].transform.position.z + PLANE_LENGTH * planes.Length);
			}
		}
		
		// =========================================================================================================================
		// Spawn cubes
		if (musicDelay > 0) {
			musicDelay -= dt;
			if (musicDelay <= 0) {
				SND_Music.Play();
				IMG_Tutorial.SetActive (false);
			}
		}
		if (spawnIndex < SCR_MusicData.instance.GetData().Length) {
			spawnCount += dt;
			if (musicDelay <= 0) {
				spawnCount = SND_Music.time + SCR_Cube.SPAWN_Z / SPEED_Z - SCR_MusicData.instance.GetData()[0] + MUSIC_OFFSET;
			}
			if (spawnCount >= SCR_MusicData.instance.GetData()[spawnIndex]) {
				spawnIndex ++;
				GameObject tempCube = SCR_Pool.GetFreeObject (PFB_Cube);
				tempCube.GetComponent<SCR_Cube>().Spawn();
				SCR_ProgressBar.instance.SetProgress (1.0f * spawnIndex / SCR_MusicData.instance.GetData().Length);
			}
		
			brickCount -= dt;
			if (brickCount <= 0) {
				float brickX = SCR_Brick.SPAWN_X;
				
				if (Random.Range(-10, 10) > 0) {
					brickX = -brickX;
				}
				
				bool spawn = true;
				List<GameObject> cubes = SCR_Pool.GetObjectList(SCR_Action.instance.PFB_Cube);
				for (int i=0; i<cubes.Count; i++) {
					if (cubes[i].activeSelf) {
						if (cubes[i].transform.position.z < SCR_Brick.SPAWN_Z + SCR_Cube.SIZE_Z * 1.5f
						&&  cubes[i].transform.position.z > SCR_Brick.SPAWN_Z - SCR_Cube.SIZE_Z * 1.3f
						&&  Mathf.Sign(cubes[i].transform.position.x) == Mathf.Sign(brickX)) {
							spawn = false;
							break;
						}
					}
				}
				
				if (spawn == true) {
					brickCount = BRICK_SPAWN_LATENCY;
					if (Random.Range(0, 100) > OBSTACLE_CHANCE) {
						GameObject tempBrick = SCR_Pool.GetFreeObject (PFB_Brick);
						tempBrick.GetComponent<SCR_Brick>().Spawn(brickX);
					}
					else {
						GameObject tempObstacle = SCR_Pool.GetFreeObject (PFB_Obstacle);
						tempObstacle.GetComponent<SCR_Obstacle>().Spawn();
					}
					brickCount = BRICK_SPAWN_LATENCY;
				}
				else {
					brickCount = BRICK_BLOCK_LATENCY;
				}
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
		// Handle losing
		if (lose && loseDelay > 0) {
			loseDelay -= dt;
			SND_Music.volume = loseDelay;
			if (loseDelay <= 0) {
				loseDelay = 0;
				CTN_Replay.SetActive (true);
			}
		}
		// =========================================================================================================================
    }
	
	private void ApplyColor() {
		MAT_Cube[0].SetColor("_Color", currentColor); 
		MAT_Cube[1].SetColor("_Color", currentColor); 
		
		ball.GetComponent<SCR_Ball>().SetColor (currentColor);
		
		List<GameObject> explosions = SCR_Pool.GetObjectList(PFB_Explosion);
		for (int i=0; i<explosions.Count; i++) {
			explosions[i].GetComponent<SCR_Explosion>().SetColor (currentColor);
		}
	}
	
	public void ChangePlaneColor(Color color) {
		MAT_Plane.SetColor("_Color", color); 
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
	
	public void Score() {
		score ++;
		TXT_Score.text = "" + score;
	}
}
