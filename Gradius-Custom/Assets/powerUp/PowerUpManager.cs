using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {
	public GameObject PowerUp;
	float camH,camW;
	static int count = 0;


	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0, 0);
		camH = PlayerController.camH;
		camW = PlayerController.camW;
		count += 1;
		if (count == 15) {
			Instantiate(PowerUp);
			count = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PlayerController>().SwitchPower();
			Destroy(this.gameObject);
		}
	}
}
