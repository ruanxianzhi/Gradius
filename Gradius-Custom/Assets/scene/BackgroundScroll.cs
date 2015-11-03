using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {
	static public float speed = 2f;
	float camH;
	float camW;
	
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0, 0);
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		camH /= 2;
		camW /= 2;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnBecameInvisible(){
		Vector3 pos = this.gameObject.transform.position;
		pos.x = camW + 1f;
		pos.y = camH * Random.Range (-1f, 1f);
		this.gameObject.transform.position = pos;
	}
}
