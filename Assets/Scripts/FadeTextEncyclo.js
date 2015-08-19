#pragma strict
public var duration: float;
var myColor : Color;
var myColor2 : Color;

var title : UnityEngine.UI.Text;
var myText1: UnityEngine.UI.Text;
var myText2: UnityEngine.UI.Text;
var ratio : float;
var countdown1: float;
var pageNumber: int;

function Start () {
	pageNumber = 0 ;
}

function Update () {
	
	if (pageNumber == 1){
		
		countdown1 += Time.deltaTime;
		
		myColor = myText1.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(1,0, ratio);
		myText1.color = myColor;
		myText2.color = myColor;
		title.color = myColor;
		
		
		
	}
	
}

function NextPages(time:float){
	
	pageNumber++;
	countdown1 = time;
}

