using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {
	public Vector3 speed = new Vector3(20f, 0, 0);
	public GameObject go;
	float camH,camW;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = speed;
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = go.transform.position.y;
		transform.position = pos;
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 >= camW / 2) {
			Destroy(this.gameObject);
		}
	}
}
