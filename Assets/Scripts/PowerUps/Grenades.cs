using UnityEngine;
using System.Collections;

public class Grenades : MonoBehaviour {

	public bool buttonPressed,isGrenades;
	public PlayerControl playerControl;
	public GameMaster gameMaster;
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

		if (buttonPressed) {
			timer = 0;
			isGrenades = true;
			buttonPressed = false;
			gameMaster.gameState = 0;
		}
	}

	public void ButtonPressed(){
		if (timer >= 45) {
			gameMaster.gameState = 3;
			playerControl.highlightingTiles = false;
			buttonPressed = true;
		}
	}

}
