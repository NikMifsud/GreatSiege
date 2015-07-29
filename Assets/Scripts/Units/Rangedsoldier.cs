using UnityEngine;
using System.Collections;

public class Rangedsoldier : MonoBehaviour {
	public float curr_Health;
	public float max_Health;
	public int Armor;
	public int Movement;
	public int AttackRange;
	public int Attack;
	public int DamageTaken;
	public float Movementtime;
	public bool canMove;
	public GameObject healthBar;
	public bool isSelected;

	// Use this for initialization
	void Start () {
		max_Health = 100;
		curr_Health = 100;
		Armor = 0;
		Movement = 2;
		AttackRange = 2;
		Attack = 30;
		canMove = true;
		isSelected = false;
		Movementtime = 20;
	}

	public void Movementcheck(){
		Movementtime = 20;
	}

	public void DecreaseCooldown(){
		canMove = false;
	}

	// Update is called once per frame
	void Update () {
		if (Movementtime <= 0) {
			canMove = true;
			Movementtime = 20;
		}
		if (canMove == false) {
			Movementtime -= Time.deltaTime;
		}
		if (curr_Health == 0) {
			Destroy (this.gameObject);
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