﻿using UnityEngine;
using System.Collections;

public class moveleft : MonoBehaviour {
	float movingCamera;
	public movingcamera haqa;
	// Use this for initialization
	void Start () {
		//float movingCamera = Camera.main.GetComponent<movingcamera>().speedMod;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver () {
		Debug.Log ("it burns");
		//movingCamera = 1;
		haqa.speedMod = -0.6f;
	}
	void OnMouseExit () {
		haqa.speedMod = 0;
	}
}