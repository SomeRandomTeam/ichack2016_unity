using UnityEngine;
using System.Collections;

public class StreamCSharp : MonoBehaviour {

	public string url = "http://192.168.12.1:9999";

	// Use this for initialization
	public void Start () {

	}
	
	// Update is called once per frame
	public IEnumerable Update () {
		WWW www = new WWW (url);
		yield return www;
		Renderer renderer = GetComponent<Renderer> ();
		renderer.material.mainTexture = www.texture;
	}
}
