using UnityEngine;
using System.Collections;

public class StreamCSharp : MonoBehaviour {

	public string url = "http://mishrabhinav.com/assets/img/mymos.jpg";

	// Use this for initialization
	IEnumerator Start () {

		while (true) {
			WWW www = new WWW (url);
			yield return www;
			Renderer renderer = GetComponent<Renderer> ();
			renderer.material.mainTexture = www.texture;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Cardboard.SDK.Triggered) {
			url = "http://mishrabhinav.com/assets/img/avatar.jpg";
		}

	}
}
