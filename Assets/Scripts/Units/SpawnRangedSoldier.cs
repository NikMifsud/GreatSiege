using UnityEngine;
using System.Collections.Generic;

public class SpawnRangedSoldier : MonoBehaviour {
	public bool SpawnSoldier;
	public GameObject Soldier;
	private GameMaster gameMaster;
	public economy economy;
	private PlayerControl playerControl;
	public List<Transform> tiles;
	public Material highlightedTexture,mudTexture,dirtTexture,outpostTexture,stoneTexture;
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
					if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit" || hit.collider.tag == "FootUnit") {
						SpawnSoldier = false;
						gameMaster.gameState = 0;
					} else if (hit.collider.GetComponent<TileBehaviour> ().isPassable == true && (hit.collider.tag == "SpawnStoneTile" || hit.collider.tag == "SpawnDirtTile" || hit.collider.tag == "SpawnOutpostTile" || hit.collider.tag == "SpawnMudTile")) {
						var cubeTemp = hit.collider.transform.position;
						if (hit.collider.gameObject.tag == "SpawnStoneTile") {
							Soldier.transform.FindChild ("ranged").GetComponent<Rangedsoldier> ().AttackRange = 5;
							Soldier.transform.FindChild ("ranged").GetComponent<Rangedsoldier> ().Armor = 5;
							cubeTemp.y = 0.2f;
						} if(hit.collider.gameObject.tag == "SpawnOutpostTile"){
							cubeTemp.y = 0.1f;
							economy.outpost += 1;
						}
						if(hit.collider.gameObject.tag == "SpawnMudTile" || hit.collider.gameObject.tag == "SpawnDirtTile"){
							cubeTemp.y = 0.1f;
						}
						Transform myUnitTransform = Soldier.transform.FindChild ("ranged");
						myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = hit.collider.gameObject.GetComponent<TileBehaviour> ();
						Instantiate (Soldier, cubeTemp, Quaternion.identity);
						food.Food -= 50;
						hit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
						RemoveSpawnArea ();
						SpawnSoldier = false;
						gameMaster.gameState = 0;
						
					} else {
						RemoveSpawnArea ();
						SpawnSoldier = false;
						gameMaster.gameState = 0;
					}
				
			}else{
				RemoveSpawnArea ();
				SpawnSoldier = false;
				gameMaster.gameState = 0;
			}
		}
	}
	
	public void ButtonClicked () {
		playerControl.highlightingTiles = false;
		if (food.Food >= 50) {
			SpawnSoldier = true;
			GenerateSpawnArea ();
			gameMaster.gameState = 3;
		} else {
			SpawnSoldier = false;
			gameMaster.gameState = 0;
		}

	}

	public void GenerateSpawnArea(){
		tiles = new List<Transform>();
		//ArrayList tiles = new ArrayList ();
		Transform transform = GameObject.Find ("HexagonGrid").transform;
		foreach (Transform child in transform) {
			tiles.Add(child);
		}
		tiles.Reverse ();
		tiles.RemoveRange (22, (tiles.Count - 22));
		tiles.ToArray ();
		for (int k = 0; k < tiles.Count; k++) {
			if(tiles[k].gameObject.tag == "StoneTile")
				tiles[k].gameObject.tag = "SpawnStoneTile";
			else if(tiles[k].gameObject.tag == "DirtTile")
				tiles[k].gameObject.tag = "SpawnDirtTile";
			else if(tiles[k].gameObject.tag == "OutpostTile")
				tiles[k].gameObject.tag = "SpawnOutpostTile";
			else if(tiles[k].gameObject.tag == "MudTile")
				tiles[k].gameObject.tag = "SpawnMudTile";
			
			Transform mychildtransform = tiles[k].transform.FindChild("Cylinder");
			mychildtransform.GetComponent<Renderer> ().material = highlightedTexture;
		}
	}
	
	public void RemoveSpawnArea(){
		for (int k = 0; k < tiles.Count; k++) {
			if (tiles [k].gameObject.tag == "SpawnMudTile"){
				tiles [k].gameObject.tag = "MudTile";
				Transform mychildtransform = tiles[k].transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudTexture;
			}
			else if (tiles [k].gameObject.tag == "SpawnStoneTile"){
				tiles [k].gameObject.tag = "StoneTile";
				Transform mychildtransform = tiles[k].transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
			else if (tiles [k].gameObject.tag == "SpawnOutpostTile"){
				tiles [k].gameObject.tag = "OutpostTile";
				Transform mychildtransform = tiles[k].transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostTexture;
			}
			else if (tiles [k].gameObject.tag == "SpawnDirtTile"){
				tiles [k].gameObject.tag = "DirtTile";
				Transform mychildtransform = tiles[k].transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtTexture;
			}
		}
	}

}
