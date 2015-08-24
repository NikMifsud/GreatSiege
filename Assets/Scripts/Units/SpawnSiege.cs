using UnityEngine;
using System.Collections.Generic;

public class SpawnSiege : MonoBehaviour {
	public bool SpawnSoldier;
	public GameObject Soldier;
	private GameMaster gameMaster;
	public economy economy;
	private PlayerControl playerControl;
	public Material highlightedTexture,mudTexture,dirtTexture,outpostTexture,stoneTexture;
	public List<Transform> tiles;
	public economy food;
	// Use this for initialization
	void Start () {
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SpawnSoldier == true && Input.GetMouseButtonDown (0)) {
				//deselect all those in the scene in order to avoid user error
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit" || hit.collider.tag == "FootUnit" || hit.collider.tag == "PikeUnit") {
						SpawnSoldier = false;
						RemoveSpawnArea();
						playerControl.highlightingTiles = false;
						gameMaster.gameState = 0;
					} else if (hit.collider.GetComponent<TileBehaviour> ().isPassable == true && (hit.collider.tag == "FortTile")) {
						var cubeTemp = hit.collider.transform.position;
						Transform myUnitTransform = Soldier.transform.FindChild ("cannon");
						myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = hit.collider.gameObject.GetComponent<TileBehaviour> ();
						Instantiate (Soldier, cubeTemp, Quaternion.identity);
						food.Food -= 80;
						hit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
						RemoveSpawnArea ();
						playerControl.highlightingTiles = false;
						SpawnSoldier = false;
						gameMaster.gameState = 0;
						
					} else {
						playerControl.highlightingTiles = false;
						RemoveSpawnArea ();
						SpawnSoldier = false;
						gameMaster.gameState = 0;
					}
			}else{
				playerControl.highlightingTiles = false;
				RemoveSpawnArea ();
				SpawnSoldier = false;
				gameMaster.gameState = 0;
			}
		}
	}
	
	public void ButtonClicked () {
		playerControl.highlightingTiles = true;
		if (food.Food >= 80) {
			SpawnSoldier = true;
			GenerateSpawnArea ();
			gameMaster.gameState = 3;
		} else {
			SpawnSoldier = false;
			gameMaster.gameState = 0;
		}
	}

	public void GenerateSpawnArea(){


		GameObject[] transforms = GameObject.FindGameObjectsWithTag("FortTile");
		foreach(GameObject trans in transforms){
			Transform mychildtransform = trans.transform.FindChild("Cylinder");
			mychildtransform.GetComponent<Renderer> ().material = highlightedTexture;
		}


	}
	
	public void RemoveSpawnArea(){

		GameObject[] transforms = GameObject.FindGameObjectsWithTag("FortTile");
		foreach(GameObject trans in transforms){
			Transform mychildtransform = trans.gameObject.transform.FindChild("Cylinder");
			mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
		}
	}

}
