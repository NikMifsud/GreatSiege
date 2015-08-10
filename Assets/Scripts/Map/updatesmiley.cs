using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updatesmiley : MonoBehaviour {
	public happiness HappyUnits;
	public int Growth;
	public Sprite sprite1; 
	public Sprite sprite2; 
	public Sprite sprite3; 
	public Sprite sprite4; 
	public Sprite sprite5; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HappyUnits.HappyUnits >= 80){
			this.GetComponent<Image> ().sprite = sprite1;
			Growth = -1;
		}
		if (HappyUnits.HappyUnits <= 80){
			this.GetComponent<Image> ().sprite = sprite2;
			Growth = 0;
		}
		if (HappyUnits.HappyUnits <= 60){
			this.GetComponent<Image> ().sprite = sprite3;
			Growth = 1;
		}
		if (HappyUnits.HappyUnits <= 40){
			this.GetComponent<Image> ().sprite = sprite4;
			Growth = 2;
		}
		if (HappyUnits.HappyUnits <= 20){
			this.GetComponent<Image> ().sprite = sprite5;
			Growth = 3;
		}
	}
}