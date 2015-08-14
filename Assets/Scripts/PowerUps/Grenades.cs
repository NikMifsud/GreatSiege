using UnityEngine;
using System.Collections;

public class Grenades : MonoBehaviour {

	public bool buttonPressed,isGrenades;
	public PlayerControl playerControl;
	public GameMaster gameMaster;

	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPressed) {
			isGrenades = true;
			buttonPressed = false;
			gameMaster.gameState = 0;
		}
	}

	public void ButtonPressed(){
		gameMaster.gameState = 3;
		playerControl.highlightingTiles = false;
		buttonPressed = true;
	}

}
