using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
	
	public bool buttonPressed;
	public PlayerControl playerControl;
	public GameMaster gameMaster;

	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPressed && Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit") {
					buttonPressed = false;
					gameMaster.gameState = 0;
				}	
				if(hit.collider.tag == "FootUnit" && hit.collider.gameObject.GetComponent<Footsoldier>().canMove == true){
					gameMaster.gameState = 0;
					hit.collider.gameObject.GetComponent<FlameCone>().isFlame = true;
					buttonPressed = false;
				}

			gameMaster.gameState = 0;
			buttonPressed = false;
			}
		}
	}
	
	public void ButtonPressed(){
		gameMaster.gameState = 3;
		playerControl.highlightingTiles = false;
		buttonPressed = true;
	}
	
}
