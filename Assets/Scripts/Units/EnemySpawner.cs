using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
	
	public float spawnTime = 0;            // How long between each spawn.
	public int enemyCount; 
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public List<Transform> tiles;
	bool spawn;
	public GameObject tile;
	public GameObject[] enemies;
	//public GameObject Dragut;
	public bool DragutSpawned;
	public int enemiesDead;
	public List<GameObject[]> enemiesAlive;
	public Text waves;
	public static int waveNumber;
	public int siegeCount;
	public int dragutCount;
	GameObject enemy;
	public Statistics stats;
	string wave;
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		//	InvokeRepeating ("Spawn", spawnTime, 0);
		waveNumber = 1;
		enemyCount = 1;
		spawnTime = 0;
		siegeCount = 0;
		dragutCount = 0;
		DragutSpawned = false;
		enemiesDead = 0;
		spawn = true;
	}
	
	public void Movementcheck(){
		spawnTime = 15;
	}
	
	public void SpawnDone(){
		spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		wave = waveNumber.ToString ();
		if (Application.loadedLevelName == "Knights1") {
			waves.text = "Survive 4 waves: " + wave + "/4";
		}
		if (Application.loadedLevelName == "Knights2") {
			waves.text = "Kill Dragut";
		} 
		if (enemiesDead == 10 && enemyCount <= 40) {
			spawnTime = 0;
			siegeCount = 0;
			dragutCount = 0;
			waveNumber += 1;
			enemiesDead = 0;
		}


		if (spawnTime <= 0) {
			spawn = true;
			spawnTime = 0;
			if (enemyCount == 10 || enemyCount == 20 || enemyCount == 30) {
				spawnTime = 90;
				siegeCount = 0;
				dragutCount = 0;
				waveNumber += 1;
			} else {
				spawnTime = 0;
			}
			if (spawn == false) {
				spawnTime -= Time.deltaTime;
			}
			if (spawn == true) {
				Spawn ();
			}
		}
	}
	public void Spawn ()
	{
		if (enemyCount <= 40) {
			tiles = new List<Transform> ();
			Transform transform = GameObject.Find ("HexagonGrid").transform;
			foreach (Transform child in transform) {
				if(child.gameObject.GetComponent<TileBehaviour>().isPassable == true)
					tiles.Add (child);
			}
			tiles.RemoveRange (22, (tiles.Count - 22));
			tiles.ToArray ();
			
			int randomTileIndex = UnityEngine.Random.Range (0, 22);
			GameObject spawnTile = tiles [randomTileIndex].gameObject;
			Vector3 position = new Vector3(0,0,0);
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
			if (Application.loadedLevelName == "Knights2"){
					if((siegeCount < 2) && (dragutCount <1)){
						enemy = enemies[Random.Range(0,4)];
					}else if ((dragutCount >= 1) && (siegeCount <2)){
						enemy = enemies[Random.Range(0,3)];
					}else {
						enemy = enemies[Random.Range(0,2)];
				}
			}
			if (Application.loadedLevelName != "Knights2"){
				if(siegeCount < 2){
					enemy = enemies[Random.Range(0,3)];
				}
				else {
					enemy = enemies[Random.Range(0,2)];
				}
			}

			Transform myUnitTransform = null;
			if(enemy.gameObject.tag == "RangedEnemy"){
				myUnitTransform = enemy.transform.FindChild ("EnemyRanged");
				position = new Vector3 (spawnTile.transform.position.x, 0.16f, spawnTile.transform.position.z);
				if(spawnTile.gameObject.tag == "StoneTile"){
					myUnitTransform.gameObject.GetComponent<EnemyRanged>().AttackRange = 5;
					position = new Vector3 (spawnTile.transform.position.x, 0.2f, spawnTile.transform.position.z);
				}
			}
			if(enemy.gameObject.tag == "Enemy"){
				myUnitTransform = enemy.transform.FindChild("Enemy");
				position = new Vector3 (spawnTile.transform.position.x, 0.11f, spawnTile.transform.position.z);
				if(spawnTile.gameObject.tag == "StoneTile"){
					position = new Vector3 (spawnTile.transform.position.x, 0.12f, spawnTile.transform.position.z);
				}
			}
			if(enemy.gameObject.tag == "Dragut"){
				myUnitTransform = enemy.transform.FindChild("Dragut");
				dragutCount +=1;
				position = new Vector3 (spawnTile.transform.position.x, 0.11f, spawnTile.transform.position.z);
				if(spawnTile.gameObject.tag == "StoneTile"){
					position = new Vector3 (spawnTile.transform.position.x, 0.12f, spawnTile.transform.position.z);
				}
			}
			if(enemy.gameObject.tag == "EnemySiege"){
				siegeCount += 1;
				position = new Vector3 (spawnTile.transform.position.x, 0.11f, spawnTile.transform.position.z);
				if(spawnTile.gameObject.tag == "StoneTile"){
					position = new Vector3 (spawnTile.transform.position.x, 0.12f, spawnTile.transform.position.z);
				}
			}
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isEnemy = true;
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
			if((enemy.tag == "RangedEnemy") || (enemy.tag == "Enemy") || (enemy.tag == "Dragut")){
				myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			}
			if(enemy.tag == "EnemySiege"){
				enemy.gameObject.GetComponent<CharacterMovement>().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			}
			Instantiate (enemy, position, Quaternion.identity);
			/*if ((Application.loadedLevelName == "Knights2") && (DragutSpawned == false) && waveNumber == 1 ){
				Instantiate (enemies [3], position, Quaternion.identity);
				DragutSpawned = true;
			}*/
			enemyCount += 1;


			SpawnDone ();
		}
	}
}