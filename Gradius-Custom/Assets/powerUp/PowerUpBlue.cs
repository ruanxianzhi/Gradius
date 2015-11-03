using UnityEngine;
using System.Collections;

public class PowerUpBlue : MonoBehaviour {
	public float speed = 2f;
	float camH,camW;
	GameObject[] basic;

	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0, 0);
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-speed, 0f, 0f);
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
	}
	
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			basic = GameObject.FindGameObjectsWithTag("Basic");
			foreach (GameObject b in basic) {
				Destroy(b);
			}
			Destroy(this.gameObject);
		}
	}
}
