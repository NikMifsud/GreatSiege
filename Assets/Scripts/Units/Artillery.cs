using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;


public class Artillery : MonoBehaviour {
	public float curr_Health;
	public float max_Health;
	public int Armor;
	public int Movement;
	public int AttackRange;
	public AudioClip cannonShotEffect;
	public int Attack;
	public int DamageTaken;
	private AudioSource source;
	public float Movementtime;
	public bool canMove;
	public GameObject healthBar;
	public bool isSelected;
	public bool isMud;
	Collider[] colliders;
	public economy economy;
	public bool enemyHit;
	
	// Use this for initialization
	void Start () {
		enemyHit = false;
		max_Health = 250;
		curr_Health = 250;
		Armor = 20;
		Movement = 1;
		Attack = 80;
		source = Camera.main.GetComponent<AudioSource> ();
		isSelected = false;
		isMud = false;
		canMove = true;
		Movementtime = 30;
		economy = GameObject.FindGameObjectWithTag ("Economy").GetComponent<economy>();
	}

	public void DecreaseCooldown(){
		canMove = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (curr_Health > 250) {
			curr_Health = 250;
		}

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
		if (DamageTaken <= 19) {
			DamageTaken = 0;
		}
		if (curr_Health <= 0) {
			Destroy (this.gameObject);
		}
		if (DamageTaken >= 1) {
			curr_Health = curr_Health + (Armor - DamageTaken);
			DamageTaken = 0;
		}
		float calc_Health = curr_Health / max_Health;
		SetHealthBar (calc_Health);

		if (canMove) {
			colliders = Physics.OverlapSphere (this.transform.position, 4f);
			int randomIndex = UnityEngine.Random.Range (0, colliders.Count());
			colliders.ToArray();
		//	int i = Random.Range(0,(colliders.Count));
				//random selection of enemy 
				GameObject enemy = colliders[randomIndex].gameObject;
				if ((enemy.gameObject.tag == "Enemy" || enemy.gameObject.tag == "AttackableEnemy") && enemyHit == false) {
				gameObject.transform.GetComponentInParent<ThrowSimulation>().Target = enemy.gameObject.transform;
					source.PlayOneShot (cannonShotEffect, 1.0f);
					enemy.GetComponent<disablinghp> ().JustHit = true;
					enemy.GetComponent<Enemy> ().DealtDamage (Attack);
					enemy.GetComponent<Enemy> ().CheckDeath ();
					enemyHit = true;
					DecreaseCooldown ();
				}
				if ((enemy.gameObject.tag == "RangedEnemy" || enemy.gameObject.tag == "AttackableRangedEnemy") && enemyHit == false) {
					gameObject.transform.GetComponentInParent<ThrowSimulation>().Target = enemy.gameObject.transform;
					source.PlayOneShot (cannonShotEffect, 1.0f);
					enemy.GetComponent<disablinghp> ().JustHit = true;
					enemy.GetComponent<EnemyRanged> ().DealtDamage (Attack);
					enemy.GetComponent<EnemyRanged> ().CheckDeath ();
					enemyHit = true;
					DecreaseCooldown ();
				}
				if ((enemy.gameObject.tag == "EnemySiege" || enemy.gameObject.tag == "AttackableEnemySiege") && enemyHit == false) {
				gameObject.transform.GetComponentInParent<ThrowSimulation>().Target = enemy.gameObject.transform;
					source.PlayOneShot (cannonShotEffect, 1.0f);
					enemy.GetComponent<disablinghp> ().JustHit = true;
					enemy.GetComponent<EnemyCannon> ().DealtDamage (Attack);
					enemy.GetComponent<EnemyCannon> ().CheckDeath ();
					enemyHit = true;
					DecreaseCooldown ();
				}
		}

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
				if (objectHit.collider.gameObject.tag == "DirtTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else if (objectHit.collider.gameObject.tag == "StoneTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else 	if (objectHit.collider.gameObject.tag == "MudTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
				} else 	if (objectHit.collider.gameObject.tag == "OutpostTile") {
					objectHit.collider.gameObject.GetComponent<TileBehaviour> ().isPassable = true;
					economy.outpost -= 1;
				}
			}
			if(this.tag == "SelectedSiegeUnit"){
				Camera.main.GetComponent<PlayerControl>().highlightingTiles = false;
				revertbackEnemies();
				Camera.main.GetComponent<GameMaster>().gameState = 0;
				Destroy (GameObject.FindGameObjectWithTag("AttackRangeIndicator").gameObject);
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

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (this.transform.position, 4f);
		}

}
