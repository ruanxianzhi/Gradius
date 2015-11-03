using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cavebehavior : MonoBehaviour {
	public float speed;
	public GameObject vocanoshots;
	public int points = 300;
	float camH,camW;
	public float spawnTime=0.2f;
	float respawn;
	float totaltime;
	bool side;
	int side2 = 1;
	bool eject;
	bool force;
	GameObject shiprigidbody;

	// Use this for initialization
	void Start () {
		totaltime = 2f;
		speed = BackgroundScroll.speed;
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0f, 0f);
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		respawn = spawnTime;
		side = false;
		eject = true;
		force = false;
		shiprigidbody = GameObject.Find("Player");
		if (this.gameObject.transform.position.y > shiprigidbody.transform.position.y) {
			side2 = -1;
			transform.Rotate (0,0,180);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (eject) {
			totaltime -= Time.deltaTime;
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = spawnTime;
			GameObject obj = Instantiate (vocanoshots) as GameObject;
			obj.transform.position = this.gameObject.transform.position;
			float yspeed = Random.value * 26f * side2;
			obj.GetComponent<Rigidbody> ().velocity = new Vector3 (0, yspeed, 0);
			side = !side;
			
		}
		if (force) {
			shiprigidbody.GetComponent<Rigidbody>().AddForce((this.gameObject.transform.position - shiprigidbody.transform.position) * 1000f * Time.smoothDeltaTime);
		}
		if (totaltime < 0) {
			force = true;
			eject = false;
		}

	}
}
