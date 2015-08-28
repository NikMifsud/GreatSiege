using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlameThrower : MonoBehaviour {
	
	public bool buttonPressed;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
	public ParticleSystem fire;
	public Material highlightedMaterial;
	public float timer;
	public float radialwipe;

	// Use this for initialization
	void Start () {
		timer = 45;
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		radialwipe = (timer / 45);
		Image myImage = GameObject.Find("Power1").GetComponent<Image> ();
		Button myButton = GameObject.Find("Power1").GetComponent<Button> ();
		myImage.fillAmount = radialwipe;
		if (timer <= 45) {
			myButton.enabled = false;
			timer += Time.deltaTime;
		}
		if (timer >= 45) {
			myButton.enabled = true;
		}
		if (buttonPressed) {
			highlightAvailableUnits();
		}

		if (buttonPressed && Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "RangedUnit" || hit.collider.tag == "SiegeUnit" || hit.collider.tag == "PikeUnit") {
					buttonPressed = false;
					gameMaster.gameState = 0;
				}	
				if(hit.collider.tag == "FootUnit" && hit.collider.gameObject.GetComponent<Footsoldier>().canMove == true){
					timer = 0;
					gameMaster.gameState = 0;
					hit.collider.gameObject.GetComponent<FlameCone>().isFlame = true;
					GameObject[] objects = GameObject.FindGameObjectsWithTag("FootUnit");
					
					foreach(GameObject foot in objects){
						if(foot.GetComponent<Footsoldier>().canMove == true){
							foot.GetComponent<CharacterMovement>().unitOriginalTile.transform.FindChild("Cylinder").gameObject.GetComponent<Renderer>().material = highlightedMaterial;
							if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "FlameThrowerStone"){
								foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "StoneTile";
							}
							if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "FlameThrowerMud"){
								foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "MudTile";
							}
							if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "FlameThrowerOutpost"){
								foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "OutpostTile";
							}
							if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "FlameThrowerDirt"){
								foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "DirtTile";
							}
						}
					}
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
			highlightAvailableUnits();
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

	void highlightAvailableUnits(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag("FootUnit");

		foreach(GameObject foot in objects){
			if(foot.GetComponent<Footsoldier>().canMove == true){
				foot.GetComponent<CharacterMovement>().unitOriginalTile.transform.FindChild("Cylinder").gameObject.GetComponent<Renderer>().material = highlightedMaterial;
				if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "StoneTile"){
					foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "FlameThrowerStone";
				}
				if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "MudTile"){
					foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "FlameThrowerMud";
				}
				if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "OutpostTile"){
					foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "FlameThrowerOutpost";
				}
				if(foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag == "DirtTile"){
					foot.GetComponent<CharacterMovement>().unitOriginalTile.gameObject.tag = "FlameThrowerDirt";
				}
			//	playerControl.highlightingTiles = true;
			}
		}
	}
	
}
