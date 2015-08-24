#pragma strict
public var duration: float;
var myColor : Color;
var myColor2 : Color;
var transparent : Color;

var title : UnityEngine.UI.Text;
var myText1: UnityEngine.UI.Text;
var myText2: UnityEngine.UI.Text;
var myText3: UnityEngine.UI.Text;
var myText4: UnityEngine.UI.Text;
var myText5: UnityEngine.UI.Text;
var myText6: UnityEngine.UI.Text;
var myText7: UnityEngine.UI.Text;
var myText8: UnityEngine.UI.Text;
var myText9: UnityEngine.UI.Text;
var myText10: UnityEngine.UI.Text;
var myText11: UnityEngine.UI.Text;
var myText12: UnityEngine.UI.Text;
var myText13: UnityEngine.UI.Text;

var number1: UnityEngine.UI.Text;
var number2: UnityEngine.UI.Text;
var number3: UnityEngine.UI.Text;
var number4: UnityEngine.UI.Text;
var number5: UnityEngine.UI.Text;
var number6: UnityEngine.UI.Text;
var number7: UnityEngine.UI.Text;
var number8: UnityEngine.UI.Text;
var number9: UnityEngine.UI.Text;
var number10: UnityEngine.UI.Text;
var number11: UnityEngine.UI.Text;
var number12: UnityEngine.UI.Text;
var number13: UnityEngine.UI.Text;

var ratio : float;
var countdown1: float;
var pageNumber: int;


function Start () {
	pageNumber = 0 ;
	transparent.a = 0.0;
}

function Update () {
	
	if (pageNumber == 0){
		
		myText3.color = transparent;
		myText4.color = transparent;
		myText5.color = transparent;
		myText6.color = transparent;
		myText7.color = transparent;
		myText8.color = transparent;
		myText9.color = transparent;
		myText10.color = transparent;
		myText11.color = transparent;
		myText12.color = transparent;
		myText13.color = transparent;
		
		number3.color = transparent;
		number4.color = transparent;
		number5.color = transparent;
		number6.color = transparent;
		number7.color = transparent;
		number8.color = transparent;
		number9.color = transparent;
		number10.color = transparent;
		number11.color = transparent;
		number12.color = transparent;
		number13.color = transparent;
		
		countdown1 += Time.deltaTime;
		
		myColor = myText1.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText1.color = myColor;
		myText2.color = myColor;
		number1.color = myColor;
		number2.color = myColor;
		title.color = myColor;
		
	}
	
	if (pageNumber == 1){
		
		title.color = transparent;	
		myText1.color = transparent;
		myText2.color = transparent;
		myText5.color = transparent;
		myText6.color = transparent;
		
		number1.color = transparent;
		number2.color = transparent;
		number5.color = transparent;
		number6.color = transparent;
		
		countdown1 += Time.deltaTime;
		
		myColor = myText3.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText3.color = myColor;
		myText4.color = myColor;
		
		number3.color = myColor;
		number4.color = myColor;
	
	}
	
	if (pageNumber == 2){
		
		countdown1 += Time.deltaTime;
		
		myText3.color = transparent;
		myText4.color = transparent;
		myText7.color = transparent;
		myText8.color = transparent;
		
		myColor = myText5.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText5.color = myColor;
		myText6.color = myColor;
		
		number3.color = transparent;
		number4.color = transparent;
		number7.color = transparent;
		number8.color = transparent;
		number5.color = myColor;
		number6.color = myColor;
		
	}
	
	if (pageNumber == 3){
		
		countdown1 += Time.deltaTime;
		
		myText5.color = transparent;
		myText6.color = transparent;
		myText9.color = transparent;
		myText10.color = transparent;
		
		myColor = myText7.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText7.color = myColor;
		myText8.color = myColor;
		
		number5.color = transparent;
		number6.color = transparent;
		number9.color = transparent;
		number10.color = transparent;
		number7.color = myColor;
		number8.color = myColor;
		
	}
	
	if (pageNumber == 4){
		
		countdown1 += Time.deltaTime;
		
		myText7.color = transparent;
		myText8.color = transparent;
		myText11.color = transparent;
		myText12.color = transparent;
		
		myColor = myText9.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText9.color = myColor;
		myText10.color = myColor;
		
		number7.color = transparent;
		number8.color = transparent;
		number11.color = transparent;
		number12.color = transparent;
		number10.color = myColor;
		number9.color = myColor;
		
	}
	
	if (pageNumber == 5){
		
		countdown1 += Time.deltaTime;
		
		myText9.color = transparent;
		myText10.color = transparent;
		myText13.color = transparent;
		
		myColor = myText11.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText11.color = myColor;
		myText12.color = myColor;
		
		number9.color = transparent;
		number10.color = transparent;
		number13.color = transparent;
		number11.color = myColor;
		number12.color = myColor;
		
	}
	
	if (pageNumber == 6){
		
		countdown1 += Time.deltaTime;
		
		myText11.color = transparent;
		myText12.color = transparent;
				
		myColor = myText11.color;
		ratio = countdown1/duration;
		myColor.a = Mathf.Lerp(0,1, ratio);
		myText13.color = myColor;
		
		number11.color = transparent;
		number12.color = transparent;
		number13.color = myColor;
		
	}
	
}

function NextPages(time:float){
	if(pageNumber <= 5){
		pageNumber++;
		countdown1 = time;
	}
}

function PreviousPages(time:float){
	
	if(pageNumber >= 1){
		pageNumber--;
		countdown1 = time;
	}
	
}
