using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class FireWheel : MonoBehaviour {

	public int Movement;
	public int AttackRange;
	public int Attack;
	public GridManager gridManager;
	public float Movementtime;
	public bool canMove;
	public ArrayList availableMoves;
	public ArrayList enemies;
	Collider[] colliders;
	bool enemyHit,isOnOutpost, outpostTileClose;
	int enemyCounter,allyCounter,footCounter,rangedCounter,siegeCounter;
	Vector3 movement;
	GameObject outpostTile;
	public bool isMud;
	public AudioClip footHitEffect;
	private AudioSource source;
	public int tileCount;

	// Use this for initialization
	void Start () {
		tileCount = 0;
		gridManager = Camera.main.GetComponent<GridManager> ();
		Movement = 2;
		AttackRange = 1;
		canMove = false;
		Attack = 100;
		Movementtime = 0.5f;
		enemyHit = false;
		source = Camera.main.GetComponent<AudioSource> ();
	}
	
	public void DecreaseCooldown(){
		canMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMud) {
			Movementtime = 0.5f;
			isMud = false;
		}
		
		if (Movementtime <= 0) {
			canMove = true;
			enemyHit = false;
			Movementtime = 0.5f;
		}
		if (canMove == false) {
			Movementtime -= Time.deltaTime;
		}
		if (canMove == true) {
			tileCount++;
			MoveForward ();
		}

		if (tileCount > 7 || enemyHit) {
			this.GetComponent<CharacterMovement>().unitOriginalTile.GetComponent<TileBehaviour>().isPassable = true;
			Destroy(this.gameObject);

		}

	}

	void MoveForward(){
		enemies = new ArrayList ();
		colliders = Physics.OverlapSphere(this.transform.position, 0.7f);


		foreach (Collider colliderObject in colliders) {
			if (colliderObject.gameObject.tag == "Enemy" || colliderObject.gameObject.tag == "RangedEnemy" || colliderObject.gameObject.tag == "AttackableEnemy" || colliderObject.gameObject.tag == "AttackableRangedEnemy") {
				enemies.Add (colliderObject.gameObject);
			}
			enemies.ToArray ();
			foreach (GameObject enemy in enemies) {
				//if enemy is close then attack him, if not then just move forward
				if (enemy.tag == "Enemy" || enemy.tag == "AttackableEnemy") {
					if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
						if (enemy.tag == "Enemy" || enemy.tag == "RangedEnemy") {
							enemy.GetComponent<disablinghp> ().JustHit = true;
							enemy.GetComponent<Enemy> ().DealtDamage (Attack);
							enemy.GetComponent<Enemy> ().CheckDeath ();
							enemyHit = true;
							DecreaseCooldown ();
						}
					} 
				}
				if (enemy.tag == "RangedEnemy" || enemy.tag == "AttackableRangedEnemy") {
					if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, enemy.GetComponent<CharacterMovement> ().unitOriginalTile.tile).ToList ().Count <= (AttackRange + 1)) {
						if (enemy.tag == "RangedEnemy" || enemy.tag == "AttackableRangedEnemy") {
							enemy.GetComponent<disablinghp> ().JustHit = true;
							enemy.GetComponent<EnemyRanged> ().DealtDamage (Attack);
							enemy.GetComponent<EnemyRanged> ().CheckDeath ();
							enemyHit = true;
							DecreaseCooldown ();
						}
					} 
				}
			}
		}




		availableMoves = new ArrayList ();
		outpostTileClose = false;
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
				outpostTileClose = true;
				outpostTile = tile;
				availableMoves.Add (tile);
			}
		}
		foreach (GameObject tile in DirtTiles) {
			Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
			if (PathFinder.FindPath (this.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (Movement) && tilePosition.y < currentPosition.y && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true) {
				availableMoves.Add (tile);
			}
		}
		
		//move the actual enemy forward to a random tile. S
		if (availableMoves.Count > 0) {
			int randomTileIndex = UnityEngine.Random.Range (0, availableMoves.Count);
			availableMoves.ToArray ();
			GameObject destinationTile;
			if(outpostTileClose == false){
				destinationTile = (GameObject)availableMoves [randomTileIndex];
			}
			else{
				destinationTile = outpostTile;
			}

			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = true;
			this.GetComponent<CharacterMovement> ().unitOriginalTile = destinationTile.GetComponent<TileBehaviour> ();
			this.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = false;

			
			//set up the movement
			movement = destinationTile.transform.position;
			if (destinationTile.tag == "StoneTile") {
				movement.y = 0.2f;
				isMud = false;
				isOnOutpost = false;
			}else if(destinationTile.tag == "OutpostTile"){
				movement.y = 0.14f;
				isMud = false;
				isOnOutpost = true;
			}else if(destinationTile.tag == "MudTile"){
				movement.y = 0.14f;
				isMud = true;
				isOnOutpost = false;
			}
			else{
				movement.y = 0.14f;
				isMud = false;
				isOnOutpost = false;
			}
			//move 
			this.transform.position = movement;
		}
		DecreaseCooldown ();
	}
}