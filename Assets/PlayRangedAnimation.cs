using UnityEngine;
using System.Collections;

public class PlayRangedAnimation : MonoBehaviour {
	
	public Animator anim;
	public Animation attackAnim;
	public Rangedsoldier attack;
	public bool attacked = false;
	public bool dead = false;
	public AudioClip bowShotEffect,deathEffect;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = Camera.main.GetComponent<AudioSource> ();
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
				if(enemy.tag == "Enemy" || enemy.tag == "AttackableEnemy"){
					source.PlayOneShot(bowShotEffect,1.0f);
					enemy.gameObject.GetComponent<disablinghp> ().JustHit = true;
					enemy.gameObject.GetComponent<Enemy> ().isBeingAttacked = false;
					enemy.gameObject.GetComponent<Enemy> ().DealtDamage (attack.Attack);
					attacked = false;
					attack.attack = false;
				}
				if(enemy.tag == "RangedEnemy"|| enemy.tag == "AttackableRangedEnemy"){
					source.PlayOneShot(bowShotEffect,1.0f);
					enemy.gameObject.GetComponent<disablinghp> ().JustHit = true;
					enemy.gameObject.GetComponent<EnemyRanged> ().isBeingAttacked = false;
					enemy.gameObject.GetComponent<EnemyRanged> ().DealtDamage (attack.Attack);
					attacked = false;
					attack.attack = false;
				}
				if(enemy.tag == "EnemySiege"|| enemy.tag == "AttackableEnemySiege"){
					source.PlayOneShot(bowShotEffect,1.0f);
					enemy.gameObject.GetComponent<disablinghp> ().JustHit = true;
					enemy.gameObject.GetComponent<EnemyCannon> ().isBeingAttacked = false;
					enemy.gameObject.GetComponent<EnemyCannon> ().DealtDamage (attack.Attack);
					attacked = false;
					attack.attack = false;
				}
			}
			yield return null;
		}
	}
	
	public IEnumerator WaitForDeathAnimation(){

		while (true) {
			if (dead){
				source.PlayOneShot(deathEffect,1.0f);
				attack.gameObject.GetComponent<disablinghp>().JustHit = true;
				yield return new WaitForSeconds (4f);
				Destroy(attack.gameObject);
			}
			yield return null;
		}
	}
	
}
