using UnityEngine;
using System.Collections;

public class Rangedsoldier : MonoBehaviour {
	public float curr_Health;
	public float max_Health;
	public int Armor;
	public int Movement;
	public int AttackRange;
	public int Attack;
	public Statistics stats;
	public int DamageTaken;
	public float Movementtime;
	public bool canMove;
	public GameObject healthBar;
	public bool isSelected;
	public economy economy;
	public bool attack;
	public bool isMud;
	public bool isDead;
	// Use this for initialization
	void Start () {
		stats = Camera.main.GetComponent<Statistics> ();
		max_Health = 100;
		curr_Health = 100;
		Armor = 0;
		isDead = false;
		attack = false;
		Movement = 2;
		//AttackRange = 4;
		Attack = 30;
		canMove = true;
		isSelected = false;
		Movementtime = 15;
		isMud = false;
		economy = GameObject.FindGameObjectWithTag ("Economy").GetComponent<economy>();
	}

	public void DecreaseCooldown(){
		canMove = false;
	}

	// Update is called once per frame
	void Update () {
		if (curr_Health > 100) {
			curr_Health = 100;
		}

		if (isMud) {
			Movementtime = 20;
			isMud = false;
		}

		if (Movementtime <= 0) {
			canMove = true;
			attack = false;
			Movementtime = 15;
		}
		if (canMove == false) {
			Movementtime -= Time.deltaTime;
		}
		if (curr_Health <= 0) {
			if(isDead == false){
				stats.rangedDead += 1;
				isDead = true;
			}
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
			if(this.tag == "SelectedRangedUnit"){
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

}