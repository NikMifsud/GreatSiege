using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class HealthPowerUps : MonoBehaviour {

	public bool HealthButtonPressed;
	public Material highlightedMaterial;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public ParticleSystem healingAura;
	public bool playing;
	public float timer;


	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		timer = 45;
	}
	
	// Update is called once per frame
	void Update () {

		if (timer <= 45) {
			timer += Time.deltaTime;
		}

		Ray _ray;
		RaycastHit _hitInfo;

		// On Left Click
		if (Input.GetMouseButtonDown (0) && HealthButtonPressed == true) {
			timer = 0;
			GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
			GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
			GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
			GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");

			_ray = Camera.main.ScreenPointToRay (Input.mousePosition); // Specify the ray to be casted from the position of the mouse click

			// Raycast and verify that it collided
			if (Physics.Raycast (_ray, out _hitInfo)) {
				healingAura.transform.position = _hitInfo.collider.gameObject.transform.position;
				//healingAura.transform.position.y += 0.1;
				playing = true;
				healingAura.emissionRate = 200f;
				healingAura.gameObject.transform.FindChild("Sparkles").gameObject.GetComponent<ParticleSystem>().emissionRate = 10f;
				StartCoroutine(WaitForAnimation());
				foreach(GameObject tile in MudTiles){
				var path = PathFinder.FindPath (_hitInfo.collider.GetComponent<TileBehaviour> ().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
					if (path.ToList ().Count <= 3 && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
							if (objectHit.collider.gameObject.tag == "FootUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 60;
							}
							if (objectHit.collider.gameObject.tag == "RangedUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 60;
							} 
							if (objectHit.collider.gameObject.tag == "Artillery") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Artillery> ().curr_Health += 60;
							}
						}

					}
					if (path.ToList ().Count <= 3){
						tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
					}
				}

				foreach(GameObject tile in DirtTiles){
					var path = PathFinder.FindPath (_hitInfo.collider.GetComponent<TileBehaviour> ().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
					if (path.ToList ().Count <= 3 && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
							if (objectHit.collider.gameObject.tag == "FootUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 60;
							}
							if (objectHit.collider.gameObject.tag == "RangedUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 60;
							} 
							if (objectHit.collider.gameObject.tag == "Artillery") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Artillery> ().curr_Health += 60;
							}
						}
					}
					if (path.ToList ().Count <= 3){
						tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
					}
				}

				foreach(GameObject tile in StoneTiles){
					var path = PathFinder.FindPath (_hitInfo.collider.GetComponent<TileBehaviour> ().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
					if (path.ToList ().Count <= 3 && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
							if (objectHit.collider.gameObject.tag == "FootUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 60;
							}
							if (objectHit.collider.gameObject.tag == "RangedUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 60;
							} 
							if (objectHit.collider.gameObject.tag == "Artillery") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Artillery> ().curr_Health += 60;
							}
						}
					}
					if (path.ToList ().Count <= 3){
						tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
					}
				}

				foreach(GameObject tile in OutpostTiles){
					var path = PathFinder.FindPath (_hitInfo.collider.GetComponent<TileBehaviour> ().tile, tile.gameObject.GetComponent<TileBehaviour> ().tile);
					if (path.ToList ().Count <= 3 && tile.gameObject.GetComponent<TileBehaviour> ().isPassable == false) {
						RaycastHit objectHit;
						Vector3 up = Vector3.up;
						if (Physics.Raycast (tile.gameObject.transform.position, up, out objectHit, 50)) {
							if (objectHit.collider.gameObject.tag == "FootUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Footsoldier> ().curr_Health += 60;
							}
							if (objectHit.collider.gameObject.tag == "RangedUnit") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Rangedsoldier> ().curr_Health += 60;
							} 
							if (objectHit.collider.gameObject.tag == "Artillery") {
								objectHit.collider.gameObject.GetComponent<disablinghp>().JustHit = true;
								objectHit.collider.gameObject.GetComponent<Artillery> ().curr_Health += 60;
							}
						}
					}
					if (path.ToList ().Count <= 3){
						tile.gameObject.GetComponent<TileBehaviour>().powerUp = false;
					}
				}
			}
			playing = false;
			HealthButtonPressed = false;
			gameMaster.gameState = 0;

		}
	}

	public void ButtonClicked () {
		if (timer >= 45) {
			HealthButtonPressed = true;
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
		}
	}

	
	public IEnumerator WaitForAnimation(){
		while (true) {
			if (playing){
				yield return new WaitForSeconds (2f);
				healingAura.emissionRate = 0f;
				healingAura.gameObject.transform.FindChild("Sparkles").gameObject.GetComponent<ParticleSystem>().emissionRate = 0f;
			}
			yield return null;
		}
	}
	
}
