using UnityEngine;
using System.Collections;

public class FlamethrowerCollider : MonoBehaviour {

	bool attacked;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.name);

			if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "AttackableEnemy") {
				other.gameObject.GetComponent<Enemy> ().DealtDamage (60);
			}
			if (other.gameObject.tag == "RangedEnemy" || other.gameObject.tag == "AttackableRangedEnemy") {
				other.gameObject.GetComponent<EnemyRanged> ().DealtDamage (60);
			}
	}
} 