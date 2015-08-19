﻿using UnityEngine;
using System.Collections;

public class FlamethrowerCollider : MonoBehaviour {

	bool attacked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
			if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "AttackableEnemy") {
				other.gameObject.GetComponent<Enemy> ().CheckDeath();
				other.gameObject.GetComponent<Enemy> ().DealtDamage (60);
				other.gameObject.GetComponent<Enemy> ().CheckDeath();
				other.gameObject.GetComponent<disablinghp> ().JustHit = true;
				other.gameObject.GetComponent<Enemy> ().CheckDeath();
			}
			if (other.gameObject.tag == "RangedEnemy" || other.gameObject.tag == "AttackableRangedEnemy") {
				other.gameObject.GetComponent<EnemyRanged> ().CheckDeath();
				other.gameObject.GetComponent<EnemyRanged> ().DealtDamage (60);
				other.gameObject.GetComponent<EnemyRanged> ().CheckDeath();
				other.gameObject.GetComponent<disablinghp> ().JustHit = true;
				other.gameObject.GetComponent<EnemyRanged> ().CheckDeath ();
			}
	}
} 