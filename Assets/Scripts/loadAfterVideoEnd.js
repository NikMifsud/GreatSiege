#pragma strict

function Start () {

	if(Application.loadedLevelName == "Cutscene1"){
		
		yield WaitForSeconds(49);
		Application.LoadLevel("Level1Knights");
	}
}

function Update () {

}

function loadAtEnd(){
	
}