using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{

	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 12f;            // How long between each spawn.
	public int enemyCount; 
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public List<Transform> tiles;
	bool spawn;
	public GameObject tile;
	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
	//	InvokeRepeating ("Spawn", spawnTime, spawnTime);
		enemyCount = 0;
		spawn = true;
	}

	public void Movementcheck(){
		spawnTime = 12;
	}
	
	public void SpawnDone(){
		spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTime <= 0) {
			spawn = true;
			spawnTime = 12;
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
		if (enemyCount <= 10) {
			tiles = new List<Transform> ();

			//ArrayList tiles = new ArrayList ();
			Transform transform = GameObject.Find ("HexagonGrid").transform;
			foreach (Transform child in transform) {
				//if(child.gameObject.GetComponent<TileBehaviour>().isPassable == true)
				tiles.Add (child);
			}
			tiles.RemoveRange (22, (tiles.Count - 22));
			tiles.ToArray ();
		
			int randomTileIndex = UnityEngine.Random.Range (0, 22);
			GameObject spawnTile = tiles [randomTileIndex].gameObject;
			Vector3 position = new Vector3 (spawnTile.transform.position.x, 0.12f, spawnTile.transform.position.z);
			// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.

			Transform myUnitTransform = enemy.transform.FindChild ("Enemy");
		
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isEnemy = true;
			spawnTile.gameObject.GetComponent<TileBehaviour> ().isPassable = false;
			myUnitTransform.GetComponent<CharacterMovement> ().unitOriginalTile = spawnTile.gameObject.GetComponent<TileBehaviour> ();
			Instantiate (enemy, position, Quaternion.identity);
			enemyCount += 1;
			SpawnDone ();

			/*
		position.y += 0.11f;
		RaycastHit objectHit;
		Vector3 down = Vector3.down;
		Debug.Log ("DRAWINGRAY");
		Debug.DrawRay (position, Vector3.down * 50, Color.green);
		if (Physics.Raycast(position, down, out objectHit, 50))
		{
			Debug.Log("hit something");
			Debug.Log(objectHit.collider.gameObject.name);
			if(objectHit.collider.gameObject.tag == "DirtTile"){
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = true;
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = false;
				enemy.GetComponent<CharacterMovement>().unitOriginalTile = objectHit.collider.gameObject.GetComponent<TileBehaviour>();
			}
			else if(objectHit.collider.gameObject.tag == "MudTile"){
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = true;
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = false;
				enemy.GetComponent<CharacterMovement>().unitOriginalTile = objectHit.collider.gameObject.GetComponent<TileBehaviour>();
			}
			else if(objectHit.collider.gameObject.tag == "StoneTile"){
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = true;
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = false;
				enemy.GetComponent<CharacterMovement>().unitOriginalTile = objectHit.collider.gameObject.GetComponent<TileBehaviour>();
			} 
			else if(objectHit.collider.gameObject.tag == "OutpostTile"){
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = true;
				objectHit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = false;
				enemy.GetComponent<CharacterMovement>().unitOriginalTile = objectHit.collider.gameObject.GetComponent<TileBehaviour>();
			}
		}
*/
			//	enemy.GetComponent<CharacterMovement>().unitOriginalTile;
		}
	}
}