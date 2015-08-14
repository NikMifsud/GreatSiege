using UnityEngine;
using System.Collections.Generic;

public class IncreasedArmor : MonoBehaviour {
	
	public bool CooldownButtonPressed;
	float armorTimer=0;
	public PlayerControl playerControl;
	public GameMaster gameMaster;

	// Use this for initialization
	void Start () {
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		armorTimer = 0;
	}

	// Update is called once per frame
	void Update () {
		if (armorTimer >= 1) {
			armorTimer += Time.deltaTime;
		}

		if (armorTimer >= 45) {
			armorTimer = 0;

			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");

			if(footUnits != null){
				foreach(GameObject unit in footUnits){
					Debug.Log (unit.name);
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Armor = 10;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "rangedsoldierpivot 1 1(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("ranged");
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().Armor = 0;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Cannonpivotpoint(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("cannon");
						myUnitTransform.gameObject.GetComponent<Artillery>().Armor = 20;
					}
				}
			}
		}

		if (CooldownButtonPressed == true) {
			armorTimer = 1;
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");

			if(footUnits != null){
				foreach(GameObject unit in footUnits){
					Debug.Log (unit.name);
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Armor = 20;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "rangedsoldierpivot 1 1(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("ranged");
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().Armor = 10;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Cannonpivotpoint(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("cannon");
						myUnitTransform.gameObject.GetComponent<Artillery>().Armor = 30;
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
