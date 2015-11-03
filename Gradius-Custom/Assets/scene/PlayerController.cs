using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum powerLevel{None,Double,Shield}

public class PlayerController : MonoBehaviour {
	public Vector2 maxSpeed;
	Rigidbody shipRigid;
	public GameObject shotPrefab;
	public GameObject doublePrefab;
	public GameObject shieldPrefab;
	public Vector3 shotSpawn = new Vector3(1.5f,0f,0f);
	Text healthText;
	public float reload;
	float currReload;
	public GameObject[] powpanels;
	Image panel;
	static public float camH, camW;
	static public powerLevel pow;
	powerLevel lastPow;
	static public int[] fansKilled = new int[30];
	static public bool invinMode;
	public float powerTimeTotal;
	float powerTime;
	public int degree;
	static public bool passRoad;
	
	// Use this for initialization
	void Start () {
		BackgroundScroll.speed = 2f;
		for (int i=0; i<30; i++) {
			fansKilled[i] = 0;
		}
		shipRigid = this.GetComponent<Rigidbody> ();
		healthText = GameObject.Find ("Health").GetComponent<Text>();
		healthText.text = StartController.health.ToString();
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		powerTime = 0;
		pow = powerLevel.None;
		lastPow = powerLevel.None;
		degree = 0;
		passRoad = false;
		invinMode = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (currReload < Time.deltaTime) {
			currReload = 0;
		} else {
			currReload -= Time.deltaTime;
		}
		if (powerTime < Time.deltaTime) {
			powerTime = 0;
			PowerNone();
		} else {
			powerTime -= Time.deltaTime;
		}
		GameObject.Find ("Time").GetComponent<Slider> ().value = ((float)powerTime)/powerTimeTotal;
		if (StartController.health <= 0) {
			Application.LoadLevel("StartScene");
			StartController.startScene = false;
			StartController.health = 3;
			Spawner.spot = 0;
		}
		Vector3 speed = Vector3.zero;
		if (Input.GetKey (KeyCode.I)) {
			invinMode = true;
		}
		if ((Input.GetKey (KeyCode.W)||Input.GetKey (KeyCode.UpArrow)) && this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 < camH / 2) {
			speed.y += maxSpeed.y;
		}
		if ((Input.GetKey (KeyCode.S)||Input.GetKey (KeyCode.DownArrow)) && this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y / 2 > -camH / 2) {
			speed.y -= maxSpeed.y;
		}
		if ((Input.GetKey (KeyCode.A)||Input.GetKey (KeyCode.LeftArrow)) &&  this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/2 > -camW/2) {
			speed.x -= maxSpeed.x;
		}
		if ((Input.GetKey (KeyCode.D)||Input.GetKey (KeyCode.RightArrow)) &&  this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/2 < camW/2) {
			speed.x += maxSpeed.x;
		}
		if ((Input.GetKey (KeyCode.Z)||Input.GetKey (KeyCode.K))) {
			if (degree < 60) {
				transform.Rotate(0,0,5f);
				degree += 5;
			}
		}
		if ((Input.GetKey (KeyCode.X)||Input.GetKey (KeyCode.L))) {
			if (degree > -60) {
				transform.Rotate(0,0,-5f);
				degree -= 5;
			}
		}
		shipRigid.velocity = speed;
		if (passRoad == true) {
			shipRigid.velocity = new Vector3(-3f,0,0);
			if (transform.position.x < -camW / 4) {
				passRoad = false;
			}
		}
		if(Input.GetKey(KeyCode.Space)){
			if(currReload > 0) return;
			GameObject shot;
			GameObject doubleshot;
			currReload = reload;
			shot = Instantiate(shotPrefab) as GameObject;
			if (pow == powerLevel.Double) {
				doubleshot = Instantiate(doublePrefab) as GameObject;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			currReload = 0;
		}

	}
	
	void OnTriggerEnter (Collider coll){
		if (invinMode == true)
			return;
		if (coll.gameObject.tag == "Basic" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyWeapon" || coll.gameObject.tag == "obstcale") {
			StartController.health -= 1;
			Application.LoadLevel("MainScene2");
			StartController.startScene = false;
		}
	}
	
	public void SwitchPower (){
		switch(pow){
		case powerLevel.None:
			if (lastPow == powerLevel.Double) ToShield();
			else ToDouble();
			break;
		case powerLevel.Shield:
			ToShield();
			break;
		case powerLevel.Double:
			ToDouble();
			break;
		}
		powerTime = powerTimeTotal;
	}
	
	void ToDouble (){
		pow = powerLevel.Double;
		lastPow = powerLevel.Double;
		powpanels[0].GetComponent<Text>().color = Color.yellow;
		powpanels[1].GetComponent<Text>().color = Color.white;
	}
	
	void ToShield (){
		Destroy(GameObject.FindWithTag("Shield"));
		pow = powerLevel.Shield;
		lastPow = powerLevel.Shield;
		GameObject shield;
		shield = Instantiate(shieldPrefab) as GameObject;
		powpanels[1].GetComponent<Text>().color = Color.yellow;
		powpanels[0].GetComponent<Text>().color = Color.white;
	}
	
	public void PowerNone (){
		pow = powerLevel.None;
		Destroy(GameObject.FindWithTag("Shield"));
		powpanels[0].GetComponent<Text>().color = Color.white;
		powpanels[1].GetComponent<Text>().color = Color.white;
	}
	
	void OnDestroy (){
		
	}
}