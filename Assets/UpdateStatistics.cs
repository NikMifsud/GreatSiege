using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateStatistics : MonoBehaviour {

	public Statistics stats;
	public Text footUnits;
	public Text rangedUnits;
	public Text pikeUnits;
	public Text musketUnits;
	public Text enemy;
	public Text enemyRanged;
	public Text enemySiege;
	public Text outpostCaptured;
	public Text totalFood;
	public Text spentFood;
	public Text knightsTotals, OttomansTotals, ResourcesTotals;
	public Text minutes, seconds;
	public bool objectivesComplete;
	public GameObject gold1,gold2,gold3,gold4,gold5;
	public GameObject knightsGold,OttomanGold,ResourceGold;
	public GameObject outpostGold,objectivesGold;
	public GameObject levelWon, levelLost, nextLevel;
	public int starCount;
	// Use this for initialization
	void Start () {
		starCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateStatisticScreen(){
		footUnits.text = "" + stats.footSoldierDead;
		rangedUnits.text = stats.rangedDead.ToString();
		pikeUnits.text = stats.pikeDead.ToString();
		musketUnits.text = stats.musketDead.ToString();
		enemy.text = stats.enemyFootDead.ToString ();
		enemyRanged.text = stats.enemyRangedDead.ToString ();
		enemySiege.text = stats.enemySiegeDead.ToString ();
		outpostCaptured.text = stats.outpostCaptured.ToString ();
		totalFood.text = stats.foodGenerated.ToString ();
		spentFood.text = stats.foodSpent.ToString ();

		knightsTotals.text = (stats.footSoldierDead + stats.rangedDead + stats.pikeDead + stats.musketDead).ToString ();
		OttomansTotals.text = (stats.enemyFootDead + stats.enemyRangedDead + stats.enemySiegeDead).ToString ();
		ResourcesTotals.text = (stats.foodGenerated - stats.foodSpent).ToString();

		if ((stats.footSoldierDead + stats.rangedDead + stats.pikeDead + stats.musketDead) < 80) {
			knightsGold.SetActive(true);
			starCount += 1;
		}
		if ((stats.enemyFootDead + stats.enemyRangedDead + stats.enemySiegeDead) >= 120) {
			OttomanGold.SetActive(true);
			starCount += 1;
		}
		if ((stats.foodGenerated - stats.foodSpent) >= 0) {
			ResourceGold.SetActive(true);
			starCount += 1;
		}
		if (stats.outpostCaptured >= 1) {
			outpostGold.SetActive(true);
			starCount += 1;
		}
		if (stats.objectiveComplete == true) {
			objectivesGold.SetActive(true);
			levelWon.SetActive(true);
			starCount += 1;
		}
		if (stats.objectiveComplete == false) {
			levelLost.SetActive(true);
			nextLevel.SetActive(false);
		}

		if (starCount == 1) {
			gold1.SetActive(true);
			gold2.SetActive(false);
			gold3.SetActive(false);
			gold4.SetActive(false);
			gold5.SetActive(false);
		}
		if (starCount == 2) {
			gold1.SetActive(true);
			gold2.SetActive(true);
			gold3.SetActive(false);
			gold4.SetActive(false);
			gold5.SetActive(false);
		}
		if (starCount == 3) {
			gold1.SetActive(true);
			gold2.SetActive(true);
			gold3.SetActive(true);
			gold4.SetActive(false);
			gold5.SetActive(false);
		}
		if (starCount == 4) {
			gold1.SetActive(true);
			gold2.SetActive(true);
			gold3.SetActive(true);
			gold4.SetActive(true);
			gold5.SetActive(false);
		}
		if (starCount == 5) {
			gold1.SetActive(true);
			gold2.SetActive(true);
			gold3.SetActive(true);
			gold4.SetActive(true);
			gold5.SetActive(true);
		}

		minutes.text = ((stats.minutes).ToString ());
		seconds.text = ((stats.gametimer).ToString ("0"));


	}
}
