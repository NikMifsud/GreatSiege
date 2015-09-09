using UnityEngine;
using System.Collections;

//attached to the main camera <-- IMP
//used to store all the counts of all the different statistics in the game, from outpost to allied dead and enemies killed
//has the winning termination condition in it which loads the stats screen

public class Statistics : MonoBehaviour {

	public int enemiesKilled;
	public GameObject text;
	public GameMaster gameMaster;
	public GameObject pauseCover;
	public int footSoldierDead, rangedDead,musketDead,pikeDead,enemyFootDead,enemyRangedDead,enemySiegeDead, outpostCaptured,foodGenerated,foodSpent;
	public bool objectiveComplete;
	public Canvas statScreen;
	public float gametimer;
	public int minutes;
	bool Leveldone;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		minutes = 0;
		gametimer = 0;
		enemiesKilled = 0;
		footSoldierDead = 0;
		rangedDead = 0;
		musketDead = 0;
		pikeDead = 0;
		enemyFootDead = 0;
		enemyRangedDead = 0;
		enemySiegeDead = 0;
		outpostCaptured = 0;
		foodGenerated = 0;
		foodSpent = 0;
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		Leveldone = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("outposts: " + outpostCaptured);
		gametimer += Time.deltaTime;
		if (gametimer >= 60) {
			minutes += 1;
			gametimer = 0;
		}

		if (Application.loadedLevelName == "Knights1") {
			if (enemiesKilled >= 40 && Leveldone == false) {
				objectiveComplete = true;
				statScreen.gameObject.SetActive (true);
				statScreen.gameObject.GetComponent<UpdateStatistics> ().UpdateStatisticScreen ();

				gameMaster.gameState = 4;
				Time.timeScale = 0;
				Leveldone = true;
				//text.SetActive(true);
			}
		}

		if (Application.loadedLevelName == "Knights2") {
			if (minutes >= 10 && Leveldone == false) { // if Dragut = dead instead of 10 mins since dragut will b spawned on wave number 3
				objectiveComplete = true;
				statScreen.gameObject.SetActive (true);
				statScreen.gameObject.GetComponent<UpdateStatistics> ().UpdateStatisticScreen ();
				
				gameMaster.gameState = 4;
				Time.timeScale = 0;
				Leveldone = true;
			}
		}

		if (Application.loadedLevelName == "Knights3") {
			if (EnemySpawner.waveNumber >= 4 && Leveldone == false) {
				objectiveComplete = true;
				statScreen.gameObject.SetActive (true);
				statScreen.gameObject.GetComponent<UpdateStatistics> ().UpdateStatisticScreen ();
				
				gameMaster.gameState = 4;
				Time.timeScale = 0;
				Leveldone = true;
			}
		}

		if (Application.loadedLevelName == "Knights4") {
			if (enemiesKilled >= 40  && Leveldone == false) { // and Vallette = alive
				objectiveComplete = true;
				statScreen.gameObject.SetActive (true);
				statScreen.gameObject.GetComponent<UpdateStatistics> ().UpdateStatisticScreen ();
				
				gameMaster.gameState = 4;
				Time.timeScale = 0;
				Leveldone = true;
			}
		}

		if (Application.loadedLevelName == "Knights5") { // destroy seige tower + capture 2 control points
			if (enemiesKilled >= 40  && Leveldone == false) { // and Vallette = alive
				objectiveComplete = true;
				statScreen.gameObject.SetActive (true);
				statScreen.gameObject.GetComponent<UpdateStatistics> ().UpdateStatisticScreen ();
				
				gameMaster.gameState = 4;
				Time.timeScale = 0;
				Leveldone = true;
			}
		}
	}
}
