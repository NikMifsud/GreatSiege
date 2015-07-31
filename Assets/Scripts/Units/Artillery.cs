using UnityEngine;
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
	public bool canMove;
	public GameObject healthBar;
	public bool isSelected;
	
	// Use this for initialization
	void Start () {
		max_Health = 250;
		curr_Health = 250;
		Armor = 20;
		Movement = 1;
		AttackRange = 3;
		Attack = 80;
		isSelected = false;
		canMove = true;
		Movementtime = 30;
	}

	public void Movementcheck(){
		Movementtime = 30;
	}

	public void DecreaseCooldown(){
		canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Movementtime);
		if (Movementtime <= 0) {
			Movementtime = 30;
			canMove = true;
		}
		if (canMove == false) {
			Movementtime -= Time.deltaTime;
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
	
	public void DealtDamage (int DamageTaken){
		curr_Health = curr_Health - DamageTaken;
	}

	public void SetHealthBar (float myHealth){
		healthBar.transform.localScale = new Vector3 (myHealth,healthBar.transform.localScale.y, healthBar.transform.localScale.x);
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}

	public void CheckDeath(){
		if (curr_Health <= 0) {			
			RaycastHit objectHit;
			Vector3 down = Vector3.down;
			if (Physics.Raycast (this.transform.position, down, out objectHit, 50)) {
				if (objectHit.collider.gameObject.tag == "DirtTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else if (objectHit.collider.gameObject.tag == "StoneTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else 	if (objectHit.collider.gameObject.tag == "MudTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else 	if (objectHit.collider.gameObject.tag == "OutpostTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				}
			}
		}
	}

}
