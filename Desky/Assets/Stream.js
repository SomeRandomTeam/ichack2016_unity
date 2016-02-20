﻿#pragma strict

var url = "http://secondapps.com/countdown/t1456812000z0.png";

function Start () {
	// Create a texture in DXT1 format
	GetComponent.<Renderer>().material.mainTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
	while(true) {
		// Start a download of the given URL
		var www = new WWW(url);

		// wait until the download is done
		yield www;

		// assign the downloaded image to the main texture of the object
		www.LoadImageIntoTexture(GetComponent.<Renderer>().material.mainTexture);
	}
}

function Update () {

	url = "http://secondapps.com/countdown/t1456812000z0.png";
}