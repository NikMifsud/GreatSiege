using UnityEngine;
using System.Collections.Generic;

public class IncreasedAttack : MonoBehaviour {
	
	public bool CooldownButtonPressed;
	float attackTimer=0;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public float timer;
	public Animator anim;

	// Use this for initialization
	void Start () {
		timer = 90;
		attackTimer = 0;
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer >= 1) {
			attackTimer += Time.deltaTime;
		}

		if (timer <= 90) {
			timer += Time.deltaTime;
		}

		if (attackTimer >= 45) {
			anim.SetBool("isSword",false);
			attackTimer = 0;
			
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			GameObject[] pikeUnits = GameObject.FindGameObjectsWithTag("PikeUnit");

			if(footUnits != null){
				foreach(GameObject unit in footUnits){
					Debug.Log (unit.name);
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Attack = 50;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "Ranged(Clone)"){
						unit.gameObject.GetComponent<Rangedsoldier>().Attack = 30;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Musketeer(Clone)"){
						unit.gameObject.GetComponent<Musket>().Attack = 40;
					}
				}
			}
			if(pikeUnits != null){
				foreach(GameObject unit in pikeUnits){
					if(unit.name == "Pike(Clone)"){
						unit.gameObject.GetComponent<Pike>().Attack = 45;
					}
				}
			}

		}
		
		if (CooldownButtonPressed == true) {
			timer = 0;
			attackTimer = 1;
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			GameObject[] pikeUnits = GameObject.FindGameObjectsWithTag("PikeUnit");

			if(footUnits != null){
				foreach(GameObject unit in footUnits){
					Debug.Log (unit.name);
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Attack = 60;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "Ranged(Clone)"){
						unit.gameObject.GetComponent<Rangedsoldier>().Attack = 40;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Musketeer(Clone)"){
						unit.gameObject.GetComponent<Musket>().Attack = 50;
					}
				}
			}
			if(pikeUnits != null){
				foreach(GameObject unit in pikeUnits){
					if(unit.name == "Pike(Clone)"){
						unit.gameObject.GetComponent<Pike>().Attack = 55;
					}
				}
			}

			gameMaster.gameState = 0;
			CooldownButtonPressed = false;
		}
	}
	
	public void ButtonClicked () {
		if (timer >=90) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			CooldownButtonPressed = true;
			anim.SetBool("isSword",true);
		}
	}
}
