using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class movingcamera : MonoBehaviour 
{
	public float currentRotation;
	public GameObject target;
	private Vector3 point;
	public Slider Sliders;
	
	void Start() {
		Sliders = Sliders.GetComponent<Slider>();
		currentRotation = 0;
		point = target.transform.position;
		transform.LookAt(point);
	}
	void Update(){
		transform.LookAt(point);
		transform.RotateAround (target.transform.position,Vector3.up,currentRotation/8);
		if (Input.GetMouseButtonUp(0)){
			Sliders.value = 0;
		}
	}
	//void OnGUI() 
	//{
	//	transform.localEulerAngles = new Vector3(0.0f, currentRotation, 0.0f);
	//}
	public void Slider(float newCurrentRotation){
		currentRotation = newCurrentRotation;
	}
	
}