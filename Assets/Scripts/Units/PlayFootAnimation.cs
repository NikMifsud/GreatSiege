using UnityEngine;
using System.Collections;

public class PlayFootAnimation : MonoBehaviour {

	public Animator anim;
	public Animation attackAnim;
	public Footsoldier attack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (attack.attack == true) {
			anim.SetBool ("isAttack", true);
		}
		if (attack.attack == false) {
			anim.SetBool ("isAttack", false);
		}
	}


	public IEnumerator WaitForAnimation(GameObject enemy){
		Debug.Log ("Waiting");
		yield return new WaitForSeconds (4);
		Debug.Log ("Playing");
		enemy.gameObject.GetComponent<EnemyRanged>().DealtDamage(50);
	}

}
