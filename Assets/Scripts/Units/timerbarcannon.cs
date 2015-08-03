using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timerbarcannon : MonoBehaviour {
	public float myAmount;
	public Artillery activemovement;
	public Animator timeranim;
	public Animator finishedanim;
	public bool done;
	public bool canMove;
	// Use this for initialization
	void Start () {
		Animator timeranim = GetComponent<Animator>();
		done = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (activemovement.Movementtime <= 0) {
			timeranim.SetBool ("movement", true);
			finishedanim.SetBool ("finished", true);
		} 
		myAmount = activemovement.Movementtime / 30;
		Image myImage = GetComponent<Image>();
		myImage.fillAmount = myAmount;
		Transform cannon = this.transform.Find ("cannon");
	}
}