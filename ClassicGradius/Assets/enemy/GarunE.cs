using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GarunE : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public GameObject shiprigidbody;
	Rigidbody enemyBod;
	Text score;
	bool passed = false;
	public int points = 100;
	float camH,camW;
	public GameObject PowerUp;
	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
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
		if (passed)
			return;
		this.gameObject.transform.LookAt (shiprigidbody.transform);
		if (shiprigidbody.transform.position.x >= this.gameObject.transform.position.x) {
			passed = true;
			this.GetComponent<Rigidbody> ().velocity =new Vector3(-2*speed,0f,0f);
		}
		this.gameObject.transform.Translate (speed * Vector3.forward * Time.deltaTime);

		
	}
	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
		/*if (Random.value < dropChance) {
			GameObject power = Instantiate(PowerUp) as GameObject;
			power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
		}
		*/
	}
	void OnTriggerEnter(Collider coll){
		
		if (coll.gameObject.tag == "Player") {
			Destroy(this.gameObject);
		}
		else if (coll.gameObject.tag == "PlayerWeapon") {
			Scored();
			//Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
	void OnDestroy(){
		
	}
}
