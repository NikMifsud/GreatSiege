using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class economy : MonoBehaviour {
	public int Food;
	public Text thetext;
	public float number;
	public int lostfood;
	public int outpost;
	public Statistics stats;
	// Use this for initialization
	void Start () {
		Food = 500;
		outpost = 0;
		stats = Camera.main.GetComponent<Statistics> ();
	}

	// Update is called once per frame
	void Update () {
		stats.outpostCaptured = outpost;
		number += Time.deltaTime;
		if (number >= 5){
			Food += (10+ (5*outpost));
			stats.foodGenerated += (10+ (5*outpost));
			number = 0;
		}
		thetext.text = "" + Food;


	}
}
