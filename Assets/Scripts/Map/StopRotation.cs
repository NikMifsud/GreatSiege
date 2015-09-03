using UnityEngine;
using System.Collections;

public class StopRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion rotation = Quaternion.Euler(0, -90, -180);
		this.transform.rotation = rotation;
	}
}
