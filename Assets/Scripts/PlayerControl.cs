using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerControl : MonoBehaviour {
	
	private Camera PlayerCam; // Camera used by the player
	private GameMaster gameManager; // GameObject responsible for the management of the game
	public GameObject selectedCharacter;
	public GridManager gridManager;
	public economy economy;
	public int unitMovement;
	public int unitAttackRange;
	public int unitAttack;
	public bool highlightingTiles;
	public Material highlightedMaterial, stoneMaterial,outpostMaterial,mudMaterial,dirtMaterial;
	public Material enemyMaterial;
	public GameObject attackRangeIndicatorOne;
	public GameObject attackRangeIndicatorTwo;
	public GameObject attackRangeIndicatorThree;
	public GameObject attackRangeIndicatorFour;
	public bool canMove;

	// Use this for initialization
	void Start ()
	{
		PlayerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Find the Camera's GameObject from its tag
		gameManager = Camera.main.GetComponent<GameMaster> ();
		gridManager = Camera.main.GetComponent<GridManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Look for Mouse Inputs
		GetMouseInputs();

		if (gameManager.gameState == 1) {
			highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
			CheckIfPassable(unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
		}

		
	}
	
	// Detect Mouse Inputs
	void GetMouseInputs()
	{
		Ray _ray;
		RaycastHit _hitInfo;

		//select to move 
		if (gameManager.gameState == 0) {
			if (Input.GetMouseButtonDown (0)) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "FootUnit") {
						canMove = hit.collider.gameObject.GetComponent<Footsoldier>().canMove;
						if(canMove){
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedFootUnit";
							unitMovement = hit.collider.gameObject.GetComponent<Footsoldier>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Footsoldier>().AttackRange;
							unitAttack = hit.collider.gameObject.GetComponent<Footsoldier>().Attack;
							hit.collider.gameObject.GetComponent<Footsoldier>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					} if (hit.collider.gameObject.tag == "RangedUnit") {
						canMove = hit.collider.gameObject.GetComponent<Rangedsoldier>().canMove;
						if(canMove){
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedRangedUnit";
							unitMovement = hit.collider.gameObject.GetComponent<Rangedsoldier>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Rangedsoldier>().AttackRange;
							unitAttack = hit.collider.gameObject.GetComponent<Rangedsoldier>().Attack;
							hit.collider.gameObject.GetComponent<Rangedsoldier>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					} if (hit.collider.gameObject.tag == "SiegeUnit") {
						canMove = hit.collider.gameObject.GetComponent<Artillery>().canMove;
						if(canMove){
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedSiegeUnit";
							unitMovement = hit.collider.gameObject.GetComponent<Artillery>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Artillery>().AttackRange;
							unitAttack = hit.collider.gameObject.GetComponent<Artillery>().Attack;
							hit.collider.gameObject.GetComponent<Artillery>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					}
					else {
						return;
					}
				}
			}
		}

		// Move the piece if the gameState is 1
		else if (gameManager.gameState == 1) {
			// On Left Click
			if (Input.GetMouseButtonDown (0) && canMove) {
				_ray = PlayerCam.ScreenPointToRay (Input.mousePosition); // Specify the ray to be casted from the position of the mouse click
				
				// Raycast and verify that it collided
				if (Physics.Raycast (_ray, out _hitInfo)) {
					// Select the piece if it has the good Tag
					if (_hitInfo.collider.gameObject.tag == "MudTile" || _hitInfo.collider.gameObject.tag == "StoneTile" || _hitInfo.collider.gameObject.tag == "DirtTile" || _hitInfo.collider.gameObject.tag == "OutpostTile") {
							Vector3 movement = _hitInfo.collider.gameObject.transform.position;
							if (_hitInfo.collider.gameObject.tag == "StoneTile") {
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									selectedCharacter.GetComponent<Footsoldier> ().Armor = 15;
									movement.y = 0.2f;
								}
							    if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){

									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 3;
									selectedCharacter.GetComponent<Rangedsoldier> ().Armor = 5;
									movement.y = 0.2f;
								}
							    if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									
									selectedCharacter.GetComponent<Artillery> ().AttackRange = 4;
									selectedCharacter.GetComponent<Artillery> ().Armor = 25;
									movement.y = 0.2f;
								}
							}
							if(_hitInfo.collider.gameObject.tag == "OutpostTile"){
								economy.outpost += 1;
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									
									selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
									selectedCharacter.GetComponent<Footsoldier> ().Armor = 10;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
									
									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 2;
									selectedCharacter.GetComponent<Rangedsoldier> ().Armor = 0;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									
									selectedCharacter.GetComponent<Artillery> ().AttackRange = 3;
									selectedCharacter.GetComponent<Artillery> ().Armor = 20;
									movement.y = 0.1f;
								}
							}
						if(_hitInfo.collider.gameObject.tag == "MudTile" ){
							if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
								
								selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
								selectedCharacter.GetComponent<Footsoldier> ().Armor = 10;
								selectedCharacter.GetComponent<Footsoldier> ().isMud = true;
								movement.y = 0.1f;
							}
							if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
								
								selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 2;
								selectedCharacter.GetComponent<Rangedsoldier> ().Armor = 0;
								selectedCharacter.GetComponent<Rangedsoldier> ().isMud = true;
								movement.y = 0.1f;
							}
							if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
								
								selectedCharacter.GetComponent<Artillery> ().AttackRange = 3;
								selectedCharacter.GetComponent<Artillery> ().Armor = 20;
								selectedCharacter.GetComponent<Artillery> ().isMud = true;
								movement.y = 0.1f;
							}
						}
							if (_hitInfo.collider.gameObject.tag == "MudTile" || _hitInfo.collider.gameObject.tag == "OutpostTile" || _hitInfo.collider.gameObject.tag == "DirtTile") {
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									
									selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
									selectedCharacter.GetComponent<Footsoldier> ().Armor = 10;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){

									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 2;
									selectedCharacter.GetComponent<Rangedsoldier> ().Armor = 0;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){

									selectedCharacter.GetComponent<Artillery> ().AttackRange = 3;
									selectedCharacter.GetComponent<Artillery> ().Armor = 20;
									movement.y = 0.1f;
								}

							}
							selectedCharacter.GetComponent<CharacterMovement> ().destinationTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour> ();

							var path = PathFinder.FindPath (selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile, selectedCharacter.GetComponent<CharacterMovement> ().destinationTile.tile);

							//move if in the allowed distance 

							if (path.ToList ().Count <= (unitMovement + 1) && selectedCharacter.GetComponent<CharacterMovement> ().destinationTile.isPassable == true) {
								selectedCharacter.transform.position = movement;
								selectedCharacter.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = true;
								if(selectedCharacter.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "OutpostTile" && _hitInfo.collider.gameObject.tag != "OutpostTile"){
									economy.outpost -= 1;
								}
								selectedCharacter.GetComponent<CharacterMovement> ().unitOriginalTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour> ();
								selectedCharacter.GetComponent<CharacterMovement> ().unitOriginalTile.GetComponent<TileBehaviour> ().isPassable = false;
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
									selectedCharacter.gameObject.GetComponent<Footsoldier> ().DecreaseCooldown();
									selectedCharacter.gameObject.tag = "FootUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
									selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
									selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
									selectedCharacter.gameObject.tag = "RangedUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
									selectedCharacter.gameObject.GetComponent<Artillery> ().DecreaseCooldown();
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								revertbackEnemies();
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								highlightingTiles = false;
								gameManager.gameState = 0;
							}
						

					}else if(_hitInfo.collider.gameObject.tag == "AttackableEnemy"){
						//do the damage
						_hitInfo.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
						//revert back
						_hitInfo.collider.transform.parent.tag = "Enemy";
						_hitInfo.collider.gameObject.tag = "Enemy";
						_hitInfo.collider.gameObject.GetComponent<Enemy>().CheckDeath();
						RaycastHit objectHit;
						Vector3 down = Vector3.down;
						if (Physics.Raycast(_hitInfo.collider.transform.position, down, out objectHit, 50))
						{
							if(objectHit.collider.gameObject.tag == "AttackableDirtTile"){
								objectHit.collider.gameObject.tag = "DirtTile";
							}
							if(objectHit.collider.gameObject.tag == "AttackableStoneTile"){
								objectHit.collider.gameObject.tag = "StoneTile";
							}
							if(objectHit.collider.gameObject.tag == "AttackableOutpostTile"){
								objectHit.collider.gameObject.tag = "OutpostTile";
							}
							if(objectHit.collider.gameObject.tag == "AttackableMudTile"){
								objectHit.collider.gameObject.tag = "MudTile";
							}
						}
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Artillery> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableDirtTile"){
						//do damage
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast(_hitInfo.collider.transform.position, up, out objectHit, 50))
						{
							if(objectHit.collider.gameObject.tag == "AttackableEnemy"){
								objectHit.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								objectHit.collider.transform.parent.tag = "Enemy";
								objectHit.collider.gameObject.tag = "Enemy";
								objectHit.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
						}
						//revert back
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						_hitInfo.collider.gameObject.tag = "DirtTile";
						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableMudTile"){
						//do damage
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast(_hitInfo.collider.transform.position, up, out objectHit, 50))
						{
							if(objectHit.collider.gameObject.tag == "AttackableEnemy"){
								objectHit.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								objectHit.collider.transform.parent.tag = "Enemy";
								objectHit.collider.gameObject.tag = "Enemy";
								objectHit.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
						}
						//revert back
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						_hitInfo.collider.gameObject.tag = "MudTile";
						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableStoneTile"){
						//do damage
						RaycastHit objectHit;
						Vector3 up = Vector3.up; //(0,1,0)
						if (Physics.Raycast(_hitInfo.collider.transform.position, up, out objectHit, 50))
						{
							if(objectHit.collider.gameObject.tag == "AttackableEnemy"){
								objectHit.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								objectHit.collider.transform.parent.tag = "Enemy";
								objectHit.collider.gameObject.tag = "Enemy";
								objectHit.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
						}
						//revert back
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						_hitInfo.collider.gameObject.tag = "StoneTile";
						
						revertbackEnemies();

						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableOutpostTile"){
						//do damage
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast(_hitInfo.collider.transform.position, up, out objectHit, 50))
						{
							if(objectHit.collider.gameObject.tag == "AttackableEnemy"){
								objectHit.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								objectHit.collider.transform.parent.tag = "Enemy";
								objectHit.collider.gameObject.tag = "Enemy";
								objectHit.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
						}
						//revert back
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}

						_hitInfo.collider.gameObject.tag = "OutpostTile";
						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
					else if (_hitInfo.collider.gameObject.tag == "SelectedFootUnit" || _hitInfo.collider.gameObject.tag == "SelectedRangedUnit" || _hitInfo.collider.gameObject.tag == "SelectedSiegeUnit"){

						revertbackEnemies();

						highlightingTiles = false;
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Artillery>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						gameManager.gameState = 0;

					}
				}
			}
		}else if (gameManager.gameState == 3) {
			if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
				selectedCharacter.gameObject.tag = "FootUnit";
			}
			if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
				selectedCharacter.gameObject.tag = "RangedUnit";
			}
			if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
				selectedCharacter.gameObject.tag = "SiegeUnit";
			}
		}
	}
	
	public void highlightAvailableTiles(int movement, Tile originalTile){
		highlightingTiles = true;
		//1. find all the tiles in the scene
		GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
		GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
		GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
		GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");

		foreach (GameObject tile in MudTiles) { 
			var path = PathFinder.FindPath (originalTile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == true) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
			} 

			if (path.ToList ().Count <= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isEnemy == true) {
				tile.gameObject.tag = "AttackableMudTile";
				RaycastHit objectHit;
				Vector3 up = Vector3.up;
				if (Physics.Raycast (tile.transform.position, up, out objectHit, 5)) {

					if (objectHit.collider.gameObject.tag == "Enemy") {

						objectHit.collider.transform.parent.tag = "AttackableEnemy";
						objectHit.collider.gameObject.tag = "AttackableEnemy";
					}
				}
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
			}
		}

		foreach (GameObject tile in StoneTiles) {
			var path = PathFinder.FindPath (selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == true) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
			} 

			if (path.ToList ().Count <= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isEnemy == true) {
				tile.gameObject.tag = "AttackableStoneTile";
				RaycastHit objectHit;
				Vector3 up = Vector3.up;

				if (Physics.Raycast (tile.transform.position, up, out objectHit, 5)) {

					if (objectHit.collider.gameObject.tag == "Enemy") {

						objectHit.collider.transform.parent.tag = "AttackableEnemy";
						objectHit.collider.gameObject.tag = "AttackableEnemy";
					}
				}
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
			}
		}

		foreach (GameObject tile in OutpostTiles) {
			var path = PathFinder.FindPath (selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == true) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
			} 

			if (path.ToList ().Count <= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isEnemy == true) {
				tile.gameObject.tag = "AttackableOutpostTile";
				RaycastHit objectHit;
				Vector3 up = Vector3.up;

				if (Physics.Raycast (tile.transform.position, up, out objectHit, 5)) {

					if (objectHit.collider.gameObject.tag == "Enemy") {

						objectHit.collider.transform.parent.tag = "AttackableEnemy";
						objectHit.collider.gameObject.tag = "AttackableEnemy";
					}
				}
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
			}
		}

		foreach (GameObject tile in DirtTiles) {
			var path = PathFinder.FindPath (selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == true) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = highlightedMaterial;
			} 

			if (path.ToList ().Count <= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isEnemy == true) {
				tile.gameObject.tag = "AttackableDirtTile";
				RaycastHit objectHit;
				Vector3 up = Vector3.up;

				if (Physics.Raycast (tile.transform.position, up, out objectHit, 5)) {

					if (objectHit.collider.gameObject.tag == "Enemy") {

						objectHit.collider.transform.parent.tag = "AttackableEnemy";
						objectHit.collider.gameObject.tag = "AttackableEnemy";
					}
				}
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
			}
		}
	}

	public void revertbackEnemies(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AttackableEnemy");
		if(enemies !=null){
			foreach(GameObject enemy in enemies){
				enemy.tag = "Enemy";
			}
		}
		GameObject[] mudTiles = GameObject.FindGameObjectsWithTag("AttackableMudTile");
		if(mudTiles !=null){
			foreach(GameObject tile in mudTiles){
				tile.tag = "MudTile";
			}
		}
		GameObject[] stoneTiles = GameObject.FindGameObjectsWithTag("AttackableStoneTile");
		if(stoneTiles !=null){
			foreach(GameObject tile in stoneTiles){
				tile.tag = "StoneTile";
			}
		}
		GameObject[] outpostTiles = GameObject.FindGameObjectsWithTag("AttackableOutpostTile");
		if(outpostTiles !=null){
			foreach(GameObject tile in outpostTiles){
				tile.tag = "OutpostTile";
			}
		}
		GameObject[] dirtTiles = GameObject.FindGameObjectsWithTag("AttackableDirtTile");
		if(dirtTiles !=null){
			foreach(GameObject tile in dirtTiles){
				tile.tag = "DirtTile";
			}
		}
	}

	void calculateAttackIndicator(int attackRange){
		Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
		if(attackRange == 1)
			Instantiate(attackRangeIndicatorOne,rangeIndicatorPosition,Quaternion.identity);
		if(attackRange == 2)
			Instantiate(attackRangeIndicatorTwo,rangeIndicatorPosition,Quaternion.identity);
		if(attackRange == 3)
			Instantiate(attackRangeIndicatorThree,rangeIndicatorPosition,Quaternion.identity);
		if(attackRange == 4)
			Instantiate(attackRangeIndicatorFour,rangeIndicatorPosition,Quaternion.identity);
	}

	void CheckIfPassable(int movement, Tile originalTile){
		//1. find all the tiles in the scene
		GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
		GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
		GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
		GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
		
		foreach (GameObject tile in MudTiles) { 
			var path = PathFinder.FindPath (originalTile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && path.ToList ().Count >= (unitAttackRange + 1)  && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudMaterial;
			} 
			//keep the things updated out of the circle (blue material mostly)
			if (path.ToList ().Count > (movement + 1) && tile.gameObject.GetComponent<TileBehaviour>().isPassable == true){
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudMaterial;
			}
		}
		
		foreach (GameObject tile in StoneTiles) {
			var path = PathFinder.FindPath (originalTile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && path.ToList ().Count >= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneMaterial;
			} 
			//keep the things updated out of the circle (blue material mostly)
			if (path.ToList ().Count > (movement + 1)&& tile.gameObject.GetComponent<TileBehaviour>().isPassable == true){
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneMaterial;
			}
		}
		
		foreach (GameObject tile in OutpostTiles) {
			var path = PathFinder.FindPath (originalTile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			if (path.ToList ().Count <= (movement + 1) && path.ToList ().Count >= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostMaterial;
			} 
			//keep the things updated out of the circle (blue material mostly)
			if (path.ToList ().Count > (movement + 1)&& tile.gameObject.GetComponent<TileBehaviour>().isPassable == true){
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostMaterial;
			}
		}
		
		foreach (GameObject tile in DirtTiles) {
			var path = PathFinder.FindPath (originalTile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
			//keep the things updated in the circle 
			if (path.ToList ().Count <= (movement + 1) && path.ToList ().Count >= (unitAttackRange + 1) && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtMaterial;
			} 

			//keep the things updated out of the circle (blue material mostly)
			if (path.ToList ().Count > (movement + 1)&& tile.gameObject.GetComponent<TileBehaviour>().isPassable == true){
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtMaterial;
			}
		}
	}


}