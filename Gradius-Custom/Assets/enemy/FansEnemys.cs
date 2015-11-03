using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FansEnemys : MonoBehaviour {

	public float speed;
	public GameObject shiprigidbody;
	Rigidbody enemyBod;
	enum movementState { forward,back,rotate};
	movementState curr = movementState.forward;
	Text score;
	public int points = 100;
	float camH,camW;
	enum occurplace {up,down};
	//change the location that it appears
	occurplace startfrom =occurplace.up;
	public GameObject PowerUp;
	public int index;

	// Use this for initialization
	void Start ( ) {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		shiprigidbody = GameObject.Find("Player");
		if (enemyBod.transform.position.y < 0) {
			startfrom = occurplace.down;
		}
	}
	
	void Update () {
		transform.RotateAround(transform.position, Vector3.forward, 400* Time.deltaTime);
		var cos = Mathf.Cos (Mathf.PI /4);
		var sin = Mathf.Sin (Mathf.PI /4);
		if (this.transform.position.x <= - camW /8 && curr == movementState.forward) {

		//if (this.transform.position.x <= shiprigidbody.transform.position.x && curr == movementState.forward) {
			curr = movementState.back;
			
			//enemyBod.velocity = new Vector3 (speed, 0f , 0f);
			
			if (startfrom == occurplace.up) {
				sin = -sin;
			}
			enemyBod.velocity = new Vector3 (speed * cos, speed * sin, 0f);
			
		} else if (curr == movementState.back) {
			//if (this.transform.position.y <= shiprigidbody.transform.position.y && startfrom == occurplace.up) {

			if (this.transform.position.y <= 0f && startfrom == occurplace.up) {
				curr = movementState.rotate;
				enemyBod.velocity = new Vector3 (enemyBod.velocity.x * cos - enemyBod.velocity.y * sin, enemyBod.velocity.x * sin + enemyBod.velocity.y * cos, 0f);
				
			} 
			//else if (this.transform.position.y >= shiprigidbody.transform.position.y && startfrom == occurplace.down) {
			else if (this.transform.position.y >= 0f && startfrom == occurplace.down) {
				sin = -sin;
				curr = movementState.rotate;
				enemyBod.velocity = new Vector3 (enemyBod.velocity.x * cos - enemyBod.velocity.y * sin, enemyBod.velocity.x * sin + enemyBod.velocity.y * cos, 0f);
				
			}


		} else if (this.transform.position.x >= camW / 2 && curr != movementState.forward) {
			Destroy (this.gameObject);
		} else if (this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y / 2 <= -camH / 2 || this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 >= camH / 2) {
			Destroy (this.gameObject);
		}
	}	
	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Shield"||coll.gameObject.tag == "Shot1"||coll.gameObject.tag == "Shot2") {
			PlayerController.fansKilled[index] += 1;
			if (PlayerController.fansKilled[index] == 4) {
				GameObject power = Instantiate(PowerUp) as GameObject;
				power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
			}
			Scored();
			//Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
	void OnDestroy(){
		
	}
}
