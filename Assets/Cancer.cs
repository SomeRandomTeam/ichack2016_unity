using UnityEngine;
using System.Collections;

public class Cancer : MonoBehaviour {

	private const float updateFrequency = 1.0f;

	public bool mainScreen = false;
	public bool rightScreen = false;
	public bool leftScreen = false;

	int currentDisplay = 0;

	public string url;
	public int switchRequestId;

	private double nextGrabIn = 0.0f;

	private CardboardHead head;

	// Use this for initialization
	IEnumerator Start () {
		head = Camera.main.GetComponent<StereoController> ().Head;
		yield return StartCoroutine (GrabScreen ());
		getCurrentDisplay ();
		checkRightLeft ();
		yield break;
	}


	// Update is called once per frame
	void Update () {
		bool isLookedAt = IsLookedAt ();
		if (isLookedAt) {
			StartCoroutine (SwitchTo ());
			nextGrabIn -= Time.deltaTime;
			if (nextGrabIn <= 0.0f) {
				StartCoroutine (GrabScreen ());
				nextGrabIn = updateFrequency;
			}
		}

		getCurrentDisplay ();
		checkRightLeft ();

	}

	bool IsLookedAt() {
		RaycastHit hit;
		return GetComponent<Collider> ().Raycast (head.Gaze, out hit, Mathf.Infinity);
	}

	IEnumerator SwitchTo() {
		if (!mainScreen) {
			WWW nwww = new WWW (url + "switch/" + switchRequestId);
			yield return nwww;
		}
		yield break;
	}

	IEnumerator GrabScreen() {

		StartCoroutine (SwitchTo());

		getCurrentDisplay ();
		checkRightLeft ();

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

	IEnumerator getCurrentDisplay() {
		WWW disp = new WWW (url + "desktop");
		yield return disp;
		currentDisplay = int.Parse (disp.text);
	}

	void checkRightLeft() {
		getCurrentDisplay ();
		if (rightScreen){
			switchRequestId = (currentDisplay + 1) % 6; 

		} else if (leftScreen) {
			switchRequestId = (currentDisplay + 5) % 6; 
		}
	}
}
