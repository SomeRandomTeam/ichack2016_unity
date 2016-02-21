using UnityEngine;
using System.Collections;

public class Cancer : MonoBehaviour {

	public string url = "http://192.168.12.1:9999/";
	public string newRequest = "http://192.168.12.1:9999/switch/0";
	private bool firstLoad = true;
	private double delay = 0.0d;

	private CardboardHead head;

	// Use this for initialization
	IEnumerator Start () {
		head = Camera.main.GetComponent<StereoController> ().Head;

		yield break;

	}


	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider> ().Raycast (head.Gaze, out hit, Mathf.Infinity);
		//Debug.Log (isLookedAt);
		if (firstLoad) {
			firstLoad = false;
			StartCoroutine (display ());
		} else if (Time.time > delay && isLookedAt) {
			delay = Time.time + 2.0d;
			StartCoroutine (display ());
		}
	}

	IEnumerator display() {
		WWW nwww = new WWW (newRequest);
		yield return nwww;
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
		delay = Time.time + 2.0d;

	}


}
