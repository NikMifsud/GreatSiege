using UnityEngine;
using System.Collections;

public class uimove : MonoBehaviour {
	public GameObject Fred;
	public float Godown;
	public float Goup;
	public int Bob = 0;
	public float Dcarl;
	public Animator anim;
	// Use this for initialization
	void Start () {
		Animator anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Bob);
	if (Bob == 1){
			anim.SetBool("Move", true);
		}
		if (Bob == 2){
				anim.SetBool("Move", false);
			}
				if (Bob > 2){
			Bob = 1;

				}

}
	 public void UIshow (){
		Bob = Bob+1;

	}
}