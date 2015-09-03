using UnityEngine;
using System.Collections;

public class disablinghp : MonoBehaviour {
	public bool Appear;
	public bool JustHit;
	public float Numbered;
	public int CountDown;
	public Canvas hpbar;

	// Use this for initialization
	void Start () {
		CountDown = 0;
		JustHit = true;
		Appear = false;
		Numbered = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (JustHit == true) {
			Appear = true;
			Numbered += Time.deltaTime;
		}
		if (Numbered >= 1) {
			CountDown -= 1;
			Numbered = 0;
		}
		if (Appear == true) {
			hpbar.enabled = true;
		}
		
		if (CountDown <= 0) {
			Appear = false;
			JustHit = false;
			Numbered = 0;
			CountDown = 5;
		}
		
		if (Appear == false) {
			hpbar.enabled = false;
		}
		
	}
}