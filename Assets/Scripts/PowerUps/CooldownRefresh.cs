using UnityEngine;
using System.Collections.Generic;

public class CooldownRefresh : MonoBehaviour {

	public bool CooldownButtonPressed;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	
	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}

	// Update is called once per frame
	void Update () {
		if (CooldownButtonPressed == true) {
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");

			if(footUnits != null){
				foreach(GameObject unit in footUnits){
					Debug.Log (unit.name);
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Movementtime = 10f;
						unit.gameObject.GetComponent<Footsoldier>().canMove = true;
						unit.gameObject.GetComponent<Footsoldier>().attack = false;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "rangedsoldierpivot 1 1(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("ranged");
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().Movementtime = 15f;
						myUnitTransform.gameObject.GetComponent<Rangedsoldier>().canMove = true;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Cannonpivotpoint(Clone)"){
						Transform myUnitTransform = unit.transform.FindChild ("cannon");
						myUnitTransform.gameObject.GetComponent<Artillery>().Movementtime = 30f;
						myUnitTransform.gameObject.GetComponent<Artillery>().canMove = true;
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
