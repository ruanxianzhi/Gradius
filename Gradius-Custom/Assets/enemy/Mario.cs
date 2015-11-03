using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {
	float camH,camW;
	bool stop;
	public GameObject win;

	// Use this for initialization
	void Start () {
		camH = PlayerController.camH;
		camW = PlayerController.camW;
		stop = false;
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (stop == true)
			return;
		if (BackgroundScroll.speed == 0 || transform.position.x < camW /3) {
			this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			stop = true;
		}

	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.tag == "Shot1" || coll.gameObject.tag == "Shot2") {
			if (PlayerController.invinMode == false) StartController.health -= 1;
			Application.LoadLevel("MainScene2");
		}
		if (coll.gameObject.tag == "Shield" || coll.gameObject.tag == "Player") {
			Destroy(GameObject.FindWithTag("Shield"));
			Destroy(this.gameObject);
			GameObject winm = Instantiate(win) as GameObject;
		}
	}
}
