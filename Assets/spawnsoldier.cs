using UnityEngine;
using System.Collections;

public class spawnsoldier : MonoBehaviour {
	public bool SpawnSoldier;
	public GameObject Soldier;
	public economy Food;
	public happiness HappyUnits;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SpawnSoldier == true && Input.GetMouseButtonDown(0)){
				Debug.Log("Mouse button is pressed");
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray,out hit))
				{
					if(hit.collider.gameObject.tag=="DirtTile"){
					var cubeTemp = hit.collider.transform.position;
						Debug.Log("Clicked a tile");
					if(Soldier.gameObject.tag == "Footsoldier" && Food.Food >= 30){
						Instantiate(Soldier,cubeTemp,Quaternion.identity);
						Food.Food = Food.Food - 30;
						HappyUnits.HappyUnits = HappyUnits.HappyUnits - 10;
						SpawnSoldier = false;
					}
					if(Soldier.gameObject.tag == "Ranged" && Food.Food >= 50){
						Instantiate(Soldier,cubeTemp,Quaternion.identity);
						Food.Food = Food.Food - 50;
						SpawnSoldier = false;
					}
					if(Soldier.gameObject.tag == "Cannon" && Food.Food >= 80){
						Instantiate(Soldier,cubeTemp,Quaternion.identity);
						Food.Food = Food.Food - 80;
						SpawnSoldier = false;
					}
					}else{
						Debug.Log("Didn't click a tile");
						SpawnSoldier = false;
					}
				}
			}
	}

	public void ButtonClicked () {
		SpawnSoldier = true;
	}
}
