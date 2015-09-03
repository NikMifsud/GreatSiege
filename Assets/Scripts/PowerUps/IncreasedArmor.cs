using UnityEngine;
using System.Collections.Generic;

public class IncreasedArmor : MonoBehaviour {
	
	public bool CooldownButtonPressed;
	float armorTimer=0;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public float timer;
	public Animator anim;
	public AudioClip clip;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		source = Camera.main.GetComponent<AudioSource> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		armorTimer = 0;
		timer = 90;
	}

	// Update is called once per frame
	void Update () {
		if (armorTimer >= 1) {
			armorTimer += Time.deltaTime;
		}

		if (timer <= 90) {
			timer += Time.deltaTime;
		}


		if (armorTimer >= 45) {
			armorTimer = 0;
			anim.SetBool("isShield",false);
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			GameObject[] pikeUnits = GameObject.FindGameObjectsWithTag("PikeUnit");

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
					if(unit.name == "Ranged(Clone)"){
						unit.gameObject.GetComponent<Rangedsoldier>().Armor = 0;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Musketeer(Clone)"){
						unit.gameObject.GetComponent<Musket>().Armor = 0;
					}
				}
			}
			if(pikeUnits != null){
				foreach(GameObject unit in pikeUnits){
					if(unit.name == "Pike(Clone)"){
						unit.gameObject.GetComponent<Pike>().Armor = 20;
					}
				}
			}
		}

		if (CooldownButtonPressed == true) {
			timer = 0;
			armorTimer = 1;
			source.PlayOneShot(clip,1f);
			GameObject[] footUnits = GameObject.FindGameObjectsWithTag("FootUnit");
			GameObject[] rangedUnits = GameObject.FindGameObjectsWithTag("RangedUnit");
			GameObject[] siegeUnits = GameObject.FindGameObjectsWithTag("SiegeUnit");
			GameObject[] pikeUnits = GameObject.FindGameObjectsWithTag("PikeUnit");
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
					if(unit.name == "Ranged(Clone)"){
						unit.gameObject.GetComponent<Rangedsoldier>().Armor = 10;
					}
				}
			}
			if(siegeUnits != null){
				foreach(GameObject unit in siegeUnits){
					if(unit.name == "Musketeer(Clone)"){
						unit.gameObject.GetComponent<Musket>().Armor = 10;
					}
				}
			}
			if(pikeUnits != null){
				foreach(GameObject unit in pikeUnits){
					if(unit.name == "Pike(Clone)"){
						unit.gameObject.GetComponent<Pike>().Armor = 10;
					}
				}
			}
			gameMaster.gameState = 0;
			CooldownButtonPressed = false;

		}
	}
	
	public void ButtonClicked () {
		if (timer >= 90) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			CooldownButtonPressed = true;
			anim.SetBool ("isShield",true);
		}
	}
}
