#pragma strict

public static var Volume:float;
var MuteButton : GameObject;
var SoundButton : GameObject;
var PlayButton : GameObject;
var PauseButton : GameObject;
var PauseMenu	: GameObject;

function Start (){	
	MuteButton.SetActive(true);
	SoundButton.SetActive(false);
	PauseMenu.SetActive(false);
}

function Awake(){	
	DontDestroyOnLoad(this.gameObject);
}

function Mute(){	
	AudioListener.volume = 0.0;
	MuteButton.SetActive(false);
	SoundButton.SetActive(true);
}

function SoundPlay(){	
	AudioListener.volume = 1.0;
	SoundButton.SetActive(false);
	MuteButton.SetActive(true);
}

function PauseGameButton(){	
	PauseButton.SetActive(false);
	PlayButton.SetActive(true);
	PauseMenu.SetActive(true);
}

function ResumePlayButton(){	
	PlayButton.SetActive(false);
	PauseButton.SetActive(true);
	PauseMenu.SetActive(false);
}