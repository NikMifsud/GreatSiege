using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextMapCharUI : MonoBehaviour {

	public Text HealthText;
	public Text AttackText;
	public Text CDText;
	public Text MovementText;
	public Text RangeText;
	public PlayerControl playerControl;
	public HoverGUI hoverGUI;

	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (hoverGUI.isOnFootUnit == true) {
			HealthText.text = "100";
			AttackText.text = "50";
			CDText.text = "10";
			MovementText.text = "2";
			RangeText.text = "1";
		}

		else if (hoverGUI.isOnRangedUnit == true) {
			HealthText.text = "100";
			AttackText.text = "30";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "2";
		}

		else if (hoverGUI.isOnSiegeUnit == true) {
			HealthText.text = "300";
			AttackText.text = "90";
			CDText.text = "30";
			MovementText.text = "0";
			RangeText.text = "20";
		}
		else if (hoverGUI.isOnMusket == true) {
			HealthText.text = "100";
			AttackText.text = "40";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "3";
		}
		else if (hoverGUI.isOnPike == true) {
			HealthText.text = "100";
			AttackText.text = "45";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "2";
		}


		else {
			if (playerControl.selectedCharacter != null) {
				if (playerControl.selectedCharacter.tag == "FootUnit" || playerControl.selectedCharacter.tag == "SelectedFootUnit") {
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().AttackRange.ToString ();
				}

				if (playerControl.selectedCharacter.tag == "RangedUnit" || playerControl.selectedCharacter.tag == "SelectedRangedUnit") {
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange.ToString ();
				}
				if (playerControl.selectedCharacter.tag == "SiegeUnit" || playerControl.selectedCharacter.tag == "SelectedSiegeUnit") {
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().AttackRange.ToString ();
				}
				
				if (playerControl.selectedCharacter.tag == "PikeUnit" || playerControl.selectedCharacter.tag == "SelectedPikeUnit") {
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().AttackRange.ToString ();
				}
			}
		}

	}
}

