using UnityEngine;
using System.Collections;

public class BackgroundBuilder : MonoBehaviour {
	public GameObject background;
	float camH;
	float camW;
	public int stars;

	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		camH /= 2;
		camW /= 2;
		GameObject star;
		for (int i = 0; i < stars; ++i) {
			star = Instantiate (background) as GameObject;
			star.transform.position = new Vector3(camW*Random.Range(-1f,1f), camH*Random.Range(-1f,1f),10);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
