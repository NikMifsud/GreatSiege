using UnityEngine;
using System.Collections;

public class MoveBullets : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "AttackableEnemy" || other.gameObject.tag == "RangedEnemy" || other.gameObject.tag == "AttackableRangedEnemy" || other.gameObject.tag == "EnemySiege" || other.gameObject.tag == "AttackableEnemySiege") {
			this.gameObject.transform.position = new Vector3(100,100,100);
			Debug.Log ("moved");
		}
	}
}
