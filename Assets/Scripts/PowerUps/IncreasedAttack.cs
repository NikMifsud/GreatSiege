using UnityEngine;
using System.Collections.Generic;

public class IncreasedAttack : MonoBehaviour {
	
	public bool CooldownButtonPressed;
	float attackTimer=0;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer >= 1) {
			attackTimer += Time.deltaTime;
		}
		
		Debug.Log (attackTimer);
		
		if (attackTimer >= 45) {
			attackTimer = 0;
			
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			
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
					if(unit.name == "rangedsoldierpivot 1 1(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("ranged");
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().Attack = 30;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Cannonpivotpoint(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("cannon");
						myUnitTransform.gameObject.GetComponent<Artillery>().Attack = 80;
					}
				}
			}
		}
		
		if (CooldownButtonPressed == true) {
			attackTimer = 1;
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			
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
					if(unit.name == "rangedsoldierpivot 1 1(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("ranged");
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().Attack = 40;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Cannonpivotpoint(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("cannon");
						myUnitTransform.gameObject.GetComponent<Artillery>().Attack = 90;
					}
				}
			}
			gameMaster.gameState = 0;
			CooldownButtonPressed = false;
		}
	}
	
	public void ButtonClicked () {
		gameMaster.gameState = 3;
		playerControl.highlightingTiles = false;
		CooldownButtonPressed = true;
	}
}
