using UnityEngine;
using System.Collections.Generic;

public class SpawnFireWheel : MonoBehaviour {

	public bool SpawnSoldier;
	public GameObject Soldier;
	private PlayerControl playerControl;
	private GameMaster gameMaster;
	public List<Transform> tiles;
	public Material highlightedTexture,mudTexture,dirtTexture,outpostTexture,stoneTexture;
	public float timer;


	// Use this for initialization
	void Start () {
		timer = 45;
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (timer <= 45) {
			timer += Time.deltaTime;
		}

		if (SpawnSoldier == true && Input.GetMouseButtonDown (0)) {
			timer = 0;
			//deselect all those in the scene in order to avoid user error
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit" || hit.collider.tag == "FootUnit") {
					SpawnSoldier = false;
					gameMaster.gameState = 0;
				}	else if (hit.collider.GetComponent<TileBehaviour> ().isPassable == true && (hit.collider.tag == "SpawnStoneTile" || hit.collider.tag == "SpawnDirtTile" || hit.collider.tag == "SpawnOutpostTile" || hit.collider.tag == "SpawnMudTile")) {
					var cubeTemp = hit.collider.transform.position;
					if (hit.collider.gameObject.tag == "SpawnStoneTile") {
						cubeTemp.y = 0.2f;
					} 
					else{
						cubeTemp.y = 0.1f;
					}
					Soldier.GetComponent<CharacterMovement> ().unitOriginalTile = hit.collider.gameObject.GetComponent<TileBehaviour> ();
					Quaternion rotation = new Quaternion(90f,-90f,0f,0f);
					Instantiate (Soldier, cubeTemp, rotation);
					hit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
					SpawnSoldier = false;
					RemoveSpawnArea();
					gameMaster.gameState = 0;
				} else {
					gameMaster.gameState = 0;
					SpawnSoldier = false;
					RemoveSpawnArea();
				}
				
			}else{
				gameMaster.gameState = 0;
				SpawnSoldier = false;
				RemoveSpawnArea();
			}
		}
	}
	
	public void ButtonClicked () {
		if (timer >= 45) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			SpawnSoldier = true;
			GenerateSpawnArea ();
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
		tiles.RemoveRange (44, (tiles.Count - 44));
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
