using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
	
	public bool buttonPressed;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public ParticleSystem fire;

	public float timer;

	// Use this for initialization
	void Start () {
		timer = 45;
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (timer <= 45) {
			timer += Time.deltaTime;
		}

		if (buttonPressed && Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit") {
					buttonPressed = false;
					gameMaster.gameState = 0;
				}	
				if(hit.collider.tag == "FootUnit" && hit.collider.gameObject.GetComponent<Footsoldier>().canMove == true){
					timer = 0;
					gameMaster.gameState = 0;
					hit.collider.gameObject.GetComponent<FlameCone>().isFlame = true;
				//	fire.transform.position = hit.collider.gameObject.transform.position;
			//		fire.emissionRate = 100f;
			//		fire.gameObject.transform.FindChild("Smoke").gameObject.GetComponent<ParticleSystem>().emissionRate = 1f;
			//		fire.gameObject.transform.FindChild("Sparkles").gameObject.GetComponent<ParticleSystem>().emissionRate = 0f;
			//		fire.Simulate(0f);
			//		StartCoroutine(WaitForFlame ());
					buttonPressed = false;
				}

			gameMaster.gameState = 0;
			buttonPressed = false;
			}
		}
	}
	
	public void ButtonPressed(){
		if (timer >= 45) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			buttonPressed = true;
		}
	}

	IEnumerator WaitForFlame(){
		yield return new WaitForSeconds (3);
		fire.emissionRate = 0f;
		fire.gameObject.transform.FindChild("Smoke").gameObject.GetComponent<ParticleSystem>().emissionRate = 0f;
		fire.gameObject.transform.FindChild("Sparkles").gameObject.GetComponent<ParticleSystem>().emissionRate = 0f;
	}
	
}
