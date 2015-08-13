using UnityEngine;
using System.Collections;

public class FlameCone : MonoBehaviour {

	public FlameThrower flameThrower;
	public float timer;
	public bool timerStarted;
	// Use this for initialization
	void Start () {
		timer = 0;
		timerStarted = false;
		flameThrower = GameObject.Find("Flamethrower").GetComponent<FlameThrower>();
	}
	
	// Update is called once per frame
	void Update () {

		if (timer <= 3 && timerStarted == true) {
			timer += Time.deltaTime;
		}

		if (timer > 3) {
			this.transform.FindChild("Flamethrower").gameObject.SetActive (false);
			flameThrower.isFlame = false;
			timerStarted = false;
			timer = 0;
		}

		if (flameThrower.isFlame) {
			this.transform.FindChild("Flamethrower").gameObject.SetActive (true);
			timerStarted = true;

		}




	
	}
}
