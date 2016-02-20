using UnityEngine;
using System.Collections;

public class StreamCSharp : MonoBehaviour {

	public string url = "http://127.0.0.1:9999";

	// Use this for initialization
	IEnumerator Start () {
		yield break;
	}

	IEnumerator UpdateScreengrab() {
		WWW www = new WWW (url);
		yield return www;
		Renderer renderer = GetComponent<Renderer> ();
		Texture2D tex = null;
		try {
			tex = www.texture;
		} catch {
			Debug.Log ("Failed to load image");
		}
		if (tex != null) {
			renderer.material.mainTexture = tex;
		}
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine (UpdateScreengrab ());
	}
}
