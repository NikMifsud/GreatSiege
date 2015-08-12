#pragma strict

var cutscene1 : MovieTexture;
//var sound1 : AudioClip;

function Start () {
	
	GetComponent.<Renderer>().material.mainTexture = cutscene1;
	//sound1 = cutscene1.audioClip;
	cutscene1.Play();
	//sound1.isReadyToPlay();
}

function Update () {

}