using UnityEngine;
using System.Collections;

public class vocanoshots : MonoBehaviour {
	public float speed;
	public GameObject shiprigidbody;
	float camH,camW;
	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		//false->left
		//true->right

	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y / 2 <= -5*camH/12) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x / 2 >= camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 >= camH/2) {
			Destroy(this.gameObject);
			return;
		}

	}
	void OnTriggerEnter(Collider coll){

		if (coll.gameObject.tag == "Player" ||coll.gameObject.tag == "PlayerWeapon" ) {
			Destroy(this.gameObject);
		}
	
	}
	void OnDestroy(){
		
	}
}
