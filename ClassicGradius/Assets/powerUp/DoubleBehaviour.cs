﻿using UnityEngine;
using System.Collections;

public class DoubleBehaviour : MonoBehaviour {
	public Vector3 speed = new Vector3(14f, 14f, 0);
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
		if (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x / 2 >= camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 >= camH / 2) {
			Destroy(this.gameObject);
			return;
		}
	
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Basic" || coll.gameObject.tag == "Enemy") {
			Destroy(this.gameObject);
		}
	}
}
