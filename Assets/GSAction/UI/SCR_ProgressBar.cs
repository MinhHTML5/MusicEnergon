using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SCR_ProgressBar : MonoBehaviour {
	public const float MOVE_SPEED = 5.0f;
	
	public static SCR_ProgressBar instance;
	
    public GameObject sprContent;
	
	private float currentProgress = 0;
	private float targetProgress = 0;
	
    private void Start() {
        instance = this;
		currentProgress = 0;
		targetProgress = 0;
		UpdateProgress();
    }
	
	public void SetProgress (float progress) {
		if (progress > 1) progress = 1;
		if (progress < 0) progress = 0;
		
		targetProgress = progress;
	}

    private void Update() {
        float dt = Time.deltaTime;
		
		float moveSpeed = MOVE_SPEED * Mathf.Abs(currentProgress - targetProgress);
		if (moveSpeed < 0.05f) {
			moveSpeed = 0.05f;
		}
		
		if (currentProgress > targetProgress) {
			currentProgress -= moveSpeed * dt;
			if (currentProgress < targetProgress) {
				currentProgress = targetProgress;
			}
			UpdateProgress();
		}
		else if (currentProgress < targetProgress) {
			currentProgress += moveSpeed * dt;
			if (currentProgress > targetProgress) {
				currentProgress = targetProgress;
			}
			UpdateProgress();
		}
    }
	
	private void UpdateProgress() {
		sprContent.GetComponent<Image>().fillAmount = currentProgress;
	}
}
