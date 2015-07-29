#pragma strict

var show2 : Animator;
//var show3 : Animator;
static var intNumber : int;

function Start(){
	
	intNumber = 1;
}

function Update () {

}

function SetNumberForward(values: int) {
     if(intNumber <3){
	     intNumber++;
	     values = intNumber;
	     show2.SetInteger("Values", intNumber);
	 }
 }
 
 function SetNumberBackward(values: int) {
     if(intNumber >1){
	     intNumber--;
	     values = intNumber;
	     show2.SetInteger("Values", intNumber);
	 }
 }