using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextMapCharUI : MonoBehaviour {

	public Text HealthText;
	public Text AttackText;
	public Text CDText;
	public Text MovementText;
	public Text RangeText;
	public Text Defence;
	public PlayerControl playerControl;
	public HoverGUI hoverGUI;
	public Image image;
	public Sprite imageFoot;
	public Sprite imageRanged;
	public Sprite imagePike;
	public Sprite imageMusket;

	// Use this for initialization
	void Start () {
		playerControl = Camera.main.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (hoverGUI.isOnFootUnit == true) {
			image.sprite = imageFoot;
			HealthText.text = "100";
			AttackText.text = "50";
			Defence.text = "10";
			CDText.text = "10";
			MovementText.text = "2";
			RangeText.text = "1";
		}

		else if (hoverGUI.isOnRangedUnit == true) {
			image.sprite = imageRanged;
			HealthText.text = "100";
			AttackText.text = "30";
			Defence.text = "0";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "2";
		}

		else if (hoverGUI.isOnSiegeUnit == true) {
		//	image.sprite = imageMusket;
			HealthText.text = "300";
			AttackText.text = "90";
			CDText.text = "30";
			Defence.text = "50";
			MovementText.text = "0";
			RangeText.text = "20";
		}
		else if (hoverGUI.isOnMusket == true) {
			image.sprite = imageMusket;
			HealthText.text = "100";
			AttackText.text = "40";
			Defence.text = "0";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "3";
		}
		else if (hoverGUI.isOnPike == true) {
			image.sprite = imagePike;
			HealthText.text = "100";
			AttackText.text = "45";
			Defence.text = "10";
			CDText.text = "15";
			MovementText.text = "2";
			RangeText.text = "2";
		}


		else {
			if (playerControl.selectedCharacter != null) {
				if (playerControl.selectedCharacter.tag == "FootUnit" || playerControl.selectedCharacter.tag == "SelectedFootUnit") {
					image.sprite = imageFoot;
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Attack.ToString ();
					Defence.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Armor.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Footsoldier> ().AttackRange.ToString ();
				}

				if (playerControl.selectedCharacter.tag == "RangedUnit" || playerControl.selectedCharacter.tag == "SelectedRangedUnit") {
					image.sprite = imageRanged;
					Defence.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Armor.ToString ();
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Rangedsoldier> ().AttackRange.ToString ();
				}
				if (playerControl.selectedCharacter.tag == "SiegeUnit" || playerControl.selectedCharacter.tag == "SelectedSiegeUnit") {
					image.sprite = imageMusket;
					Defence.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Armor.ToString ();
					HealthText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().curr_Health.ToString ();
					AttackText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Attack.ToString ();
					CDText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Movementtime.ToString ();
					MovementText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().Movement.ToString ();
					RangeText.text = (string)playerControl.selectedCharacter.GetComponent<Musket> ().AttackRange.ToString ();
				}
				
				if (playerControl.selectedCharacter.tag == "PikeUnit" || playerControl.selectedCharacter.tag == "SelectedPikeUnit") {
					image.sprite = imagePike;
					Defence.text = (string)playerControl.selectedCharacter.GetComponent<Pike> ().Armor.ToString ();
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

