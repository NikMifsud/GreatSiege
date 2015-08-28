using UnityEngine;
using System.Collections;

public class dontkillme : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	if (Application.loadedLevelName == "Cutscene1") {
			Destroy (this.gameObject);
		}
	}
}
