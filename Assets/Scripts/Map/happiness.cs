﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//can be removed since deleting happiness meter

public class happiness : MonoBehaviour {
	public int HappyUnits; 
	public Text DisplayHappy;
	public float number;
	
	
	// Use this for initialization
	void Start () {
		HappyUnits = 100;
	}
	
	// Update is called once per frame
	void Update () {
		number += Time.deltaTime;
		if (number >= 5){
			HappyUnits += 1;
			number = 0;
		}
		if (HappyUnits >= 100) {
			HappyUnits = 100;
		}
	}
}