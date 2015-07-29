using UnityEngine;
using System.Collections;

public class Footsoldier : MonoBehaviour {
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
		max_Health = 100;
		curr_Health = 100;
		Armor = 10;
		Movement = 2;
		AttackRange = 1;
		Attack = 50;
		Movementtime = 10;
	}
	public void Movementcheck(){
		Movementtime = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (Movementtime >= 10) {
			Timeractive = false;
		}
		if (Timeractive == false) {
			Movementtime -=  Time.deltaTime;
		}
		if (Movementtime <= 0) {
			Timeractive = true;
		}

		if (DamageTaken <= 9) {
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
		//curr_Health = curr_Health + (Armor - DamageTaken);
	}

	public void SetHealthBar (float myHealth){
		healthBar.transform.localScale = new Vector3 (myHealth,healthBar.transform.localScale.y, healthBar.transform.localScale.x);
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
}
