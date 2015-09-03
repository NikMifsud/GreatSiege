using UnityEngine;
using System.Collections;

public class FaderFlags : MonoBehaviour {
	

	public Transform other;
	public bool isTransparent,isSolid;
	// Use this for initialization
	void Start () {
		other = Camera.main.transform;
		isSolid = false;
		isTransparent = false;
	}
	
	// Update is called once per frame
	void Update () {
		//	ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//	if (Physics.Raycast (ray, out hit)) {
		//		if(hit.collider.gameObject.tag == "Fort"){
		//			Color color = GetComponent<Renderer> ().material.color;
		//			color.a -= 0.5f;
		//			GetComponent<Renderer> ().material.color = color;
		//		}
		//
		//}
		
		float dist = Vector3.Distance(other.position, this.transform.position);
		if (dist < 4 && !isTransparent) {
			Color color = this.transform.GetComponent<Renderer>().material.color;
			//	Color color = GetComponent<Renderer> ().material.color;
			color.a -= 0.5f;
			this.transform.GetComponent<Renderer>().material.color = color;
			isTransparent = true;
			isSolid = false;
		}
		
		else if (dist > 4 && !isSolid) {
			
			isTransparent = false;
			Color color = this.transform.GetComponent<Renderer>().material.color;
			//Color color = GetComponent<Renderer> ().material.color;
			color.a += 0.5f;
			
			this.transform.GetComponent<Renderer>().material.color = color;

			
			isSolid = true;
		}
		//print("Distance to other: " + dist);
	}
	
}
