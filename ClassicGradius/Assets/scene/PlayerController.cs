using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

enum powerLevel { None, Speedup, Missile, Double, Laser, Option, Shield};

public class PlayerController : MonoBehaviour {
	public Vector2 maxSpeed;
	Rigidbody shipRigid;
	public GameObject shotPrefab;
	public GameObject missilePrefab;
	public GameObject doublePrefab;
	public GameObject laserPrefab;
	public GameObject optionPrefab;
	public GameObject shieldPrefab;
	public Vector3 shotSpawn = new Vector3(1.5f,0f,0f);
	Text healthText;
	public float reload;
	float currReload;
	public GameObject[] powpanels;
	powerLevel pow;
	Image panel;
    static public float camH, camW;
	int speedupCount;
	int optionCount;
	bool missileEnable;
	bool doubleEnable;
	bool laserEnable;
	public bool shieldEnable;
	static public bool missileReload;
	GameObject[] option = new GameObject[4];
	Queue<Vector3>[] lastPos = new Queue<Vector3>[4];
	static public int[] fansKilled = new int[30];
	bool invinMode;
	public GameObject die;

	// Use this for initialization
	void Start () {
		BackgroundScroll.speed = 2f;
		for (int i=0; i<4; i++) {
			lastPos[i] = new Queue<Vector3>();
			for (int j=0; j<30*i; j++)
				lastPos[i].Enqueue(new Vector3(-j*0.03f,-j*0.03f,0));
		}
		for (int i=0; i<30; i++) {
			fansKilled[i] = 0;
		}
		shipRigid = this.GetComponent<Rigidbody> ();
		healthText = GameObject.Find ("Health").GetComponent<Text>();
		healthText.text = StartController.health.ToString();
		invinMode = false;
		pow = powerLevel.None;
		speedupCount = 0;
		optionCount = 0;
		missileEnable = false;
		doubleEnable = false;
		laserEnable = false;
		shieldEnable = false;
		missileReload = true;
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        camH = cam.orthographicSize * 2f;
        camW = camH * cam.aspect;
	}
	
	// Update is called once per frame
	void Update () {

		if (currReload < Time.deltaTime) {
			currReload = 0;
		} else {
			currReload -= Time.deltaTime;
		}
		if (StartController.health <= 0) {
			Application.LoadLevel("StartScene");
			StartController.startScene = true;
			StartController.health = 3;
			Spawner.spot = 0;
		}
		Vector3 speed = Vector3.zero;
		if (shieldEnable == false)
			powpanels[5].GetComponent<Text>().text = "?";
		if (Input.GetKey (KeyCode.I)) {
			invinMode = true;
		}
		if (Input.GetKey (KeyCode.C)) {
			switch(pow){
			case powerLevel.Speedup:
				print ("enter speedup");
				if (speedupCount < 5) {
					speedupCount += 1;
					PowerNone();
					if (speedupCount == 5) {
						powpanels[0].GetComponent<Text>().text = "------";
					}
					maxSpeed.x += 1f;
					maxSpeed.y += 0.5f;
				}
				break;
			case powerLevel.Missile:
				print ("enter missile");
				if (missileEnable == false) {
					PowerNone();
					powpanels[1].GetComponent<Text>().text = "------";
					missileEnable = true;
					missileReload = false;
				}
				break;
			case powerLevel.Double:
				print ("enter double");
				if (doubleEnable == false) {
					PowerNone();
					powpanels[3].GetComponent<Text>().text = "Laser";
					laserEnable = false;
					powpanels[2].GetComponent<Text>().text = "------";
					doubleEnable = true;
				}
				break;
			case powerLevel.Laser:
				print ("enter laser");
				if (laserEnable == false) {
					PowerNone();	
					powpanels[2].GetComponent<Text>().text = "DOUBLE";
					doubleEnable = false;
					powpanels[3].GetComponent<Text>().text = "------";
					laserEnable = true;
				}
				break;
			case powerLevel.Option:
				print ("enter option");
				if (optionCount < 4) {
					PowerNone();
					option[optionCount] = Instantiate(optionPrefab) as GameObject;
					option[optionCount].transform.position = lastPos[optionCount].Peek();
					optionCount += 1;
					if (optionCount == 4) {
						powpanels[4].GetComponent<Text>().text = "------";
					}
				}
				break;
			case powerLevel.Shield:
				print ("enter Shield");
				if (shieldEnable == false) {
					PowerNone();
					GameObject shield;
					shield = Instantiate(shieldPrefab) as GameObject;
					powpanels[5].GetComponent<Text>().text = "------";
					shieldEnable = true;
				}
				break;
			}
		
		}
		if (Input.GetKey (KeyCode.W) && this.gameObject.transform.position.y + this.gameObject.transform.lossyScale.y / 2 < camH / 2) {
			speed.y += maxSpeed.y;
			lastPos[0].Enqueue(this.gameObject.transform.position);
			lastPos[1].Enqueue(this.gameObject.transform.position);
			lastPos[2].Enqueue(this.gameObject.transform.position);
			lastPos[3].Enqueue(this.gameObject.transform.position);
			if (lastPos[0].Count > 30)
				lastPos[0].Dequeue();
			if (lastPos[1].Count > 60)
				lastPos[1].Dequeue();
			if (lastPos[2].Count > 90)
				lastPos[2].Dequeue();
			if (lastPos[3].Count > 120)
				lastPos[3].Dequeue();
			if (optionCount>3) {
				option[3].transform.position = lastPos[3].Peek();
			}
			if (optionCount>2) {
				option[2].transform.position = lastPos[2].Peek();
			}
			if (optionCount>1) {
				option[1].transform.position = lastPos[1].Peek();
			}
			if (optionCount>0) {
				option[0].transform.position = lastPos[0].Peek();
			}
		}
		if (Input.GetKey (KeyCode.S) && this.gameObject.transform.position.y - this.gameObject.transform.lossyScale.y / 2 > -camH / 2) {
			speed.y -= maxSpeed.y;
			lastPos[0].Enqueue(this.gameObject.transform.position);
			lastPos[1].Enqueue(this.gameObject.transform.position);
			lastPos[2].Enqueue(this.gameObject.transform.position);
			lastPos[3].Enqueue(this.gameObject.transform.position);
			if (lastPos[0].Count > 30)
				lastPos[0].Dequeue();
			if (lastPos[1].Count > 60)
				lastPos[1].Dequeue();
			if (lastPos[2].Count > 90)
				lastPos[2].Dequeue();
			if (lastPos[3].Count > 120)
				lastPos[3].Dequeue();
			if (optionCount>3) {
				option[3].transform.position = lastPos[3].Peek();
			}
			if (optionCount>2) {
				option[2].transform.position = lastPos[2].Peek();
			}
			if (optionCount>1) {
				option[1].transform.position = lastPos[1].Peek();
			}
			if (optionCount>0) {
				option[0].transform.position = lastPos[0].Peek();
			}
		}
		if (Input.GetKey (KeyCode.A) &&  this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x/2 > -camW/2) {
			speed.x -= maxSpeed.x;
			lastPos[0].Enqueue(this.gameObject.transform.position);
			lastPos[1].Enqueue(this.gameObject.transform.position);
			lastPos[2].Enqueue(this.gameObject.transform.position);
			lastPos[3].Enqueue(this.gameObject.transform.position);
			if (lastPos[0].Count > 30)
				lastPos[0].Dequeue();
			if (lastPos[1].Count > 60)
				lastPos[1].Dequeue();
			if (lastPos[2].Count > 90)
				lastPos[2].Dequeue();
			if (lastPos[3].Count > 120)
				lastPos[3].Dequeue();
			if (optionCount>3) {
				option[3].transform.position = lastPos[3].Peek();
			}
			if (optionCount>2) {
				option[2].transform.position = lastPos[2].Peek();
			}
			if (optionCount>1) {
				option[1].transform.position = lastPos[1].Peek();
			}
			if (optionCount>0) {
				option[0].transform.position = lastPos[0].Peek();
			}
		}
		if (Input.GetKey (KeyCode.D) &&  this.gameObject.transform.position.x + this.gameObject.transform.lossyScale.x/2 < camW/2) {
			speed.x += maxSpeed.x;
			lastPos[0].Enqueue(this.gameObject.transform.position);
			lastPos[1].Enqueue(this.gameObject.transform.position);
			lastPos[2].Enqueue(this.gameObject.transform.position);
			lastPos[3].Enqueue(this.gameObject.transform.position);
			if (lastPos[0].Count > 30)
				lastPos[0].Dequeue();
			if (lastPos[1].Count > 60)
				lastPos[1].Dequeue();
			if (lastPos[2].Count > 90)
				lastPos[2].Dequeue();
			if (lastPos[3].Count > 120)
				lastPos[3].Dequeue();
			if (optionCount>3) {
				option[3].transform.position = lastPos[3].Peek();
			}
			if (optionCount>2) {
				option[2].transform.position = lastPos[2].Peek();
			}
			if (optionCount>1) {
				option[1].transform.position = lastPos[1].Peek();
			}
			if (optionCount>0) {
				option[0].transform.position = lastPos[0].Peek();
			}
		}
		shipRigid.velocity = speed;
		if(Input.GetKey(KeyCode.Space)){
			if(currReload > 0) return;
			GameObject missile;
			GameObject shot;
			GameObject doubleshot;
			GameObject laser;
			bool missileReloadTmp = missileReload;
			currReload = reload;
			if (laserEnable == false) {
				shot = Instantiate(shotPrefab) as GameObject;
				shot.GetComponent<Rigidbody>().MovePosition( new Vector3(this.transform.position.x + shotSpawn.x, this.transform.position.y + shotSpawn.y, 0));
				if (doubleEnable == true) {
					doubleshot = Instantiate(doublePrefab) as GameObject;
					doubleshot.GetComponent<Rigidbody>().MovePosition( new Vector3(this.transform.position.x-0.3f + shotSpawn.x, this.transform.position.y + shotSpawn.y, 0));
				}
			}
			else {
				laser = Instantiate(laserPrefab) as GameObject;
				laser.GetComponent<LaserBehaviour>().go = this.gameObject;
				laser.GetComponent<Rigidbody>().transform.position = new Vector3(this.transform.position.x+2f + shotSpawn.x, this.transform.position.y + shotSpawn.y, 0);
			}
			if (missileReload == false) {
				missileReload = true;
				missile = Instantiate(missilePrefab) as GameObject;
				missile.GetComponent<Rigidbody>().MovePosition( new Vector3(this.transform.position.x-1f + shotSpawn.x, this.transform.position.y + shotSpawn.y, 0));
			}
			if (optionCount>3) {
				GameObject missileOption;
				GameObject shotOption;
				GameObject doubleshotOption;
				GameObject laserOption;
				currReload = reload;
				if (laserEnable == false) {
					shotOption = Instantiate(shotPrefab) as GameObject;
					shotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[3].transform.position.x + shotSpawn.x, option[3].transform.position.y + shotSpawn.y, 0));
					if (doubleEnable == true) {
						doubleshotOption = Instantiate(doublePrefab) as GameObject;
						doubleshotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[3].transform.position.x-0.3f + shotSpawn.x, option[3].transform.position.y + shotSpawn.y, 0));
					}
				}
				else {
					laserOption = Instantiate(laserPrefab) as GameObject;
					laserOption.GetComponent<LaserBehaviour>().go = option[3].gameObject;
					laserOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[3].transform.position.x+2f + shotSpawn.x, option[3].transform.position.y + shotSpawn.y, 0));
				}
				if (missileReloadTmp == false) {
					missileOption = Instantiate(missilePrefab) as GameObject;
					missileOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[3].transform.position.x-1f + shotSpawn.x, option[3].transform.position.y + shotSpawn.y, 0));
				}
			}
			if (optionCount>2) {
				GameObject missileOption;
				GameObject shotOption;
				GameObject doubleshotOption;
				GameObject laserOption;
				currReload = reload;
				if (laserEnable == false) {
					shotOption = Instantiate(shotPrefab) as GameObject;
					shotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[2].transform.position.x + shotSpawn.x, option[2].transform.position.y + shotSpawn.y, 0));
					if (doubleEnable == true) {
						doubleshotOption = Instantiate(doublePrefab) as GameObject;
						doubleshotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[2].transform.position.x-0.3f + shotSpawn.x, option[2].transform.position.y + shotSpawn.y, 0));
					}
				}
				else {
					laserOption = Instantiate(laserPrefab) as GameObject;
					laserOption.GetComponent<LaserBehaviour>().go = option[2].gameObject;
					laserOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[2].transform.position.x+2f + shotSpawn.x, option[2].transform.position.y + shotSpawn.y, 0));
				}
				if (missileReloadTmp == false) {
					missileOption = Instantiate(missilePrefab) as GameObject;
					missileOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[2].transform.position.x-1f + shotSpawn.x, option[2].transform.position.y + shotSpawn.y, 0));
				}
			}
			if (optionCount>1) {
				GameObject missileOption;
				GameObject shotOption;
				GameObject doubleshotOption;
				GameObject laserOption;
				currReload = reload;
				if (laserEnable == false) {
					shotOption = Instantiate(shotPrefab) as GameObject;
					shotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[1].transform.position.x + shotSpawn.x, option[1].transform.position.y + shotSpawn.y, 0));
					if (doubleEnable == true) {
						doubleshotOption = Instantiate(doublePrefab) as GameObject;
						doubleshotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[1].transform.position.x-0.3f + shotSpawn.x, option[1].transform.position.y + shotSpawn.y, 0));
					}
				}
				else {
					laserOption = Instantiate(laserPrefab) as GameObject;
					laserOption.GetComponent<LaserBehaviour>().go = option[1].gameObject;
					laserOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[1].transform.position.x+2f + shotSpawn.x, option[1].transform.position.y + shotSpawn.y, 0));
				}
				if (missileReloadTmp == false) {
					missileOption = Instantiate(missilePrefab) as GameObject;
					missileOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[1].transform.position.x-1f + shotSpawn.x, option[1].transform.position.y + shotSpawn.y, 0));
				}
			}
			if (optionCount>0) {
				GameObject missileOption;
				GameObject shotOption;
				GameObject doubleshotOption;
				GameObject laserOption;
				currReload = reload;
				if (laserEnable == false) {
					shotOption = Instantiate(shotPrefab) as GameObject;
					shotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[0].transform.position.x + shotSpawn.x, option[0].transform.position.y + shotSpawn.y, 0));
					if (doubleEnable == true) {
						doubleshotOption = Instantiate(doublePrefab) as GameObject;
						doubleshotOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[0].transform.position.x-0.3f + shotSpawn.x, option[0].transform.position.y + shotSpawn.y, 0));
					}
				}
				else {
					laserOption = Instantiate(laserPrefab) as GameObject;
					laserOption.GetComponent<LaserBehaviour>().go = option[0].gameObject;
					laserOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[0].transform.position.x+2f + shotSpawn.x, option[0].transform.position.y + shotSpawn.y, 0));
				}
				if (missileReloadTmp == false) {
					missileOption = Instantiate(missilePrefab) as GameObject;
					missileOption.GetComponent<Rigidbody>().MovePosition( new Vector3(option[0].transform.position.x-1f + shotSpawn.x, option[0].transform.position.y + shotSpawn.y, 0));
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			currReload = 0;
		}
	}

	void OnTriggerEnter (Collider coll){
		if (invinMode == true)
			return;
		if (coll.gameObject.tag == "Basic" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyWeapon" || coll.gameObject.tag == "Hill") {
			var x = this.gameObject.transform.position;
			this.gameObject.SetActive(false);
			Instantiate(die,x,this.gameObject.transform.rotation);
			StartController.health -= 1;
			Application.LoadLevel("MainScene");
			Spawner.spot = 10*(int)(Spawner.spot/10);
		}
	}

	public void UpGrade(){
		switch(pow){
		case powerLevel.None:
			pow = powerLevel.Speedup;
			powpanels[0].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Speedup:
			pow = powerLevel.Missile;
			powpanels[0].GetComponent<Text>().color = Color.white;
			powpanels[1].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Missile:
			pow = powerLevel.Double;
			powpanels[1].GetComponent<Text>().color = Color.white;
			powpanels[2].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Double:
			pow = powerLevel.Laser;
			powpanels[2].GetComponent<Text>().color = Color.white;
			powpanels[3].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Laser:
			pow = powerLevel.Option;
			powpanels[3].GetComponent<Text>().color = Color.white;
			powpanels[4].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Option:
			pow = powerLevel.Shield;
			powpanels[4].GetComponent<Text>().color = Color.white;
			powpanels[5].GetComponent<Text>().color = Color.yellow;
			break;
		case powerLevel.Shield:
			pow = powerLevel.Speedup;
			powpanels[5].GetComponent<Text>().color = Color.white;
			powpanels[0].GetComponent<Text>().color = Color.yellow;
			break;
		}
	}

	public void PowerNone (){
		pow = powerLevel.None;
		foreach (GameObject panel in powpanels){
			panel.GetComponent<Text>().color = Color.white;
		}
	}

	void OnDestroy (){

	}
}
