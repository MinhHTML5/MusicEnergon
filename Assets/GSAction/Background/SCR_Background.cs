using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Background : MonoBehaviour {
	public Material MAT_Background;
	
    private void Start() {
        
    }

    private void Update() {
       
    }
	
	public void SetColor (Color color) {
		MAT_Background.SetColor("_Color", color);
	}
}
