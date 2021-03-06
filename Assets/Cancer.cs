﻿using UnityEngine;
using System.Collections;

public class Cancer : MonoBehaviour {

	private const float updateFrequency = 0.33f;

	public bool mainScreen = false;
	public bool rightScreen = false;
	public bool leftScreen = false;

	int currentDisplay = 0;

	public int switchRequestId;

	private double nextGrabIn = 0.0f;

	private CardboardHead head;



	// Use this for initialization
	IEnumerator Start () {
		head = Camera.main.GetComponent<StereoController> ().Head;
		yield return StartCoroutine (GrabScreen ());
		yield return StartCoroutine (checkRightLeft ());
		yield break;
	}


	// Update is called once per frame
	void Update () {
		bool isLookedAt = IsLookedAt ();
		if (isLookedAt) {
			nextGrabIn -= Time.deltaTime;
			if (nextGrabIn <= 0.0f) {
				StartCoroutine (GrabScreen ());
				nextGrabIn = updateFrequency;
			}
		}
		checkRightLeft ();
	}

	bool IsLookedAt() {
		RaycastHit hit;
		return GetComponent<Collider> ().Raycast (head.Gaze, out hit, Mathf.Infinity);
	}

	IEnumerator SwitchTo() {
		if (!mainScreen && BroadcastService.GetAddress() != null) {
			WWW nwww = new WWW ("http://" + BroadcastService.GetAddress ().ToString() + ":9999" + "/switch/" + switchRequestId);
			yield return nwww;
		}
		yield break;
	}

	IEnumerator GrabScreen() {

		yield return StartCoroutine (SwitchTo());

		yield return StartCoroutine(checkRightLeft ());

		if (BroadcastService.GetAddress () != null) {
			WWW www = new WWW ("http://" + BroadcastService.GetAddress ().ToString() + ":9999");
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
		}

		nextGrabIn = Time.time + 1.0d;
		yield break;
	}

	IEnumerator getCurrentDisplay() {
		if (BroadcastService.GetAddress () != null) {
			WWW disp = new WWW ("http://" + BroadcastService.GetAddress ().ToString () + ":9999" + "/desktop");
			yield return disp;
			currentDisplay = int.Parse (disp.text);
		}
		yield break;
	}

	IEnumerator checkRightLeft() {
		yield return StartCoroutine (getCurrentDisplay ());
		if (rightScreen){
			switchRequestId = (currentDisplay + 1) % 6; 

		} else if (leftScreen) {
			switchRequestId = (currentDisplay + 5) % 6; 
		}
		yield break;
	}
}
