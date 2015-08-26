using UnityEngine;
using System.Collections.Generic;

public class CooldownRefresh : MonoBehaviour {

	public bool CooldownButtonPressed;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public float timer;
	public AudioClip  clip;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = Camera.main.GetComponent<AudioSource> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		timer = 45;
	}

	// Update is called once per frame
	void Update () {
		if (timer <= 45) {
			timer += Time.deltaTime;
		}

		if (CooldownButtonPressed == true) {
			source.PlayOneShot(clip,1.0f);
			timer = 0;
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			GameObject[] pikeUnits = GameObject.FindGameObjectsWithTag("PikeUnit");
			if(footUnits != null){
				foreach(GameObject unit in footUnits){
				
					if(unit.name == "Foot(Clone)"){
						unit.gameObject.GetComponent<Footsoldier>().Movementtime = 10f;
						unit.gameObject.GetComponent<Footsoldier>().canMove = true;
						unit.gameObject.GetComponent<Footsoldier>().attack = false;
					}
				}
			}
			if(rangedUnits != null){
				foreach(GameObject unit in rangedUnits){
					if(unit.name == "Ranged(Clone)"){
						unit.gameObject.GetComponent<Rangedsoldier>().Movementtime = 15f;
						unit.gameObject.GetComponent<Rangedsoldier>().canMove = true;
					}
				}
			}
			if(pikeUnits != null){
				foreach(GameObject unit in pikeUnits){
					if(unit.name == "Pike(Clone)"){
						unit.gameObject.GetComponent<Pike>().Movementtime = 15f;
						unit.gameObject.GetComponent<Pike>().canMove = true;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Musketeer(Clone)"){
						unit.gameObject.GetComponent<Musket>().Movementtime = 15f;
						unit.gameObject.GetComponent<Musket>().canMove = true;
					}
				}
			}

			gameMaster.gameState = 0;
			CooldownButtonPressed = false;
		}
	}

	public void ButtonClicked () {
		if (timer >= 45) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			CooldownButtonPressed = true;
		}
	}
}
