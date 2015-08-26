using UnityEngine;
using System.Collections;

public class FlameCone : MonoBehaviour {

	public FlameThrower flameThrower;
	public float timer;
	public bool timerStarted;
	public bool isFlame;
	public AudioClip  clip;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = Camera.main.GetComponent<AudioSource> ();
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
			this.isFlame = false;
			timerStarted = false;
			timer = 0;
		}

		if (this.isFlame && this.gameObject.GetComponent<Footsoldier>().canMove) {
			source.PlayOneShot(clip,1.0f);
			this.transform.FindChild("Flamethrower").gameObject.SetActive (true);
			this.gameObject.GetComponent<Footsoldier>().DecreaseCooldown();
			timerStarted = true;
		}
	}
}
