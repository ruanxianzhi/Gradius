using UnityEngine;
using System.Collections;

public class BossShot : MonoBehaviour {
	public Vector3 speed = new Vector3(-8.5f, 0, 0);
	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -PlayerController.camW / 2)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
	}
}
