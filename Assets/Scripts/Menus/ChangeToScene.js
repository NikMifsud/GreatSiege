#pragma strict

//attached to buttons to navigate between menus

function ChangeToScene(scene:String){
	
	Application.LoadLevel(scene);
	
}

function QuitApp(){	
	
	Application.Quit();
}

function Sound(){
	var audio1: AudioSource = Camera.main.GetComponent.<AudioSource>();
	audio1.Play();
//	audio1.Play();
}