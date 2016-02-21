#pragma strict

	var url = "192.168.12.1:9999";

	var screenID = 0;
	var updateURL = "192.168.12.1:9999/switch/" + screenID;
	//var head;
	//var startpos;



	function Start () {
		initialiseScreen();
		//head = Camera.main.GetComponent.<StereoController>().Head;
	}

function initialiseScreen () {

		GetComponent.<Renderer>().material.mainTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		while(true) {

			if(screenID == transform.parent.GetComponent(ScreenID).screenID) {
				// Start a download of the given URL
				var www = new WWW(url);

				// wait until the download is done
				yield www;

				// assign the downloaded image to the main texture of the object
				www.LoadImageIntoTexture(GetComponent.<Renderer>().material.mainTexture);

			} else {
			yield;
			}

		}

}


function Update () {
   //loadScreen();
}


function screenchange() {

	transform.parent.GetComponent(ScreenID).screenID = screenID;

}
