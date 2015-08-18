using UnityEngine;
using System.Collections;

public class CloudsBehindTrees : MonoBehaviour {

	void Start () {
		if(GetComponent<Renderer>())
			GetComponent<Renderer>().material.renderQueue = 2900;
	}
}
