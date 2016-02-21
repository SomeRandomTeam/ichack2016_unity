using UnityEngine;
using System.Collections;

public class CameraCancer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered) {
			this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(-100,0,0), 100);
		}
	}
}
