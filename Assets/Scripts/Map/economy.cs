using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/*	This script is attached to the Fort
Economy managment to increase economy
*/

public class economy : MonoBehaviour {
	public int Food;
	public Text thetext;
	public float number;
	public int outpost;
	public Statistics stats;
	public int lostfood;
	public int waittime;
	public updatesmiley Growth;
	// Use this for initialization
	void Start () {
		outpost = 0;
		Food = 500;
		stats = Camera.main.GetComponent<Statistics> ();
		
	}
	// Update is called once per frame
	void Update () {
		if (Food < 0) {
			Food = 0;
		}
		stats.outpostCaptured = outpost;
		waittime = 2 + Growth.Growth;
		number += Time.deltaTime;
		if (number >= waittime){
			Food += (2+(2*outpost));
			number = 0;
		}
		thetext.text = "" + Food;
	}
}