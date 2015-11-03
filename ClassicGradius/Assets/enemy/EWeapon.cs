using UnityEngine;
using System.Collections;

public class EWeapon : MonoBehaviour {
	public float speed = 7f;
	public GameObject shiprigidbody;
	float camH,camW;

	// Use this for initialization
	void Start () {
		shiprigidbody = GameObject.Find("Player");	
		var heading = shiprigidbody.gameObject.transform.position - this.gameObject.transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		var tmp = speed * direction;
		//tmp.x +=BackgroundScroll.speed;
		this.GetComponent<Rigidbody> ().velocity =  tmp;
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
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

	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
	}
}
