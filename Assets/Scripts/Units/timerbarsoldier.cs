using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerbarsoldier : MonoBehaviour {
	public float myAmount;
	public Footsoldier activemovement;
	public Animator timeranim;
	// Use this for initialization
	void Start () {
		Animator timeranim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	//line added
	void Update () {
		myAmount = activemovement.Movementtime / 10;
		Image myImage = GetComponent<Image>();
		myImage.fillAmount = myAmount;
		if (activemovement.Movementtime <= 0){
			timeranim.SetBool("movement", true);
		}
	}
}
