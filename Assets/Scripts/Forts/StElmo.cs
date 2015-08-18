using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;


public class StElmo : MonoBehaviour {
	public float curr_Health;
	public float max_Health;
	public GameObject text;
	public GameMaster gameMaster;
	public GameObject pauseCover;
	public int DamageTaken;

	public GameObject healthBar;

	
	// Use this for initialization
	void Start () {
		max_Health = 1000;
		curr_Health = 1000;
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}

	
	// Update is called once per frame
	void Update () {
		
		if (curr_Health > 1000) {
			curr_Health = 1000;
		}

		if (curr_Health <= 0) {
			Destroy (this.gameObject);
			Time.timeScale = 0;
			pauseCover.SetActive(false);
			gameMaster.gameState = 4;
			text.SetActive(true);
		}

		if (DamageTaken >= 1) {
			curr_Health = curr_Health - DamageTaken;
			DamageTaken = 0;
		}
		float calc_Health = curr_Health / max_Health;
		SetHealthBar (calc_Health);
	}
		

	
	public void DealtDamage (int DamageTaken){
		curr_Health = curr_Health - DamageTaken;
	}
	
	public void SetHealthBar (float myHealth){
		healthBar.transform.localScale = new Vector3 (myHealth,healthBar.transform.localScale.y, healthBar.transform.localScale.x);
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}
