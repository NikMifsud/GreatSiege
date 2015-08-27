using UnityEngine;
using System.Collections;

public class ButtonClickSound : MonoBehaviour {

	public AudioClip clip;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = Camera.main.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ButtonClicked(){
		source.PlayOneShot (clip, 1f);
	}
}
