using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public bool pauseButtonPressed;
	public GameObject pauseCover;
	public GameMaster gameMaster;

	// Use this for initialization
	void Start () {
		pauseButtonPressed = false;
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(pauseButtonPressed){
			pauseCover.SetActive(true);
			Time.timeScale = 0;
			gameMaster.gameState = 4;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseButtonPressed = false;
			pauseCover.SetActive(false);
			Time.timeScale = 1;
			gameMaster.gameState = 0;
		}
	}

	public void ButtonClicked(){
			pauseButtonPressed = true;
		
	}
}
