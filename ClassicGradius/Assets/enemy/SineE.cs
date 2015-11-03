using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SineE : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public GameObject shiprigidbody;
	Rigidbody enemyBod;
	Text score;
	public int points = 100;
	float camH,camW;
	float m_degrees = 15.0f;
	float m_amplitude = 0.05f;
	public GameObject PowerUp;
	public bool isRed;

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
		/*
		var pos = enemyBod.transform.position;
		pos.y += Mathf.Sin(Time.time*720*3)*0.1f;
		enemyBod.transform.rotation = Quaternion.AngleAxis(30, Vector3.forward);
		enemyBod.AddForce(-enemyBod.transform.right * speed);
		*/
		float deltaTime = Time.deltaTime;
		float m_period = 2.5f;
		// Move center along x axis
		//enemyBod.gameObject.transform.position.x += deltaTime * speed;
		
		// Update degrees
		float degreesPerSecond = 360.0f / m_period;
		m_degrees = Mathf.Repeat(m_degrees + (deltaTime * degreesPerSecond), 360.0f);
		float radians = m_degrees * Mathf.Deg2Rad;
		
		// Offset by sin wave
		Vector3 offset = new Vector3(0.0f, m_amplitude * Mathf.Sin(radians), 0.0f);
		transform.position = enemyBod.gameObject.transform.position + offset;
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
			Destroy(this.gameObject);
			if (isRed == true) {
				GameObject power = Instantiate(PowerUp) as GameObject;
				power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
			}
			//Destroy(coll.gameObject);
		}
	}
	void OnDestroy(){
		
	}
}
