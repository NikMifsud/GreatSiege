using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class HealthPowerUps : MonoBehaviour {

	public bool HealthButtonPressed;
	public Material highlightedMaterial;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Ray _ray;
		RaycastHit _hitInfo;

		// On Left Click
		if (Input.GetMouseButtonDown (0) && HealthButtonPressed == true) {
			GameObject[] MudTiles = GameObject.FindGameObjectsWithTag ("MudTile");
			GameObject[] StoneTiles = GameObject.FindGameObjectsWithTag ("StoneTile");
			GameObject[] OutpostTiles = GameObject.FindGameObjectsWithTag ("OutpostTile");
			GameObject[] DirtTiles = GameObject.FindGameObjectsWithTag ("DirtTile");

			_ray = Camera.main.ScreenPointToRay (Input.mousePosition); // Specify the ray to be casted from the position of the mouse click
			
			// Raycast and verify that it collided
			if (Physics.Raycast (_ray, out _hitInfo)) {

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
			HealthButtonPressed = false;

			//revert tiles back
		}
	}

	public void ButtonClicked () {
		HealthButtonPressed = true;
	}
	
}
