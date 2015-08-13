using UnityEngine;
using System.Collections;

public class Grenades : MonoBehaviour {

	public bool buttonPressed,isGrenades;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPressed) {
			isGrenades = true;
			buttonPressed = false;
		}
	}

	public void ButtonPressed(){
		buttonPressed = true;
	}

}
