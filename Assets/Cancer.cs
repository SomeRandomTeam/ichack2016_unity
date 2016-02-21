using UnityEngine;
using System.Collections;

public class Cancer : MonoBehaviour {

	private const float updateFrequency = 1.0f;

	public bool mainScreen = false;

	public string url;
	public string switchRequestUrl;

	private double nextGrabIn = 0.0f;

	private CardboardHead head;

	// Use this for initialization
	IEnumerator Start () {
		head = Camera.main.GetComponent<StereoController> ().Head;
		yield return StartCoroutine (GrabScreen ());
		yield break;
	}


	// Update is called once per frame
	void Update () {
		bool isLookedAt = IsLookedAt ();
		//Debug.Log (isLookedAt);
		if (isLookedAt) {
			StartCoroutine (SwitchTo ());
			// First detect trigger then magnify
			if (Cardboard.SDK.Triggered) {
				Magnify ();
			}

			//Then look for time passed and refresh
			nextGrabIn -= Time.deltaTime;
			if (nextGrabIn <= 0.0f) {
				StartCoroutine (GrabScreen ());
				nextGrabIn = updateFrequency;
			}
		}
	}

	bool IsLookedAt() {
		RaycastHit hit;
		return GetComponent<Collider> ().Raycast (head.Gaze, out hit, Mathf.Infinity);
	}

	void Magnify() {
		Camera.main.transform.position = new Vector3 (-110, 0, 0);
	}

	IEnumerator SwitchTo() {
		WWW nwww = new WWW (switchRequestUrl);
		yield return nwww;
		yield break;
	}

	IEnumerator GrabScreen() {
		WWW www = new WWW (url);
		yield return www;

		try {
			Texture2D tex = www.texture;
			Renderer renderer = GetComponent<Renderer> ();
			if (tex != null) {
				renderer.material.mainTexture = tex;
			}
		} catch {
			Debug.Log ("Failed to load image");
		}

		nextGrabIn = Time.time + 1.0d;
		yield break;
	}
}
