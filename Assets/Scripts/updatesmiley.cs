using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updatesmiley : MonoBehaviour {
	public happiness HappyUnits;
	public int Growth;
	public GameObject happy1; 
	public GameObject happy2; 
	public GameObject happy3; 
	public GameObject happy4; 
	public GameObject happy5; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HappyUnits.HappyUnits >= 81){
			happy1.SetActive(false);
			happy2.SetActive(false);
			happy3.SetActive(false);
			happy4.SetActive(false);
			happy5.SetActive(true);
			Growth = -1;
		}
		if ((HappyUnits.HappyUnits <= 80) && (HappyUnits.HappyUnits >=61)){
			happy1.SetActive(false);
			happy2.SetActive(false);
			happy3.SetActive(false);
			happy4.SetActive(true);
			happy5.SetActive(false);
			Growth = 0;
		}
		if ((HappyUnits.HappyUnits <= 60) && (HappyUnits.HappyUnits >=41)){
			happy1.SetActive(false);
			happy2.SetActive(false);
			happy3.SetActive(true);
			happy4.SetActive(false);
			happy5.SetActive(false);
			Growth = 1;
		}
		if ((HappyUnits.HappyUnits <= 40) && (HappyUnits.HappyUnits >=21)){
			happy1.SetActive(false);
			happy2.SetActive(true);
			happy3.SetActive(false);
			happy4.SetActive(false);
			happy5.SetActive(false);
			Growth = 2;
		}
		if (HappyUnits.HappyUnits <= 20){
			happy1.SetActive(true);
			happy2.SetActive(false);
			happy3.SetActive(false);
			happy4.SetActive(false);
			happy5.SetActive(false);
			Growth = 3;
		}
	}
}