using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
	
	public float spawnTime = 0;            // How long between each spawn.
	public int enemyCount; 
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public List<Transform> tiles;
	bool spawn;
	public GameObject tile;
	public GameObject[] enemies;

	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
	//	InvokeRepeating ("Spawn", spawnTime, 0);
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
		if (spawnTime <= 0) {
			spawn = true;
			spawnTime = 0;
			if(enemyCount >= 15){
				spawnTime = 15;
			}
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
		if (enemyCount <= 30) {
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

			GameObject enemy = enemies[Random.Range(0,2)];
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

			spawnTile.gameObject.GetComponent<TileBehaviour> ().isEnemy = true;
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
			myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			Instantiate (enemy, position, Quaternion.identity);
			enemyCount += 1;
			SpawnDone ();

		}
	}
}