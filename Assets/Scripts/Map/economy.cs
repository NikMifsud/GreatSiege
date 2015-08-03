using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class economy : MonoBehaviour {
	public int Food;
	public Text thetext;
	public float number;
	public int lostfood;
	// Use this for initialization
	void Start () {
		Food = 120;
	}
	// Update is called once per frame
	void Update () {
		number += Time.deltaTime;
	if (number >= 5){
		Food += 10;
		number = 0;
		}
		thetext.text = "" + Food;
	}
}
