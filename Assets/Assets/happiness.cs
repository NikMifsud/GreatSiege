using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class happiness : MonoBehaviour {
	public int HappyUnits; 
	public Text DisplayHappy;


	// Use this for initialization
	void Start () {
		HappyUnits = 100;
	}
	
	// Update is called once per frame
	void Update () {
		DisplayHappy.text = HappyUnits + "%";
	}
}
