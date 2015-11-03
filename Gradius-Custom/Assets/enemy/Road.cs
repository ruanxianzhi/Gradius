using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Road : MonoBehaviour {
	public List <GameObject> roadbaby;
	float camH,camW;
	public float spawnTime=1f;
	float respawn;
	static bool movestate;
	bool eject;
	int size;
	GameObject shiprigidbody;

	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		shiprigidbody = GameObject.Find("Player");
		respawn = spawnTime;
		movestate = true;
		eject = true;
		size = roadbaby.Count;
		print (camW);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		if (transform.position.x - transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (eject == true && transform.position.x < -camW / 4) {
			BackgroundScroll.speed = 0;
		}
		if (shiprigidbody.transform.position.x > (camW/3-1f)) {
			PlayerController.passRoad = true;
			eject = false;
			BackgroundScroll.speed = 2f;
		}
		if (eject) {
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = spawnTime;
			int random = (int)(Random.value*size);
			GameObject obj =Instantiate (roadbaby[random]) as GameObject;
			Vector3 pos = this.gameObject.transform.position;
			pos.z = 0f;
			pos.x += 3f*random;
			obj.transform.position =  pos;

		}


	}
}
