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
	public ParticleEmitter fire;
	public ParticleEmitter fire2;
	public ParticleEmitter smoke;
	public GameObject healthBar;
	public Canvas statScreen;
	bool Leveldone;
	// Use this for initialization
	void Start () {
		Leveldone = false;
		max_Health = 3000;
		curr_Health = 3000;
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		fire = GameObject.Find("OuterCore").GetComponent<ParticleEmitter>();
		fire2 = GameObject.Find("InnerCore").GetComponent<ParticleEmitter>();
		smoke = GameObject.Find("smoke").GetComponent<ParticleEmitter>();
	}

	
	// Update is called once per frame
	void Update () {
		
		if (curr_Health > 3000) {
			curr_Health = 3000;
		}

		if (curr_Health <= 0 && Leveldone == false) {
		//	Destroy (this.gameObject);
			fire.minEmission = 40;
			fire.maxEmission = 40;
			smoke.minEmission = 40;
			smoke.maxEmission = 40;
			fire2.minEmission = 30;
			fire2.maxEmission = 30;
			statScreen.gameObject.SetActive(true);
			statScreen.gameObject.GetComponent<UpdateStatistics>().UpdateStatisticScreen();

			gameMaster.gameState = 4;
			Time.timeScale = 0;
			Leveldone = true;
			//text.SetActive(true);
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
