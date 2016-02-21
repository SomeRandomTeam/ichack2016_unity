using UnityEngine;
using System.Collections;

public class CameraCancer : MonoBehaviour {

	bool near = true;
	// Use this for initialization
	void Start () {
	
	}



	// Update is called once per frame
	void Update () {
		if (Cardboard.SDK.Triggered && near) {
			this.transform.position = new Vector3 (-110	, 0, 0);
			near = !near;
		} else if (Cardboard.SDK.Triggered) {
			this.transform.position = new Vector3 (-20, 5, 0);
			near = !near;
		}
	}
}
