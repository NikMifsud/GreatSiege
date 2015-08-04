﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerbarcannon : MonoBehaviour {
	public float myAmount;
	public Artillery activemovement;
	public Animator timeranim;
	public Animator finishedanim;
	public bool done;
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
		
		if (activemovement.canMove == false) {
			timeranim.SetBool ("movement", false);
			finishedanim.SetBool ("finished", false);
			Image myImage = GetComponent<Image> ();
			if(activemovement.isMud)
				myAmount = activemovement.Movementtime / 35;
			else
				myAmount = activemovement.Movementtime / 30;
			myImage.fillAmount = myAmount;
		}
		Transform cannon = this.transform.Find ("cannon");
	}
}