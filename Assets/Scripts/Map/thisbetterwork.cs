using UnityEngine;
using System.Collections;

//depricated script

public class thisbetterwork : MonoBehaviour {
	Animation here;
	public int Carl = 0;
	// Use this for initialization
	void Start () {
		here = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Carl == 1){
			here.Play("moving");
		}
	}
}
