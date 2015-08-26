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
	public GameObject attackRangeIndicatorFive;
	public GameObject attackRangeIndicatorSix;
	public GameObject attackRangeIndicatorSeven;
	public bool canMove,firingGrenades,firingFire;
	public Grenades isGrenades;
	public HoverGUI hover;
	public PlayFootAnimation animation;

	// Use this for initialization
	void Start ()
	{
		PlayerCam = Camera.main.GetComponent<Camera>(); // Find the Camera's GameObject from its tag
		gameManager = Camera.main.GetComponent<GameMaster> ();
		gridManager = Camera.main.GetComponent<GridManager> ();
		isGrenades = GameObject.Find ("Grenades").GetComponent<Grenades> ();
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

		if (selectedCharacter != null) {
			if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
				if(selectedCharacter.GetComponent<Footsoldier>().curr_Health <= 0){
					revertbackEnemies();
					highlightingTiles = false;
					Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
					gameManager.gameState = 0;
				}
			}
			if(selectedCharacter.gameObject.tag == "SelectdRangedUnit"){
				if(selectedCharacter.GetComponent<Rangedsoldier>().curr_Health <= 0){
					revertbackEnemies();
					highlightingTiles = false;
					Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
					gameManager.gameState = 0;
				}
			}
			if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
				if(selectedCharacter.GetComponent<Musket>().curr_Health <= 0){
					revertbackEnemies();
					highlightingTiles = false;
					Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
					gameManager.gameState = 0;
				}
			}
			if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
				if(selectedCharacter.GetComponent<Pike>().curr_Health <= 0){
					revertbackEnemies();
					highlightingTiles = false;
					Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
					gameManager.gameState = 0;
				}
			}
		}
		
		
		//select to move 
		if (gameManager.gameState == 0) {
			if (Input.GetMouseButtonDown (0)) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.gameObject.tag == "FootUnit") {
						hover.isOnFootUnit = false;
						hover.isOnRangedUnit = false;
						hover.isOnSiegeUnit = false;
						hover.isOnPike = false;
						hover.isOnMusket = false;
						canMove = hit.collider.gameObject.GetComponent<Footsoldier>().canMove;
						if(canMove){
							unitAttack = hit.collider.gameObject.GetComponent<Footsoldier>().Attack;
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedFootUnit";
							selectedCharacter.GetComponent<disablinghp>().Appear = true;
							unitMovement = hit.collider.gameObject.GetComponent<Footsoldier>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Footsoldier>().AttackRange;
							hit.collider.gameObject.GetComponent<Footsoldier>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					} if (hit.collider.gameObject.tag == "RangedUnit") {
						hover.isOnFootUnit = false;
						hover.isOnRangedUnit = false;
						hover.isOnSiegeUnit = false;
						hover.isOnPike = false;
						hover.isOnMusket = false;
						canMove = hit.collider.gameObject.GetComponent<Rangedsoldier>().canMove;
						if(canMove){
							unitAttack = hit.collider.gameObject.GetComponent<Rangedsoldier>().Attack;
							if(isGrenades.isGrenades == true)
							{
								firingGrenades = true;
								unitAttack = 50;
							}
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedRangedUnit";
							selectedCharacter.GetComponent<disablinghp>().Appear = true;
							unitMovement = hit.collider.gameObject.GetComponent<Rangedsoldier>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Rangedsoldier>().AttackRange;
							hit.collider.gameObject.GetComponent<Rangedsoldier>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					} if (hit.collider.gameObject.tag == "SiegeUnit") {
						hover.isOnFootUnit = false;
						hover.isOnRangedUnit = false;
						hover.isOnPike = false;
						hover.isOnMusket = false;
						hover.isOnSiegeUnit = false;
						canMove = hit.collider.gameObject.GetComponent<Musket>().canMove;
						if(canMove){
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedSiegeUnit";
							selectedCharacter.GetComponent<disablinghp>().Appear = true;
							unitMovement = hit.collider.gameObject.GetComponent<Musket>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Musket>().AttackRange;
							unitAttack = hit.collider.gameObject.GetComponent<Musket>().Attack;
							hit.collider.gameObject.GetComponent<Musket>().isSelected = true;
							highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
							Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
							calculateAttackIndicator(unitAttackRange);
						}
					} if(hit.collider.gameObject.tag == "PikeUnit"){
						hover.isOnFootUnit = false;
						hover.isOnRangedUnit = false;
						hover.isOnPike = false;
						hover.isOnMusket = false;
						hover.isOnSiegeUnit = false;
						canMove = hit.collider.gameObject.GetComponent<Pike>().canMove;
						if(canMove){
							selectedCharacter = hit.collider.gameObject;
							gridManager.selectedCharacter = hit.collider.gameObject;
							gameManager.gameState = 1;
							selectedCharacter.gameObject.tag = "SelectedPikeUnit";
							selectedCharacter.GetComponent<disablinghp>().Appear = true;
							unitMovement = hit.collider.gameObject.GetComponent<Pike>().Movement;
							unitAttackRange = hit.collider.gameObject.GetComponent<Pike>().AttackRange;
							unitAttack = hit.collider.gameObject.GetComponent<Pike>().Attack;
							hit.collider.gameObject.GetComponent<Pike>().isSelected = true;
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
									
									movement.y = 0.2f;
								}
							    if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){

									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = (5);
									
									movement.y = 0.2f;
								}
							    if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									
									selectedCharacter.GetComponent<Musket> ().AttackRange = (5);

									movement.y = 0.2f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
								
									movement.y = 0.2f;
								}
							}
							if(_hitInfo.collider.gameObject.tag == "OutpostTile"){
								economy.outpost += 1;
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									
									selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
									
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
									
									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = (4);
									
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									
								selectedCharacter.GetComponent<Musket> ().AttackRange = (4);
									
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.GetComponent<Pike> ().AttackRange = 2;
									movement.y = 0.1f;
								}
							}
							if(_hitInfo.collider.gameObject.tag == "MudTile" ){
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									
									selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
									
									selectedCharacter.GetComponent<Footsoldier> ().isMud = true;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
									
									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 4;
									
									selectedCharacter.GetComponent<Rangedsoldier> ().isMud = true;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									
									selectedCharacter.GetComponent<Musket> ().AttackRange = 4;
									
									selectedCharacter.GetComponent<Musket> ().isMud = true;
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.GetComponent<Pike> ().AttackRange = 2;
									selectedCharacter.GetComponent<Pike> ().isMud = true;
									movement.y = 0.1f;
								}

							}
							if (_hitInfo.collider.gameObject.tag == "DirtTile") {
								if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
									selectedCharacter.GetComponent<Footsoldier> ().AttackRange = 1;
									
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
									selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange = 4;
									
									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
									selectedCharacter.GetComponent<Musket> ().AttackRange = 4;

									movement.y = 0.1f;
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.GetComponent<Pike> ().AttackRange = 2;
									movement.y = 0.1f;
								}

							}
							selectedCharacter.GetComponent<CharacterMovement> ().destinationTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour> ();

							var path = PathFinder.FindPath (selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile, selectedCharacter.GetComponent<CharacterMovement> ().destinationTile.tile);

							//move if in the allowed distance 

							if (path.ToList ().Count <= (unitMovement + 1) && selectedCharacter.GetComponent<CharacterMovement> ().destinationTile.isPassable == true) {
								selectedCharacter.transform.position = movement;
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								selectedCharacter.gameObject.transform.rotation = Quaternion.identity;
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
									selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
									selectedCharacter.gameObject.GetComponent<Musket> ().DecreaseCooldown();
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.gameObject.GetComponent<Pike>().isSelected = false;
									selectedCharacter.gameObject.GetComponent<Pike> ().DecreaseCooldown();
									selectedCharacter.gameObject.tag = "PikeUnit";
								}
								revertbackEnemies();
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								highlightingTiles = false;
								gameManager.gameState = 0;
							}
						else{
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
								selectedCharacter.gameObject.GetComponent<Pike>().isSelected = false;
								selectedCharacter.gameObject.tag = "PikeUnit";
							}
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							gameManager.gameState = 0;
						}
						

					}else if(_hitInfo.collider.gameObject.tag == "AttackableEnemy" || _hitInfo.collider.gameObject.tag == "AttackableRangedEnemy" || _hitInfo.collider.gameObject.tag == "AttackableEnemySiege"){
						if(_hitInfo.collider.gameObject.tag == "AttackableEnemy" ){
							if(firingGrenades){
								TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<CharacterMovement>().unitOriginalTile.GetComponent<TileBehaviour>();
								//do damage
								//_hitInfo.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								//get his tile and then spread to the ones near him 

								GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
								GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
								GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
								GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
								GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
								GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
								GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
								GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			

								foreach (GameObject tile in MudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in StoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in OutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in DirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}

								foreach (GameObject tile in AttackableMudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableStoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableOutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableDirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								firingGrenades = false;
								isGrenades.isGrenades = false;
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								
							//	selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().Target = _hitInfo.collider.transform;
							//	StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().SimulateProjectile());
								StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								_hitInfo.collider.gameObject.GetComponent<Enemy>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.transform.parent.tag = "Enemy";
								_hitInfo.collider.gameObject.tag = "Enemy";
								_hitInfo.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
							if(!firingGrenades){
								//do the damage
							//	_hitInfo.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								if(selectedCharacter.tag == "SelectedFootUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayFootAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedPikeUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayPikeAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedRangedUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().SimulateProjectile());
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedSiegeUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().SimulateProjectile());
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayMusketAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								_hitInfo.collider.gameObject.GetComponent<Enemy>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.transform.parent.tag = "Enemy";
								_hitInfo.collider.gameObject.tag = "Enemy";
								_hitInfo.collider.gameObject.GetComponent<Enemy>().CheckDeath();
							}
						}
						if(_hitInfo.collider.gameObject.tag == "AttackableRangedEnemy"){

							if(firingGrenades){
								TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<CharacterMovement>().unitOriginalTile.GetComponent<TileBehaviour>();
								//do damage
						//		_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().DealtDamage(unitAttack);
								//get his tile and then spread to the ones near him 
								
								GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
								GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
								GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
								GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
								GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
								GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
								GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
								GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
								
								foreach (GameObject tile in MudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in StoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in OutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in DirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								
								foreach (GameObject tile in AttackableMudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableStoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableOutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableDirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}

								firingGrenades = false;
								isGrenades.isGrenades = false;
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.transform.parent.tag = "RangedEnemy";
								_hitInfo.collider.gameObject.tag = "RangedEnemy";
								_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().CheckDeath();
							}

							if(!firingGrenades){
								//do the damage
							//	_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().DealtDamage(unitAttack);
							
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								if(selectedCharacter.tag == "SelectedFootUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayFootAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedPikeUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayPikeAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedRangedUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().SimulateProjectile());
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedSiegeUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().SimulateProjectile());

									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayMusketAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.transform.parent.tag = "RangedEnemy";
								_hitInfo.collider.gameObject.tag = "RangedEnemy";
								_hitInfo.collider.gameObject.GetComponent<EnemyRanged>().CheckDeath();
							}
						}
						if(_hitInfo.collider.gameObject.tag == "AttackableEnemySiege" ){
							if(firingGrenades){
								TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<CharacterMovement>().unitOriginalTile.GetComponent<TileBehaviour>();
								//do damage
								//			_hitInfo.collider.gameObject.GetComponent<Enemy>().DealtDamage(unitAttack);
								//get his tile and then spread to the ones near him 
								
								GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
								GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
								GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
								GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
								GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
								GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
								GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
								GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
								
								foreach (GameObject tile in MudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in StoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in OutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in DirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								
								foreach (GameObject tile in AttackableMudTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableStoneTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableOutpostTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								foreach (GameObject tile in AttackableDirtTiles) {
									Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
									if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
										//do the damage from tile up
										DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
									}
								}
								firingGrenades = false;
								isGrenades.isGrenades = false;
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								_hitInfo.collider.gameObject.GetComponent<EnemyCannon>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.gameObject.tag = "EnemySiege";
								_hitInfo.collider.gameObject.GetComponent<EnemyCannon>().CheckDeath();
							}
							if(!firingGrenades){
								//do the damage
							//	_hitInfo.collider.gameObject.GetComponent<EnemyCannon>().DealtDamage(unitAttack);
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
								if(selectedCharacter.tag == "SelectedFootUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayFootAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedPikeUnit"){
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayPikeAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedRangedUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationArrows>().SimulateProjectile());
									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayRangedAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								if(selectedCharacter.tag == "SelectedSiegeUnit"){
									selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().Target = _hitInfo.collider.transform;
									StartCoroutine(selectedCharacter.gameObject.GetComponent<ThrowSimulationBullets>().SimulateProjectile());

									StartCoroutine(selectedCharacter.gameObject.GetComponentInChildren<PlayMusketAnimation>().WaitForAnimation(_hitInfo.collider.gameObject));
								}
								_hitInfo.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								_hitInfo.collider.gameObject.GetComponent<EnemyCannon>().isBeingAttacked = true;
								//revert back
								_hitInfo.collider.gameObject.tag = "EnemySiege";
								_hitInfo.collider.gameObject.GetComponent<EnemyCannon>().CheckDeath();
							}
						}

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
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit" || selectedCharacter.gameObject.tag == "FootUnit"){
						
							//look at the enemy
							selectedCharacter.GetComponent<disablinghp>().Appear = false;

							selectedCharacter.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(_hitInfo.collider.gameObject.transform.position.x, selectedCharacter.gameObject.transform.position.y, _hitInfo.collider.gameObject.transform.position.z) - selectedCharacter.gameObject.transform.position);
							//animation is for attack
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().attack = true;
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "FootUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedRangedUnit" || selectedCharacter.gameObject.tag == "RangedUnit"){
					
							selectedCharacter.GetComponent<disablinghp>().Appear = false;
							selectedCharacter.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(_hitInfo.collider.gameObject.transform.position.x, selectedCharacter.gameObject.transform.position.y, _hitInfo.collider.gameObject.transform.position.z) - selectedCharacter.gameObject.transform.position);
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().attack = true;
							selectedCharacter.gameObject.GetComponent<Rangedsoldier>().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "RangedUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit" || selectedCharacter.gameObject.tag == "SiegeUnit"){
						
							selectedCharacter.GetComponent<disablinghp>().Appear = false;
							selectedCharacter.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(_hitInfo.collider.gameObject.transform.position.x, selectedCharacter.gameObject.transform.position.y, _hitInfo.collider.gameObject.transform.position.z) - selectedCharacter.gameObject.transform.position);
							selectedCharacter.gameObject.GetComponent<Musket> ().attack = true;
							selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Musket> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedPikeUnit" || selectedCharacter.gameObject.tag == "PikeUnit"){
						
							selectedCharacter.GetComponent<disablinghp>().Appear = false;
							selectedCharacter.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(_hitInfo.collider.gameObject.transform.position.x, selectedCharacter.gameObject.transform.position.y, _hitInfo.collider.gameObject.transform.position.z) - selectedCharacter.gameObject.transform.position);
							selectedCharacter.gameObject.GetComponent<Pike> ().attack = true;
							selectedCharacter.gameObject.GetComponent<Pike>().isSelected = false;
							selectedCharacter.gameObject.GetComponent<Pike> ().DecreaseCooldown();
							selectedCharacter.gameObject.tag = "PikeUnit";
						}


						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						gameManager.gameState = 0;
					}
				/*	else if(_hitInfo.collider.gameObject.tag == "AttackableDirtTile"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
						if(firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
							//get his tile and then spread to the ones near him 
							
							GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
							GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
							GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
							GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
							GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
							GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
							GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
							GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
							
							foreach (GameObject tile in MudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in StoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in OutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in DirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							
							foreach (GameObject tile in AttackableMudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableStoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableOutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableDirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							firingGrenades = false;
							isGrenades.isGrenades = false;
							_hitInfo.collider.gameObject.tag = "DirtTile";
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
						if(!firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							_hitInfo.collider.gameObject.tag = "DirtTile";
							if(selectedCharacter.tag == "SelectedFootUnit"){
								selectedCharacter.gameObject.GetComponent<Footsoldier>().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedRangedUnit"){
								selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedSiegeUnit"){
								selectedCharacter.gameObject.GetComponent<Musket>().DecreaseCooldown();
							}
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;

						}
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableMudTile"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
						if(firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
							//get his tile and then spread to the ones near him 
							
							GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
							GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
							GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
							GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
							GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
							GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
							GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
							GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
							
							foreach (GameObject tile in MudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in StoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in OutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in DirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							
							foreach (GameObject tile in AttackableMudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableStoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableOutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableDirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}

							firingGrenades = false;
							isGrenades.isGrenades = false;
							_hitInfo.collider.gameObject.tag = "MudTile";
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
						if(!firingGrenades){
							//do damage
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							_hitInfo.collider.gameObject.tag = "MudTile";
							if(selectedCharacter.tag == "SelectedFootUnit"){
								selectedCharacter.gameObject.GetComponent<Footsoldier>().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedRangedUnit"){
								selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedSiegeUnit"){
								selectedCharacter.gameObject.GetComponent<Musket>().DecreaseCooldown();
							}
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableStoneTile"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
						if(firingGrenades){
							//do damage
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
							//get his tile and then spread to the ones near him 
							
							GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
							GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
							GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
							GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
							GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
							GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
							GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
							GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
							
							foreach (GameObject tile in MudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in StoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in OutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in DirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							
							foreach (GameObject tile in AttackableMudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableStoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableOutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableDirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}

							firingGrenades = false;
							isGrenades.isGrenades = false;
							_hitInfo.collider.gameObject.tag = "StoneTile";
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
						if(!firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							_hitInfo.collider.gameObject.tag = "StoneTile";
							if(selectedCharacter.tag == "SelectedFootUnit"){
								selectedCharacter.gameObject.GetComponent<Footsoldier>().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedRangedUnit"){
								selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedSiegeUnit"){
								selectedCharacter.gameObject.GetComponent<Musket>().DecreaseCooldown();
							}
							revertbackEnemies();

							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
					}
					else if(_hitInfo.collider.gameObject.tag == "AttackableOutpostTile"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
						if(firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
							//get his tile and then spread to the ones near him 

							GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
							GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
							GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
							GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");
							GameObject[] AttackableMudTiles = GameObject.FindGameObjectsWithTag ("AttackableMudTile");
							GameObject[] AttackableStoneTiles = GameObject.FindGameObjectsWithTag ("AttackableStoneTile");
							GameObject[] AttackableOutpostTiles = GameObject.FindGameObjectsWithTag ("AttackableOutpostTile");
							GameObject[] AttackableDirtTiles = GameObject.FindGameObjectsWithTag ("AttackableDirtTile");			
							
							foreach (GameObject tile in MudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in StoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in OutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in DirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							
							foreach (GameObject tile in AttackableMudTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableStoneTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableOutpostTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
								}
							}
							foreach (GameObject tile in AttackableDirtTiles) {
								Vector2 tilePosition = gridManager.calcGridPos (tile.transform.position);
								if (PathFinder.FindPath (mainEnemyTile.tile, tile.gameObject.GetComponent<TileBehaviour> ().tile).ToList ().Count <= (2) && tile.gameObject.GetComponent<TileBehaviour>().isEnemy == true) {
									//do the damage from tile up
									DoDamageTileUpGrenades(tile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}

							firingGrenades = false;
							isGrenades.isGrenades = false;
							_hitInfo.collider.gameObject.tag = "OutpostTile";
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							revertbackEnemies();
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
						if(!firingGrenades){
							TileBehaviour mainEnemyTile = _hitInfo.collider.gameObject.GetComponent<TileBehaviour>();
							//do damage
							DoDamageTileUp(mainEnemyTile.gameObject.GetComponent<TileBehaviour>());
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
								selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
								selectedCharacter.gameObject.tag = "SiegeUnit";
							}
							_hitInfo.collider.gameObject.tag = "OutpostTile";
							if(selectedCharacter.tag == "SelectedFootUnit"){
								selectedCharacter.gameObject.GetComponent<Footsoldier>().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedRangedUnit"){
								selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().DecreaseCooldown();
							}
							if(selectedCharacter.tag == "SelectedSiegeUnit"){
								selectedCharacter.gameObject.GetComponent<Musket>().DecreaseCooldown();
							}
							revertbackEnemies();
							
							Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
							highlightingTiles = false;
							gameManager.gameState = 0;
						}
					}*/
					else if (_hitInfo.collider.gameObject.tag == "FootUnit" || _hitInfo.collider.gameObject.tag == "RangedUnit" || _hitInfo.collider.gameObject.tag == "SiegeUnit" || _hitInfo.collider.gameObject.tag == "PikeUnit"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
						revertbackEnemies();
						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						highlightingTiles = false;
						if(selectedCharacter.gameObject.tag == "SelectedFootUnit"){
							selectedCharacter.gameObject.GetComponent<Footsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "FootUnit";
						}if(selectedCharacter.gameObject.tag == "SelectedRangedUnit"){
							selectedCharacter.gameObject.GetComponent<Rangedsoldier> ().isSelected = false;
							selectedCharacter.gameObject.tag = "RangedUnit";
						}if(selectedCharacter.gameObject.tag == "SelectedSiegeUnit"){
							selectedCharacter.gameObject.GetComponent<Musket> ().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
							selectedCharacter.gameObject.GetComponent<Pike> ().isSelected = false;
							selectedCharacter.gameObject.tag = "PikeUnit";
						}if (_hitInfo.collider.gameObject.tag == "FootUnit") {
							canMove = _hitInfo.collider.gameObject.GetComponent<Footsoldier>().canMove;
							if(canMove){
								selectedCharacter = _hitInfo.collider.gameObject;
								gridManager.selectedCharacter = _hitInfo.collider.gameObject;
								gameManager.gameState = 1;
								selectedCharacter.gameObject.tag = "SelectedFootUnit";
								selectedCharacter.GetComponent<disablinghp>().Appear = true;
								unitMovement = _hitInfo.collider.gameObject.GetComponent<Footsoldier>().Movement;
								unitAttackRange = _hitInfo.collider.gameObject.GetComponent<Footsoldier>().AttackRange;
								unitAttack = _hitInfo.collider.gameObject.GetComponent<Footsoldier>().Attack;
								_hitInfo.collider.gameObject.GetComponent<Footsoldier>().isSelected = true;
								highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
								Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
								calculateAttackIndicator(unitAttackRange);
							}
							else{
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
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
									selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.gameObject.GetComponent<Pike> ().isSelected = false;
									selectedCharacter.gameObject.tag = "PikeUnit";
								}
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								gameManager.gameState = 0;
							}
						} if (_hitInfo.collider.gameObject.tag == "RangedUnit") {
							canMove = _hitInfo.collider.gameObject.GetComponent<Rangedsoldier>().canMove;
							if(canMove){
								selectedCharacter = _hitInfo.collider.gameObject;
								gridManager.selectedCharacter = _hitInfo.collider.gameObject;
								gameManager.gameState = 1;
								selectedCharacter.gameObject.tag = "SelectedRangedUnit";
								selectedCharacter.GetComponent<disablinghp>().Appear = true;
								unitMovement = _hitInfo.collider.gameObject.GetComponent<Rangedsoldier>().Movement;
								unitAttackRange = _hitInfo.collider.gameObject.GetComponent<Rangedsoldier>().AttackRange;
								unitAttack = _hitInfo.collider.gameObject.GetComponent<Rangedsoldier>().Attack;
								_hitInfo.collider.gameObject.GetComponent<Rangedsoldier>().isSelected = true;
								highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
								Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
								calculateAttackIndicator(unitAttackRange);
							}
							else{
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
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
									selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.gameObject.GetComponent<Pike> ().isSelected = false;
									selectedCharacter.gameObject.tag = "PikeUnit";
								}
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								gameManager.gameState = 0;
							}
						} if (_hitInfo.collider.gameObject.tag == "SiegeUnit") {
							canMove = _hitInfo.collider.gameObject.GetComponent<Musket>().canMove;
							if(canMove){
								selectedCharacter = _hitInfo.collider.gameObject;
								gridManager.selectedCharacter = _hitInfo.collider.gameObject;
								gameManager.gameState = 1;
								selectedCharacter.gameObject.tag = "SelectedSiegeUnit";
								selectedCharacter.GetComponent<disablinghp>().Appear = true;
								unitMovement = _hitInfo.collider.gameObject.GetComponent<Musket>().Movement;
								unitAttackRange = _hitInfo.collider.gameObject.GetComponent<Musket>().AttackRange;
								unitAttack = _hitInfo.collider.gameObject.GetComponent<Musket>().Attack;
								_hitInfo.collider.gameObject.GetComponent<Musket>().isSelected = true;
								highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
								Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
								calculateAttackIndicator(unitAttackRange);
							}
							else{
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
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
									selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.gameObject.GetComponent<Pike> ().isSelected = false;
									selectedCharacter.gameObject.tag = "PikeUnit";
								}
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								gameManager.gameState = 0;
							}
						}if (_hitInfo.collider.gameObject.tag == "PikeUnit") {
							canMove = _hitInfo.collider.gameObject.GetComponent<Pike>().canMove;
							if(canMove){
								selectedCharacter = _hitInfo.collider.gameObject;
								gridManager.selectedCharacter = _hitInfo.collider.gameObject;
								gameManager.gameState = 1;
								selectedCharacter.gameObject.tag = "SelectedPikeUnit";
								selectedCharacter.GetComponent<disablinghp>().Appear = true;
								unitMovement = _hitInfo.collider.gameObject.GetComponent<Pike>().Movement;
								unitAttackRange = _hitInfo.collider.gameObject.GetComponent<Pike>().AttackRange;
								unitAttack = _hitInfo.collider.gameObject.GetComponent<Pike>().Attack;
								_hitInfo.collider.gameObject.GetComponent<Pike>().isSelected = true;
								highlightAvailableTiles (unitMovement, selectedCharacter.gameObject.GetComponent<CharacterMovement> ().unitOriginalTile.tile);
								Vector3 rangeIndicatorPosition = new Vector3(selectedCharacter.transform.position.x,0.078f,selectedCharacter.transform.position.z);
								calculateAttackIndicator(unitAttackRange);
							}
							else{
								selectedCharacter.GetComponent<disablinghp>().Appear = false;
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
									selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
									selectedCharacter.gameObject.tag = "SiegeUnit";
								}
								if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
									selectedCharacter.gameObject.GetComponent<Pike> ().isSelected = false;
									selectedCharacter.gameObject.tag = "PikeUnit";
								}
								Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
								gameManager.gameState = 0;
							}
						}
						else {
							return;
						}
					}

					else if (_hitInfo.collider.gameObject.tag == "SelectedFootUnit" || _hitInfo.collider.gameObject.tag == "SelectedRangedUnit" || _hitInfo.collider.gameObject.tag == "SelectedSiegeUnit" || _hitInfo.collider.gameObject.tag == "SelectedPikeUnit" || _hitInfo.collider.gameObject.tag == "DirtTile" ||  _hitInfo.collider.gameObject.tag == "MudTile" ||  _hitInfo.collider.gameObject.tag == "OutpostTile" ||  _hitInfo.collider.gameObject.tag == "StoneTile" ||  _hitInfo.collider.gameObject.tag == "Enemy" ||  _hitInfo.collider.gameObject.tag == "RangedEnemy"){
						selectedCharacter.GetComponent<disablinghp>().Appear = false;
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
							selectedCharacter.gameObject.GetComponent<Musket>().isSelected = false;
							selectedCharacter.gameObject.tag = "SiegeUnit";
						}
						if(selectedCharacter.gameObject.tag == "SelectedPikeUnit"){
							selectedCharacter.gameObject.GetComponent<Pike>().isSelected = false;
							selectedCharacter.gameObject.tag = "PikeUnit";
						}


						Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
						gameManager.gameState = 0;
					}
				}
			}
		}else if (gameManager.gameState == 3) {
			GameObject selectedFoot = GameObject.FindGameObjectWithTag("SelectedFootUnit");
			GameObject selectedRanged = GameObject.FindGameObjectWithTag("SelectedRangedUnit");
			GameObject selectedSiege = GameObject.FindGameObjectWithTag("SelectedSiegeUnit");
			GameObject selectedPike = GameObject.FindGameObjectWithTag("SelectedPikeUnit");
			if(selectedFoot != null){
				selectedFoot.gameObject.tag = "FootUnit";
				selectedFoot.GetComponent<disablinghp>().Appear = false;
				Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
			}
			if(selectedRanged!= null){
				selectedRanged.gameObject.tag = "RangedUnit";
				selectedRanged.GetComponent<disablinghp>().Appear = false;
				Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
			}
			if(selectedSiege!= null){
				selectedSiege.gameObject.tag = "SiegeUnit";
				selectedSiege.GetComponent<disablinghp>().Appear = false;
				Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
			}
			if(selectedPike!= null){
				selectedSiege.gameObject.tag = "PikeUnit";
				selectedSiege.GetComponent<disablinghp>().Appear = false;
				Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
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

					if (objectHit.collider.gameObject.tag == "RangedEnemy") {
						objectHit.collider.transform.parent.tag = "AttackableRangedEnemy";
						objectHit.collider.gameObject.tag = "AttackableRangedEnemy";
					}
					if (objectHit.collider.gameObject.tag == "EnemySiege") {
						objectHit.collider.gameObject.tag = "AttackableEnemySiege";
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
					if (objectHit.collider.gameObject.tag == "RangedEnemy") {
						objectHit.collider.transform.parent.tag = "AttackableRangedEnemy";
						objectHit.collider.gameObject.tag = "AttackableRangedEnemy";
					}
					if (objectHit.collider.gameObject.tag == "EnemySiege") {
						objectHit.collider.gameObject.tag = "AttackableEnemySiege";
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
					if (objectHit.collider.gameObject.tag == "RangedEnemy") {
						objectHit.collider.transform.parent.tag = "AttackableRangedEnemy";
						objectHit.collider.gameObject.tag = "AttackableRangedEnemy";
					}
					if (objectHit.collider.gameObject.tag == "EnemySiege") {
						objectHit.collider.gameObject.tag = "AttackableEnemySiege";
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
					if (objectHit.collider.gameObject.tag == "RangedEnemy") {
						objectHit.collider.transform.parent.tag = "AttackableRangedEnemy";
						objectHit.collider.gameObject.tag = "AttackableRangedEnemy";
					}
					if (objectHit.collider.gameObject.tag == "EnemySiege") {
						objectHit.collider.gameObject.tag = "AttackableEnemySiege";
					}

				}
				Transform mychildtransform = tile.transform.FindChild ("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
			}
		}
	}

	public void revertbackEnemies(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("AttackableEnemy");
		GameObject[] rangedEnemies = GameObject.FindGameObjectsWithTag("AttackableRangedEnemy");
		GameObject[] siegeEnemies = GameObject.FindGameObjectsWithTag ("AttackableEnemySiege");
		if(enemies !=null){
			foreach(GameObject enemy in enemies){
				enemy.tag = "Enemy";
			}
		}
		if(rangedEnemies !=null){
			foreach(GameObject enemy in rangedEnemies){
				enemy.tag = "RangedEnemy";
			}
		}

		if (siegeEnemies != null) {
			foreach(GameObject enemy in siegeEnemies){
				enemy.tag = "EnemySiege";
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
		if(attackRange == 5)
			Instantiate(attackRangeIndicatorFive,rangeIndicatorPosition,Quaternion.identity);
		if(attackRange == 6)
			Instantiate(attackRangeIndicatorSix,rangeIndicatorPosition,Quaternion.identity);
		if(attackRange == 7)
			Instantiate(attackRangeIndicatorSeven,rangeIndicatorPosition,Quaternion.identity);
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

	public void DoDamageTileUp(TileBehaviour tile){
		Debug.Log (tile.gameObject.name);
		Ray _ray;
		RaycastHit _hitInfo;
		RaycastHit objectHit;
		Vector3 up = Vector3.up;
		selectedCharacter.GetComponent<disablinghp> ().Appear = false;
		// Raycast and verify that it collided
			if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
				if (objectHit.collider.gameObject.tag == "AttackableEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<Enemy> ().DealtDamage (unitAttack);
					objectHit.collider.transform.parent.tag = "Enemy";
					objectHit.collider.gameObject.tag = "Enemy";
					objectHit.collider.gameObject.GetComponent<Enemy> ().CheckDeath ();
				}
			
				if (objectHit.collider.gameObject.tag == "AttackableRangedEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().DealtDamage (unitAttack);
					objectHit.collider.transform.parent.tag = "RangedEnemy";
					objectHit.collider.gameObject.tag = "RangedEnemy";
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().CheckDeath ();
				}

				if (objectHit.collider.gameObject.tag == "AttackableEnemySiege") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyCannon> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.tag = "EnemySiege";
					objectHit.collider.gameObject.GetComponent<EnemyCannon> ().CheckDeath ();
				}

				if (objectHit.collider.gameObject.tag == "Enemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<Enemy> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.GetComponent<Enemy> ().CheckDeath ();
				}
				
				if (objectHit.collider.gameObject.tag == "RangedEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().CheckDeath ();
				}

				if (objectHit.collider.gameObject.tag == "EnemySiege") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyCannon> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.GetComponent<EnemyCannon> ().CheckDeath ();
				}
			}

	}

	public void DoDamageTileUpGrenades(TileBehaviour tile){
		Debug.Log (tile.gameObject.name);
		Ray _ray;
		RaycastHit _hitInfo;
		RaycastHit objectHit;
		Vector3 up = Vector3.up;
		selectedCharacter.GetComponent<disablinghp> ().Appear = false;

			if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
				if (objectHit.collider.gameObject.tag == "Enemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<Enemy> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.GetComponent<Enemy> ().CheckDeath ();
				}
				
				if (objectHit.collider.gameObject.tag == "RangedEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().DealtDamage (unitAttack);
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().CheckDeath ();
				}

				if (objectHit.collider.gameObject.tag == "AttackableEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<Enemy> ().DealtDamage (unitAttack);
					objectHit.collider.transform.parent.tag = "Enemy";
					objectHit.collider.gameObject.tag = "Enemy";
					objectHit.collider.gameObject.GetComponent<Enemy> ().CheckDeath ();
				}
				
				if (objectHit.collider.gameObject.tag == "AttackableRangedEnemy") {
					objectHit.collider.gameObject.GetComponent<disablinghp> ().JustHit = true;
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().DealtDamage (unitAttack);
					objectHit.collider.transform.parent.tag = "RangedEnemy";
					objectHit.collider.gameObject.tag = "RangedEnemy";
					objectHit.collider.gameObject.GetComponent<EnemyRanged> ().CheckDeath ();
				}
			}

	}
}