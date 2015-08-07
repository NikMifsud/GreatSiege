using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class economy : MonoBehaviour {
	public int Food;
	public Text thetext;
	public float number;
	public int lostfood;
	public int outpost;
	// Use this for initialization
	void Start () {
		Food = 120;
		outpost = 0;
	}

	// Update is called once per frame
	void Update () {
		number += Time.deltaTime;
		if (number >= 5){
			Food += (10+ (5*outpost));
			number = 0;
		}
		thetext.text = "" + Food;
	}
}
