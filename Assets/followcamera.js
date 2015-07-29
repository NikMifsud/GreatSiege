#pragma strict
var canvas : GameObject;
var target : Camera;
function Start () {

}

function Update () {
transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
} 