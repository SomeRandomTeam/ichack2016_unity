#pragma strict

var nextActionTime = 0.0f;
var period = 200.0f;
var url = "192.168.12.1:9999";
var screenID = 0;
var updateURL = "192.168.12.1:9999/switch/" + screenID;
var head;
var delay = 0.0f;
var startpos = Vector3(0.0f, 0.0f, 0.0f);

function Start () {
	incrementID();
}

function Update () {

}

function incrementID () {
	while (true) {
		if (screenID < 5){
			yield WaitForSeconds(3);
			screenID++;
			new WWW(updateURL);
   			var www = new WWW(url);
   			yield www;
		} else {
			yield;
		}
	}
}

function gaze() {
	

}
