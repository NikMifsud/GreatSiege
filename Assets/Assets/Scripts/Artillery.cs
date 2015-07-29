﻿using UnityEngine;
using System.Collections;

public class Artillery : MonoBehaviour {
	public float curr_Health;
	public float max_Health;
	public int Armor;
	public int Movement;
	public int AttackRange;
	public int Attack;
	public int DamageTaken;
	public float Movementtime;
	public GameObject healthBar;
	public bool Timeractive;
	
	// Use this for initialization
	void Start () {
		max_Health = 250;
		curr_Health = 250;
		Armor = 20;
		Movement = 1;
		AttackRange = 3;
		Attack = 80;
		Movementtime = 30;
	}
	public void Movementcheck(){
		Movementtime = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (Movementtime >= 30) {
			Timeractive = false;
		}
		if (Timeractive == false) {
			Movementtime -=  Time.deltaTime;
		}
		if (Movementtime <= 0) {
			Timeractive = true;
		}
		if (DamageTaken <= 19) {
			DamageTaken = 0;
		}
		if (curr_Health == 0) {
			Destroy (this.gameObject);
		}
		if (DamageTaken >= 1) {
			curr_Health = curr_Health + (Armor - DamageTaken);
			DamageTaken = 0;
		}
		float calc_Health = curr_Health / max_Health;
		SetHealthBar (calc_Health);
	}
	
	void DealtDamage (){
		curr_Health = curr_Health + (Armor - DamageTaken);
	}
		public void SetHealthBar (float myHealth){
			healthBar.transform.localScale = new Vector3 (myHealth,healthBar.transform.localScale.y, healthBar.transform.localScale.x);
				healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}