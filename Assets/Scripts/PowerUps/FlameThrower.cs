using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
	
	public bool buttonPressed,isFlame;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPressed) {
			isFlame = true;
			buttonPressed = false;
		}
	}
	
	public void ButtonPressed(){
		buttonPressed = true;
	}
	
}
