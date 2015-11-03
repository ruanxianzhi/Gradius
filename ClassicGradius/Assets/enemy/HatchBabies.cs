using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HatchBabies : MonoBehaviour {
	public float speed;
	bool frombottom;
	public float heightbound;
	bool getout=false;
	bool passed = false;
	public GameObject shiprigidbody;
	float camH,camW;

	// Use this for initialization
	void Start () {
		speed = 4.5f;
		float hbound =0f;
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		frombottom = true;
		if (HatcherE.counter % 3 == 0) {
			hbound = 1 * camH / 7;
		} else if (HatcherE.counter % 3 == 1) {
			hbound = 0;
		} else {
			hbound = -1*camH/7;
		}
		HatcherE.counter++;
		if (this.gameObject.transform.position.y < 0) {
			this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, speed, 0);
			heightbound = -hbound;
		}
		else {
			frombottom = false;
			this.GetComponent<Rigidbody> ().velocity =new Vector3 (-BackgroundScroll.speed, -speed, 0) ;
			heightbound = hbound;
		}
		shiprigidbody = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 >= camH / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y / 2 <= -camH/2) {
			Destroy(this.gameObject);
			return;
		}
				if (!getout) {
			if (frombottom) {
				if (this.gameObject.transform.position.y >= heightbound) {
					this.GetComponent<Rigidbody> ().velocity = speed*Vector3.up;
					getout = true;
				}
				
			} else if (this.gameObject.transform.position.y <= heightbound) {
				this.GetComponent<Rigidbody> ().velocity = speed*Vector3.down;
				getout = true;
			}
		} else {
			this.gameObject.transform.LookAt (shiprigidbody.transform);
			this.GetComponent<Rigidbody> ().velocity = speed*Vector3.left;
		}

	}
	
	void OnTriggerEnter(Collider coll){
		
		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
		else if (coll.gameObject.tag == "PlayerWeapon") {
			//Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
	void OnDestroy(){
		
	}
}
