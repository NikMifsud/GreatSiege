#pragma strict

var image1: GameObject;
var image2: GameObject;
var image3: GameObject;

function Update () {

	if(PlayMenuNav.intNumber == 1){
		image1.SetActive(true);
		image2.SetActive(false);
		image3.SetActive(false);
	}
	
	if(PlayMenuNav.intNumber == 2){
		
		image1.SetActive(false);
		image2.SetActive(true);
		image3.SetActive(false);
	}
	
	if(PlayMenuNav.intNumber == 3){
		
		image1.SetActive(false);
		image2.SetActive(false);
		image3.SetActive(true);
	}
}