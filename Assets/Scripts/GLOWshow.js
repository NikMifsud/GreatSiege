#pragma strict

var glowStElmo : GameObject;
var glowStAngelo : GameObject;
var glowStMichael : GameObject;
var blocked1 : GameObject;
var blocked2 : GameObject;
var blocked3 : GameObject;
var blocked4 : GameObject;
var blocked5 : GameObject;
var blocked6 : GameObject;
var blocked7 : GameObject;
var blocked8 : GameObject;
var blocked9 : GameObject;


function Start () {

	glowStElmo.SetActive(false);
	glowStElmo.SetActive(false);
	glowStMichael.SetActive(false);
	
	
	/*if(level1 passed){
		blocked1.SetActive(false);
	}*/
	
}

function Update () {

}

function HoverInElmo(){
	
	glowStElmo.SetActive(true);
}

function HoverInAngelo(){
	
	glowStAngelo.SetActive(true);
}

function HoverInMichael(){
	
	glowStMichael.SetActive(true);
}

function HoverOut(){
	
	glowStElmo.SetActive(false);
	glowStMichael.SetActive(false);
	glowStAngelo.SetActive(false);
}