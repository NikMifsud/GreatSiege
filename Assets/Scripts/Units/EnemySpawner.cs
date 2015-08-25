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
	public Text waves;
	public int waveNumber;
	string wave;
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		//	InvokeRepeating ("Spawn", spawnTime, 0);
		waveNumber = 0;
		enemyCount = 0;
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
		wave = waveNumber.ToString();
		waves.text = "Survive 5 waves: " + wave + "/5";

		if (spawnTime <= 0) {
			spawn = true;
			spawnTime = 0;
			if(enemyCount == 15 || enemyCount == 30){
				spawnTime = 120;
				waveNumber += 1;
			}else
				spawnTime = 0;
		}
		if (spawn == false) {
			spawnTime -= Time.deltaTime;
		}
		if (spawn == true) {
			Spawn ();
		}
	}
	
	void Spawn ()
	{
		if (enemyCount <= 75) {
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
			
			GameObject enemy = enemies[Random.Range(0,3)];
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
			if(enemy.gameObject.tag == "EnemySiege"){
				position = new Vector3 (spawnTile.transform.position.x, 0.11f, spawnTile.transform.position.z);
				if(spawnTile.gameObject.tag == "StoneTile"){
					position = new Vector3 (spawnTile.transform.position.x, 0.12f, spawnTile.transform.position.z);
				}
			}
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isEnemy = true;
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
			if(enemy.tag == "RangedEnemy" || enemy.tag == "Enemy"){
				myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			}
			if(enemy.tag == "EnemySiege"){
				enemy.gameObject.GetComponent<CharacterMovement>().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			}
			Instantiate (enemy, position, Quaternion.identity);
			enemyCount += 1;
			SpawnDone ();
			
		}
	}
}