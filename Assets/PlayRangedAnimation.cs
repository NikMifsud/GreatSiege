using UnityEngine;
using System.Collections;

public class PlayRangedAnimation : MonoBehaviour {
	
	public Animator anim;
	public Animation attackAnim;
	public Rangedsoldier attack;
	public bool attacked = false;
	public bool dead = false;
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
		if (attack.isDead == true) {
			anim.SetBool("isDead",true);
			dead = true;
			StartCoroutine(WaitForDeathAnimation());
		}
	}
	
	
	public IEnumerator WaitForAnimation(GameObject enemy){
		while (true) {
			if (attacked){
				yield return new WaitForSeconds (2f);
				if(enemy.tag == "Enemy"){
					enemy.gameObject.GetComponent<Enemy> ().DealtDamage (attack.Attack);
				}
				if(enemy.tag == "RangedEnemy"){
					enemy.gameObject.GetComponent<EnemyRanged> ().DealtDamage (attack.Attack);
				}
			}
			yield return null;
		}
	}
	
	public IEnumerator WaitForDeathAnimation(){
		while (true) {
			if (dead){
				yield return new WaitForSeconds (5f);
				Destroy(attack.gameObject);
			}
			yield return null;
		}
	}
	
}
