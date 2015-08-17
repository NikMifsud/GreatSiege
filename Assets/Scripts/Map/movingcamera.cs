using UnityEngine;
using System.Collections;

public class movingcamera : MonoBehaviour {
	public GameObject target;
	private Vector3 point;
	public float speedMod;

	public void fuckingshit(){
		speedMod = 0;
	}
	// Use this for initialization
	void Start () {
		point = target.transform.position;//get target's coords
		transform.LookAt(point);//makes the camera look to it
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (point,new Vector3(0.0f,1.0f,0.0f),20 * Time.deltaTime * speedMod);
	}
}
