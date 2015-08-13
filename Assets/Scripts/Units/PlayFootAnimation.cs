using UnityEngine;
using System.Collections;

public class PlayFootAnimation : MonoBehaviour {

	public Animator anim;
	public Animation attackAnim;
	public Footsoldier attack;
	public bool attacked = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (attack.attack == true) {
			anim.SetBool ("isAttack", true);
			attacked = true;
		}
		if (attack.attack == false) {
			anim.SetBool ("isAttack", false);
		}
	}


	public IEnumerator WaitForAnimation(GameObject enemy){
		while (true) {
			if (attacked){
				Debug.Log ("Waiting");
				yield return new WaitForSeconds (2f);
				Debug.Log ("Playing");
				if(enemy.tag == "Enemy"){
					enemy.gameObject.GetComponent<Enemy> ().DealtDamage (50);
				}
				if(enemy.tag == "RangedEnemy"){
					enemy.gameObject.GetComponent<EnemyRanged> ().DealtDamage (50);
				}
			}
			yield return null;
		}
	}

}
