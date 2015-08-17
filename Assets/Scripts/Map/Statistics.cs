using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {

	public int enemiesKilled;
	public GameObject text;
	public GameMaster gameMaster;
	public GameObject pauseCover;
	// Use this for initialization
	void Start () {
		enemiesKilled = 0;
		gameMaster = Camera.main.GetComponent<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemiesKilled >= 30) {
			Time.timeScale = 0;
			pauseCover.SetActive(false);
			gameMaster.gameState = 4;
			text.SetActive(true);
		}
	}
}
