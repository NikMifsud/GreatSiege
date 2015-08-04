﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerbarsoldier : MonoBehaviour {
	public float myAmount;
	public Footsoldier activemovement;
	public Animator timeranim;
	public Animator finishedanim;
	public bool done;
	public bool mudTimer;
	public bool canMove;
	// Use this for initialization
	void Start () {
		Animator timeranim = GetComponent<Animator>();
		done = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (activemovement.canMove == true) {
			timeranim.SetBool ("movement", true);
			finishedanim.SetBool ("finished", true);
		}

		if (activemovement.isMud) {
			mudTimer = true;
		}
	
		if (activemovement.canMove == false) {
			timeranim.SetBool ("movement", false);
			finishedanim.SetBool ("finished", false);
			Image myImage = GetComponent<Image> ();
			if(mudTimer)
				Debug.Log ("MudTimer");
				myAmount = activemovement.Movementtime / 15;
			if(!mudTimer) 
				myAmount = activemovement.Movementtime / 10;
			myImage.fillAmount = myAmount;
		}
		Transform foot = this.transform.Find ("FootSoldier");
	}
}