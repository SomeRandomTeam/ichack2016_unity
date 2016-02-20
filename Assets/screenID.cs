using UnityEngine;
using System.Collections;

public class screenID : MonoBehaviour {

	private int id;
	private string url= "http://192.168.12.1:9999/";

	public screenID(int id) {
		this.id = id;
	}

	public int getScreenId() {
		return this.id;
	}

	public IEnumerator updateScreen() {
		new WWW (url + "/switch/" + id);
		WWW image = new WWW (url);
		yield return image;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = image.texture;
	}
}
