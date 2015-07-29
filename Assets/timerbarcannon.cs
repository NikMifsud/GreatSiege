using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerbarcannon : MonoBehaviour {
	public float myAmount;
	public Artillery activemovement;
	public Animator timeranim;
	// Use this for initialization
	void Start () {
		Animator timeranim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		myAmount = activemovement.Movementtime / 30;
		Image myImage = GetComponent<Image>();
		myImage.fillAmount = myAmount;
		if (activemovement.Movementtime <= 0){
			timeranim.SetBool("movement", true);
		}
	}
}