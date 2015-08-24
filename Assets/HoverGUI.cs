using UnityEngine;
using System.Collections;

public class HoverGUI : MonoBehaviour {

	public bool isOnFootUnit;
	public bool isOnRangedUnit;
	public bool isOnSiegeUnit;
	public bool isOnPike;
	public bool isOnMusket;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetBoolFoot() {
		isOnFootUnit = true;
		isOnRangedUnit = false;
		isOnSiegeUnit = false;
	}

	public void SetBoolRanged() {
		isOnRangedUnit = true;
		isOnFootUnit = false;
		isOnSiegeUnit = false;
	}

	public void SetBoolMusket() {
		isOnSiegeUnit = false;
		isOnFootUnit = false;
		isOnRangedUnit = false;
		isOnMusket = true;
		isOnPike = false;
	}

	public void SetBoolPike() {
		isOnSiegeUnit = false;
		isOnFootUnit = false;
		isOnRangedUnit = false;
		isOnMusket = false;
		isOnPike = true;
	}

	public void SetBoolSiege() {
		isOnSiegeUnit = true;
		isOnFootUnit = false;
		isOnRangedUnit = false;
		isOnMusket = false;
		isOnPike = false;
	}
}
