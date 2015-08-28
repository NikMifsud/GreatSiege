using UnityEngine;
using System.Collections;

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
		if (enemiesKilled >= 75 && Leveldone == false) {
			objectiveComplete = true;
			statScreen.gameObject.SetActive(true);
			statScreen.gameObject.GetComponent<UpdateStatistics>().UpdateStatisticScreen();

			gameMaster.gameState = 4;
			Time.timeScale = 0;
			Leveldone =  true;
			//text.SetActive(true);
		}
	}
}
