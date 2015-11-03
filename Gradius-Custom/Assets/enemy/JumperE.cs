using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumperE : MonoBehaviour {
	public float speed;
	public GameObject EWeapon;
	Rigidbody enemyBod;
	Text score;
	public int points = 100;
	float camH,camW;
	public GameObject PowerUp;
	public int count;
	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		count = 1;

	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 >= camW / 2) {
			Destroy (this.gameObject);
			return;
		}	

	}
	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();			
	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "PlayerWeapon") {
			GameObject power = Instantiate (PowerUp) as GameObject;
			power.GetComponent<Rigidbody> ().transform.position = this.gameObject.transform.position;
			Scored ();
			//Destroy(coll.gameObject);
			Destroy (this.gameObject);
		} 
		else if (coll.gameObject.tag == "Floor") {
			Vector3 tmpcopy;
			tmpcopy = enemyBod.velocity;
			tmpcopy.y = -tmpcopy.y;
			if (count == 2){

				tmpcopy.x = -tmpcopy.x;

				if (Random.value > 0.5) {
					enemyBod.velocity = new Vector3 (0f, 0f, 0f);
					Instantiate (EWeapon, this.gameObject.transform.position, this.gameObject.transform.rotation);
				}
				enemyBod.velocity = tmpcopy;
			}
			else{

				if (Random.value > 0.8) {
					enemyBod.velocity = new Vector3 (0f, 0f, 0f);
					Instantiate (EWeapon, this.gameObject.transform.position, this.gameObject.transform.rotation);
				}
				enemyBod.velocity = tmpcopy;
			
			}
			count++;
		}
	}
	void OnDestroy(){
			
	}


	
}
