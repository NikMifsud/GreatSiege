using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Enemy : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
		gridManager = Camera.main.GetComponent<GridManager> ();
		max_Health = 100;
		curr_Health = 100;
		Armor = 10;
		Movement = 2;
		AttackRange = 1;
		canMove = true;
		Attack = 50;
		Movementtime = 10;
		isSelected = false;
	}

	public void Movementcheck(){
		Movementtime = 10;
	}

	public void DecreaseCooldown(){
		canMove = false;
	}

	// Update is called once per frame
	void Update () {
		if (Movementtime <= 0) {
			canMove = true;
			Movementtime = 10;
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
				//do something if hit object ie
				if(objectHit.collider.gameObject.tag == "AttackableDirtTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "DirtTile";
				}
				else if(objectHit.collider.gameObject.tag == "AttackableStoneTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "StoneTile";
				}
			else 	if(objectHit.collider.gameObject.tag == "AttackableMudTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "MudTile";
				}
			else 	if(objectHit.collider.gameObject.tag == "AttackableOutpostTile"){
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = true;
					objectHit.collider.gameObject.tag = "OutpostTile";
				}
			}
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
				//do something if hit object ie
				if (objectHit.collider.gameObject.tag == "AttackableDirtTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
					objectHit.collider.gameObject.tag = "DirtTile";
				} else if (objectHit.collider.gameObject.tag == "AttackableStoneTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
					objectHit.collider.gameObject.tag = "StoneTile";
				} else 	if (objectHit.collider.gameObject.tag == "AttackableMudTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
					objectHit.collider.gameObject.tag = "MudTile";
				} else 	if (objectHit.collider.gameObject.tag == "AttackableOutpostTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isEnemy = false;
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
					objectHit.collider.gameObject.tag = "OutpostTile";
				}
			}
		}
	}

	public void getNextMove(){
		bool attack = false;
		GameObject enemyToAttack = null;
		enemies = new ArrayList ();
		//1. find your surroundings and see what they are exactly
		//the more detail we have, the more precise decision we can make about things 

		//information available to us - number of enemies, types of near tiles, health and type of enemies.
		colliders = Physics.OverlapSphere(this.transform.position, 0.7f);
		enemyCounter = 0;
		footCounter = 0;
		rangedCounter = 0;
		siegeCounter = 0;
		allyCounter = 0;
		foreach(Collider colliderObject in colliders){
			if(colliderObject.gameObject.tag == "FootUnit" || colliderObject.gameObject.tag == "RangedUnit" || colliderObject.gameObject.tag == "SiegeUnit" || colliderObject.gameObject.tag == "SelectedFootUnit" || colliderObject.gameObject.tag == "SelectedRangedUnit" || colliderObject.gameObject.tag == "SelectedSiegeUnit"){
				enemies.Add(colliderObject.gameObject);
				enemyCounter++;
			}
			if(colliderObject.gameObject.tag == "FootUnit" || colliderObject.gameObject.tag == "SelectedFootUnit"){
				footCounter++;
			}
			if(colliderObject.gameObject.tag == "RangedUnit" || colliderObject.gameObject.tag == "SelectedRangedUnit"){
				rangedCounter++;
			}
			if(colliderObject.gameObject.tag == "SiegeUnit" || colliderObject.gameObject.tag == "SelectedSiegeUnit"){
				siegeCounter++;
			}
			if(colliderObject.gameObject.tag == "Enemy" || colliderObject.gameObject.tag == "AttackableEnemy"){
				allyCounter++;
			}
		}
		//Debug.Log ("Enemy Counter: " + enemyCounter + " Ally Counter: " + allyCounter);

		enemies.ToArray ();

		foreach (GameObject enemy in enemies) {
			if(enemy.tag == "SiegeUnit" || enemy.tag == "SelectedSiegeUnit"){
				if(enemy.GetComponent<Artillery>().curr_Health <= curr_Health){
					enemyToAttack = enemy;
					attack = true;
				}
			}
			if(enemy.tag == "RangedUnit" || enemy.tag == "SelectedRangedUnit"){
				if(enemy.GetComponent<Rangedsoldier>().curr_Health <= curr_Health){
					enemyToAttack = enemy;
					attack = true;
				}
			}
			if(enemy.tag == "FootUnit" || enemy.tag == "SelectedFootUnit"){
				if(enemy.GetComponent<Footsoldier>().curr_Health <= curr_Health){
					enemyToAttack = enemy;
					attack = true;
				}
			}
		}

		//if see weak enemy then go attack it 
		if (attack) {
			if(PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemyToAttack.GetComponent<CharacterMovement>().unitOriginalTile.tile).ToList().Count <= (AttackRange+1)){
				if(enemyToAttack.tag == "FootUnit" || enemyToAttack.tag == "SelectedFootUnit"){
					enemyToAttack.GetComponent<Footsoldier>().DealtDamage(Attack);
					DecreaseCooldown ();
				}
				if(enemyToAttack.tag == "RangedUnit" || enemyToAttack.tag == "SelectedRangedUni"){
					enemyToAttack.GetComponent<Rangedsoldier>().DealtDamage(Attack);
					DecreaseCooldown ();
				}
				if(enemyToAttack.tag == "SiegeUnit" || enemyToAttack.tag == "SelectedSiegeUnit"){
					enemyToAttack.GetComponent<Artillery>().DealtDamage(Attack);
					DecreaseCooldown ();
				}
			}
			else{
				//move to a tile close to the enemy 

				PathFinder.FindPath(this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemyToAttack.GetComponent<CharacterMovement>().unitOriginalTile.tile);
			}
		}

		//if more enemies than allies, move back one
		else if (enemyCounter > allyCounter) {
			availableMoves = new ArrayList ();

			//only these tile as the rest would be occupied
			GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
			GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
			GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
			GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");

			Vector2 currentPosition = gridManager.calcGridPos (this.transform.position);

			foreach (GameObject tile in MudTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y < currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in StoneTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y < currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in OutpostTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y < currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in DirtTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y < currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}

			//move the actual enemy back to a random tile. 
			availableMoves.ToArray ();

			int randomTileIndex = UnityEngine.Random.Range (0, availableMoves.Count);
			GameObject destinationTile = (GameObject)availableMoves [randomTileIndex];

			//keep the tiles updated
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = true;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile = destinationTile.GetComponent<TileBehaviour> ();
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = true;

			//set up the movement
			movement = destinationTile.transform.position;
			if(destinationTile.tag == "StoneTile"){
				movement.y = 0.12f;
			}
			else
				movement.y = 0.11f;

			//move 
			this.transform.position = movement;

			DecreaseCooldown ();

		} 
		//base case just move forward
		else {
			availableMoves = new ArrayList ();
			
			//only these tile as the rest would be occupied
			GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
			GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
			GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
			GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
			
			Vector2 currentPosition = gridManager.calcGridPos (this.transform.position);
			
			foreach (GameObject tile in MudTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in StoneTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in OutpostTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			foreach (GameObject tile in DirtTiles) {
				Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
				if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y > currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
					availableMoves.Add (tile);
				}
			}
			
			//move the actual enemy back to a random tile. 
			availableMoves.ToArray ();
			
			int randomTileIndex = UnityEngine.Random.Range (0, availableMoves.Count);
			GameObject destinationTile = (GameObject)availableMoves [randomTileIndex];
			
			//keep the tiles updated
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = true;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile = destinationTile.GetComponent<TileBehaviour> ();
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = false;
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isEnemy = true;
			
			//set up the movement
			movement = destinationTile.transform.position;
			if(destinationTile.tag == "StoneTile"){
				movement.y = 0.12f;
			}
			else
				movement.y = 0.11f;

			//move 
			this.transform.position = movement;
			
			DecreaseCooldown ();

		}
	}

/*	// used to see the sphere *DEBUGGING PURPOSES*
	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (this.transform.position, 0.7f);
	}
*/
}