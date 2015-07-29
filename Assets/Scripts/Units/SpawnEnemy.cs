using UnityEngine;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour {
	public bool SpawnSoldier;
	public GameObject Soldier;
	private GameMaster gameMaster;
	private PlayerControl playerControl;
	public Transform[] tileList;
	public Material highlightedTexture,mudTexture,dirtTexture,outpostTexture,stoneTexture;
	public List<Transform> tiles;
	// Use this for initialization
	void Start () {
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SpawnSoldier == true && Input.GetMouseButtonDown(0)){
			//deselect all those in the scene in order to avoid user error
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			if(Physics.Raycast(ray,out hit))
			{
				if(hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit" || hit.collider.tag == "FootUnit"){
					SpawnSoldier = false;
					gameMaster.gameState = 0;

				}
				else if(hit.collider.GetComponent<TileBehaviour>().isPassable == true)
				{
					var cubeTemp = hit.collider.transform.position;
					if(hit.collider.gameObject.tag == "StoneTile"){
						Soldier.transform.FindChild("Enemy").GetComponent<Enemy>().Armor = 15;
						cubeTemp.y = 0.12f;
					}
					else
						cubeTemp.y = 0.1f;
					Transform myUnitTransform = Soldier.transform.FindChild("Enemy");
					myUnitTransform.GetComponent<CharacterMovement>().unitOriginalTile = hit.collider.gameObject.GetComponent<TileBehaviour>();
					Instantiate(Soldier,cubeTemp,Quaternion.identity);
					hit.collider.gameObject.GetComponent<TileBehaviour>().isPassable = false;
					hit.collider.gameObject.GetComponent<TileBehaviour>().isEnemy = true;
					SpawnSoldier = false;
					gameMaster.gameState = 0;
					Soldier.transform.FindChild("Enemy").gameObject.GetComponent<Enemy> ().DecreaseCooldown();
				} else{
					SpawnSoldier = false;
					gameMaster.gameState = 0;
				}
			}else{
				SpawnSoldier = false;
				gameMaster.gameState = 0;
			}
		}
	}
	
	public void ButtonClicked () {
		playerControl.highlightingTiles = false;
		SpawnSoldier = true;
		gameMaster.gameState = 3;
	}
}
