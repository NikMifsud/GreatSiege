using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//Tilebehaviour is attached to each tile. For the exact location of the tile, have to access its Tile tile.
//handles the texturing/colouring of the tiles and makes use of the playerControl script and the powerUps to know what to highlight etc
//OnMouseEnter is used in conjunction with the health powerup
//bottom methods are extra 
public class TileBehaviour: MonoBehaviour
{
	public Tile tile;
	public int pointX;
	public int pointY;
	public Material mudTexture;
	public Material mudTextureHighlighted;
	public Material dirtTexture;
	public Material dirtTextureHighlighted;
	public Material stoneTexture;
	public Material stoneTextureHighlighted;
	public Material outpostTexture;
	public Material outpostTextureHighlighted;
	public Material highlightedMaterial;
	public Vector3 endPosition;
	public HealthPowerUps healthPowerUps;
	public GameMaster gameMaster;
	public Material allyMaterial;
	public Material enemyMaterial;
	public PlayerControl playerControl;
	public bool isPassable;
	public bool isEnemy;
	public bool powerUp;

	//have a memory in order to keep the colours consistant 
	public TileBehaviour previousTile;
	
	void Start(){

		gameMaster = Camera.main.GetComponent<GameMaster> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		healthPowerUps = GameObject.Find("Medic").GetComponent<HealthPowerUps>();
		this.isPassable = true;
		this.isEnemy = false;
		this.powerUp = false;
		pointX = this.tile.X;
		pointY = this.tile.Y;
	}
	
	void Update(){
		if (this.pointX == 2 && this.pointY == 14) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 0 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 2 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -7 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -6 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -4 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 3 && this.pointY == 14) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 1 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 1 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -6 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -5 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -2 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 0 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 3 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -6 && this.pointY == 14) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -8 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 5 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -5 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 2 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -1 && this.pointY == 16) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -7 && this.pointY == 15) {
			Destroy(this.gameObject);
		}
		if (this.pointX == 4 && this.pointY == 13) {
			Destroy(this.gameObject);
		}
		if (this.pointX == -7 && this.pointY == 14) {
			Destroy(this.gameObject);
		}
		if (this.isEnemy == false && this.isPassable == false) {
			Transform mychildtransform = this.transform.FindChild("Cylinder");
			mychildtransform.GetComponent<Renderer> ().material = allyMaterial;
		}

		if(playerControl.highlightingTiles == false && this.powerUp == false){
			if(this.tag == "MudTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudTexture;
			}
			if(this.tag == "StoneTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
			if(this.tag == "OutpostTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostTexture;
			}
			if(this.tag == "DirtTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtTexture;
			}
			if(this.tag == "FortTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
		}

		if (healthPowerUps.HealthButtonPressed == false && playerControl.highlightingTiles == false) {
			if(this.tag == "MudTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudTexture;
			}
			if(this.tag == "StoneTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
			if(this.tag == "OutpostTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostTexture;
			}
			if(this.tag == "DirtTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtTexture;
			}
			if(this.tag == "FortTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
		}

	}
	
	void OnMouseEnter()
	{
//		GridManager.instance.selectedTile = tile;
		//when mouse is over some tile, the tile is passable and the current tile is neither destination nor origin tile, change color to orange
	    
		if (healthPowerUps.HealthButtonPressed == true) {
			powerUp = true;
			GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
			GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
			GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
			GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
			foreach (GameObject tile in MudTiles) { 
				var path = PathFinder.FindPath (gameObject.GetComponent<TileBehaviour>().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
				if (path.ToList ().Count <= 3) {
					tile.gameObject.GetComponent<TileBehaviour>().powerUp = true;
					Transform mychildtransform = tile.transform.FindChild ("Cylinder");
					mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
					if(Input.GetMouseButtonDown (0)){
						if(tile.gameObject.GetComponent<TileBehaviour>().isPassable == false){
							RaycastHit objectHit;
							Vector3 up = Vector3.up;
							if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
								if (objectHit.collider.gameObject.tag == "FootSoldier") {
									objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "RangedSoldier") {
									objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 100;
								} 
								if (objectHit.collider.gameObject.tag == "SiegeUnit") {
									objectHit.collider.gameObject.GetComponent<Musket> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "PikeUnit") {
									objectHit.collider.gameObject.GetComponent<Pike> ().curr_Health += 100;
								}
							}
						}
					}
				}
				else tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
			}
			foreach (GameObject tile in StoneTiles) { 
				var path = PathFinder.FindPath (gameObject.GetComponent<TileBehaviour>().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
				if (path.ToList ().Count <= 3) {
					tile.gameObject.GetComponent<TileBehaviour>().powerUp = true;
					Transform mychildtransform = tile.transform.FindChild ("Cylinder");
					mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
					if(Input.GetMouseButtonDown (0)){
						if(tile.gameObject.GetComponent<TileBehaviour>().isPassable == false){
							RaycastHit objectHit;
							Vector3 up = Vector3.up;
							if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
								if (objectHit.collider.gameObject.tag == "FootSoldier") {
									objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "RangedSoldier") {
									objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 100;
								} 
								if (objectHit.collider.gameObject.tag == "SiegeUnit") {
									objectHit.collider.gameObject.GetComponent<Musket> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "PikeUnit") {
									objectHit.collider.gameObject.GetComponent<Pike> ().curr_Health += 100;
								}
							}
						}
					}
				}
				else tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
			}
			foreach (GameObject tile in OutpostTiles) { 
				var path = PathFinder.FindPath (gameObject.GetComponent<TileBehaviour>().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
				if (path.ToList ().Count <= 3) {
					tile.gameObject.GetComponent<TileBehaviour>().powerUp = true;
					Transform mychildtransform = tile.transform.FindChild ("Cylinder");
					mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
					if(Input.GetMouseButtonDown (0)){
						if(tile.gameObject.GetComponent<TileBehaviour>().isPassable == false){
							RaycastHit objectHit;
							Vector3 up = Vector3.up;
							if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
								if (objectHit.collider.gameObject.tag == "FootSoldier") {
									objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "RangedSoldier") {
									objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 100;
								} 
								if (objectHit.collider.gameObject.tag == "SiegeUnit") {
									objectHit.collider.gameObject.GetComponent<Musket> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "PikeUnit") {
									objectHit.collider.gameObject.GetComponent<Pike> ().curr_Health += 100;
								}
							}
						}
					}
				}
				else tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
			}
			foreach (GameObject tile in DirtTiles) { 
				var path = PathFinder.FindPath (gameObject.GetComponent<TileBehaviour>().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
				if (path.ToList ().Count <= 3) {
					tile.gameObject.GetComponent<TileBehaviour>().powerUp = true;
					Transform mychildtransform = tile.transform.FindChild ("Cylinder");
					mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
					if(Input.GetMouseButtonDown (0)){
						if(tile.gameObject.GetComponent<TileBehaviour>().isPassable == false){
							RaycastHit objectHit;
							Vector3 up = Vector3.up;
							if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
								if (objectHit.collider.gameObject.tag == "FootSoldier") {
									objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "RangedSoldier") {
									objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 100;
								} 
								if (objectHit.collider.gameObject.tag == "SiegeUnit") {
									objectHit.collider.gameObject.GetComponent<Musket> ().curr_Health += 100;
								}
								if (objectHit.collider.gameObject.tag == "PikeUnit") {
									objectHit.collider.gameObject.GetComponent<Pike> ().curr_Health += 100;
								}
							}
						}
					}
				}
				else tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
			}
		}

	}
	
	//changes back to fully transparent material when mouse cursor is no longer hovering over the tile
	void OnMouseExit()
	{
		GridManager.instance.selectedTile = null;
	}
	//called every frame when mouse cursor is on this tile
	//void OnMouseDown()
	//{

//			tile.Passable = true;
	//
	//		TileBehaviour originTileTB = GridManager.instance.originTileTB;
	//		//if user clicks on origin tile or origin tile is not assigned yet
	//
	//		if (this == originTileTB || originTileTB == null)
	//			originTileChanged ();
	//		else
	//			destTileChanged ();
	//	
		//	GridManager.instance.generateAndShowPath ();

	//}

	void originTileChanged()
	{
		var originTileTB = GridManager.instance.originTileTB;
		//deselect origin tile if user clicks on current origin tile
		if (this == originTileTB)
		{
			GridManager.instance.originTileTB = null;
			return;
		}
		//if origin tile is not specified already mark this tile as origin
		GridManager.instance.originTileTB = this;
		//Transform mychildtransformm = this.transform.FindChild("Cylinder");
		//mychildtransformm.GetComponent<Renderer> ().material = red;

	}

	void destTileChanged()
	{
		var destTile = GridManager.instance.destTileTB;

		//deselect destination tile if user clicks on current destination tile
		if (this == destTile)
		{
			GridManager.instance.destTileTB = null;
			GridManager.instance.originTileTB = null;
			return;
		}
		GridManager.instance.destTileTB = this;
		previousTile = destTile;
		if (previousTile != null) {
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (this.tag != "FortTile") {
			if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Fort") {

				Destroy (this.gameObject);
			}
		}

	//	if (collision.gameObject.tag == "Fort") {
	//		this.gameObject.transform.position += new Vector3(0,0.4f,0);
	//	}
	}
}