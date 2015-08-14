using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {

	public int enemiesKilled;
	public GameObject text;
	// Use this for initialization
	void Start () {
		enemiesKilled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemiesKilled >= 30) {
			Time.timeScale = 0;
			text.SetActive(true);
		}
	}
}
