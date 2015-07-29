using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class economy : MonoBehaviour {
	public int Food;
	public Text thetext;
	public float number;
	public int lostfood;
	public int waittime;
	public updatesmiley Growth;
	// Use this for initialization
	void Start () {
		Food = 1200;

	}
	// Update is called once per frame
	void Update () {
		waittime = 2 + Growth.Growth;
		number += Time.deltaTime;
	if (number >= waittime){
		Food += 2;
		number = 0;
		}
		thetext.text = "" + Food;
	}
}
