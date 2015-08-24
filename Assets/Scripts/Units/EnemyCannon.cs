using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class EnemyCannon : MonoBehaviour {
	
	public float curr_Health;
	public float max_Health;
	public int Armor;
	public int Movement;
	public int AttackRange;
	public int Attack;
	public int DamageTaken;
	public GridManager gridManager;
	public GameObject healthBar;
	public bool isSelected;
	public float Movementtime;
	public bool canMove;
	public ArrayList availableMoves;
	public ArrayList enemies;
	Collider[] colliders;
	int enemyCounter,allyCounter,footCounter,rangedCounter,siegeCounter;
	Vector3 movement;
	public bool stoneTileClose, isOnStone,enemyHit;
	GameObject stoneTile;
	public bool isMud;
	public AudioClip rangedHitEffect;
	private AudioSource source;
	public Statistics statistics;
	public int movedForward;

	// Use this for initialization
	void Start () {
		gridManager = Camera.main.GetComponent<GridManager> ();
		max_Health = 300;
		curr_Health = 300;
		Armor = 20;
		Movement = 1;
		AttackRange = 14;
		canMove = false;
		Attack = 90;
		Movementtime = 30;
		movedForward = 0;
		isSelected = false;
		stoneTileClose = false;
		enemyHit = false;
		source = Camera.main.GetComponent<AudioSource>();
		statistics = Camera.main.GetComponent<Statistics> ();
	}
	
	public void DecreaseCooldown(){
		canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMud) {
			Movementtime = 35;
			isMud = false;
		}
		
		if (Movementtime <= 0) {
			canMove = true;
			enemyHit = false;
			Movementtime = 30;
		}
		if (canMove == false) {
			Movementtime -= Time.deltaTime;
			
		}
		if (canMove == true) {
			getNextMove ();
		}
		if (curr_Health <= 0) {
			
			RaycastHit objectHit;
			Vector3 down = Vector3.down;
			if (Physics.Raycast(this.transform.position, down, out objectHit, 50))
			{
				if(objectHit.collider.gameObject.tag == "AttackableDirtTile" || objectHit.collider.gameObject.tag == "DirtTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "DirtTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableStoneTile" || objectHit.collider.gameObject.tag == "StoneTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "StoneTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableMudTile" || objectHit.collider.gameObject.tag == "MudTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "MudTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableOutpostTile" || objectHit.collider.gameObject.tag == "OutpostTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "OutpostTile";
				}
			}
			statistics.enemiesKilled += 1;
			Destroy (this.gameObject);
		}
		
		if (DamageTaken >= 1) {
			curr_Health = curr_Health - DamageTaken;
			DamageTaken = 0;
		}
		
		float calc_Health = curr_Health / max_Health;
		SetHealthBar (calc_Health);
	}
	
	public void DealtDamage (int DamageTaken){
		curr_Health = curr_Health - DamageTaken;
	}
	
	public void SetHealthBar (float myHealth){
		healthBar.transform.localScale = new Vector3 (myHealth,healthBar.transform.localScale.y, healthBar.transform.localScale.x);
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
	
	public void CheckDeath(){
		if (curr_Health <= 0) {	
			RaycastHit objectHit;
			Vector3 down = Vector3.down;
			if (Physics.Raycast (this.transform.position, down, out objectHit, 50)) {
				if(objectHit.collider.gameObject.tag == "AttackableDirtTile" || objectHit.collider.gameObject.tag == "DirtTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "DirtTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableStoneTile" || objectHit.collider.gameObject.tag == "StoneTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "StoneTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableMudTile" || objectHit.collider.gameObject.tag == "MudTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "MudTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableOutpostTile" || objectHit.collider.gameObject.tag == "OutpostTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "OutpostTile";
				}
			}
		}
	}
	
	public void getNextMove(){
		bool attack = false;
		enemies = new ArrayList ();
		//1. find your surroundings and see what they are exactly
		//the more detail we have, the more precise decision we can make about things 
		
		//information available to us - number of enemies, types of near tiles, health and type of enemies.
		colliders = Physics.OverlapSphere(this.transform.position, 2f);
		enemyCounter = 0;
		footCounter = 0;
		rangedCounter = 0;
		siegeCounter = 0;
		allyCounter = 0;
		foreach(Collider colliderObject in colliders){
			if(colliderObject.gameObject.tag == "FootUnit" || colliderObject.gameObject.tag == "RangedUnit" || colliderObject.gameObject.tag == "SiegeUnit" || colliderObject.gameObject.tag == "SelectedFootUnit" || colliderObject.gameObject.tag == "SelectedRangedUnit" || colliderObject.gameObject.tag == "SelectedSiegeUnit" || colliderObject.gameObject.tag == "SelectedPikeUnit"|| colliderObject.gameObject.tag == "PikeUnit"){
				enemies.Add(colliderObject.gameObject);
				enemyCounter++;
			}
			if(colliderObject.gameObject.tag == "Enemy" || colliderObject.gameObject.tag == "AttackableEnemy" || colliderObject.gameObject.tag == "RangedEnemy" || colliderObject.gameObject.tag == "AttackableRangedEnemy" || colliderObject.gameObject.tag == "EnemySiege" || colliderObject.gameObject.tag == "AttackableEnemySiege"){
				allyCounter++;
			}
		}
			enemies.ToArray ();
		if (enemies.Count <= 0 && isOnStone == false && movedForward <= 3) {
			MoveForward ();
		} else {
			int bias = UnityEngine.Random.Range (0, 101);
			if (bias > 25) {
				foreach (GameObject enemy in enemies) {
					//if enemy is close then attack him, if not then just move forward
					if (enemyHit == false && (enemy.tag == "SiegeUnit" || enemy.tag == "SelectedSiegeUnit")) {
						if (enemy.GetComponent<Pike> ().curr_Health <= curr_Health) {
							if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
								if (enemy.tag == "SiegeUnit" || enemy.tag == "SelectedSiegeUnit") {
									source.PlayOneShot (rangedHitEffect, 1.0f);
									enemy.GetComponent<disablinghp> ().JustHit = true;
									enemy.GetComponent<Musket> ().DealtDamage (Attack);
									enemy.GetComponent<Musket> ().CheckDeath ();
									enemyHit = true;
									DecreaseCooldown ();
								}
							}
						}
					}
					if (enemyHit == false && (enemy.tag == "PikeUnit" || enemy.tag == "SelectedPikeUnit")) {
						if (enemy.GetComponent<Pike> ().curr_Health <= curr_Health) {
							if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
								if (enemy.tag == "PikeUnit" || enemy.tag == "SelectedPikeUnit") {
									source.PlayOneShot (rangedHitEffect, 1.0f);
									enemy.GetComponent<disablinghp> ().JustHit = true;
									enemy.GetComponent<Pike> ().DealtDamage (Attack);
									enemy.GetComponent<Pike> ().CheckDeath ();
									enemyHit = true;
									DecreaseCooldown ();
								}
							}
						}
					}
					if (enemyHit == false && (enemy.tag == "RangedUnit" || enemy.tag == "SelectedRangedUnit")) {
						if (enemy.GetComponent<Rangedsoldier> ().curr_Health <= curr_Health) {
							if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
								if (enemy.tag == "RangedUnit" || enemy.tag == "SelectedRangedUnit") {
									source.PlayOneShot (rangedHitEffect, 1.0f);
									enemy.GetComponent<disablinghp> ().JustHit = true;
									enemy.GetComponent<Rangedsoldier> ().DealtDamage (Attack);
									enemy.GetComponent<Rangedsoldier> ().CheckDeath ();
									enemyHit = true;
									DecreaseCooldown ();
								}
							} 
						}

					}
					if (enemyHit == false && (enemy.tag == "FootUnit" || enemy.tag == "SelectedFootUnit")) {
						if (enemy.GetComponent<Footsoldier> ().curr_Health <= curr_Health) {
							if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
								if (enemy.tag == "FootUnit" || enemy.tag == "SelectedFootUnit") {
									source.PlayOneShot (rangedHitEffect, 1.0f);
									enemy.GetComponent<disablinghp> ().JustHit = true;
									enemy.GetComponent<Footsoldier> ().DealtDamage (Attack);
									enemy.GetComponent<Footsoldier> ().CheckDeath ();
									enemyHit = true;
									DecreaseCooldown ();
								}
							} 
						}

					}
				}
			}
			if(bias <= 25){
				source.PlayOneShot(rangedHitEffect,1.0f);
				GameObject.FindGameObjectWithTag("Fort").GetComponent<disablinghp>().JustHit = true;
				GameObject.FindGameObjectWithTag("Fort").GetComponent<StElmo> ().DealtDamage (Attack);
				enemyHit = true;
				DecreaseCooldown ();
			}
		}
	}
	
	void MoveForward(){
		
		availableMoves = new ArrayList ();
		stoneTileClose = false;
		//only these tile as the rest would be occupied
		GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
		GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
		GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
		GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
		
		Vector2 currentPosition = gridManager.calcGridPos (this.transform.position);
		
		foreach (GameObject tile in MudTiles) {
			Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
			if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement+1) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
				availableMoves.Add (tile);
			}
		}
		foreach (GameObject tile in StoneTiles) {
			Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
			if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement+1) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
				availableMoves.Add (tile);
				stoneTileClose = true;
				stoneTile = tile;
			}
		}
		foreach (GameObject tile in OutpostTiles) {
			Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
			if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement+1) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
				availableMoves.Add (tile);
			}
		}
		foreach (GameObject tile in DirtTiles) {
			Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
			if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement+1) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
				availableMoves.Add (tile);
			}
		}
		
		//move the actual enemy forward to a random tile. 
		if (availableMoves.Count > 0) {
			int randomTileIndex = UnityEngine.Random.Range (0, availableMoves.Count);
			availableMoves.ToArray ();
			GameObject destinationTile;
			if(stoneTileClose == false){
				destinationTile = (GameObject)availableMoves [randomTileIndex];
			}
			else{
				destinationTile = stoneTile;
			}
			
			//keep the tiles updated
			if(this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "AttackableDirtTile"){
				this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "DirtTile";
			}
			
			if(this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "AttackableStoneTile"){
				this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "StoneTile";
			}
			
			if(this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "AttackableMudTile"){
				this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "MudTile";
			}
			
			if(this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "AttackableOutpostTile"){
				this.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "OutpostTile";
			}
			
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = true;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile = destinationTile.GetComponent<TileBehaviour> ();
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = true;
			
			//set up the movement
			movement = destinationTile.transform.position;
			if (destinationTile.tag == "StoneTile") {
				movement.y = 0.2f;
				AttackRange = 5;
				isMud = false;
				isOnStone = true;
			}else if(destinationTile.tag == "MudTile"){
				movement.y = 0.16f;
				isOnStone = false;
				AttackRange = 4;
				isMud = true;
			}
			else {
				movement.y = 0.16f;
				isOnStone = false;
				isMud = false;
				AttackRange = 4;
			}
			
			//move 
			this.transform.position = movement;
			movedForward += 1;
		}
		DecreaseCooldown ();
		Camera.main.GetComponent<PlayerControl> ().highlightingTiles = false;
	}
	
		// used to see the sphere *DEBUGGING PURPOSES*
//	private void OnDrawGizmos() {
//		Gizmos.color = Color.red;
//		Gizmos.DrawWireSphere (this.transform.position, 2f);
//	}

}