using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {
	bool caveside = true;
	public List <WaveofEnemy> WaveInfo; 
	float size;
	static public int spot = 0;
	bool side = false;
	bool deeside = false;
	public Vector3 topPos;
	public Vector3 cavePos;
	public bool ifneednext = false;
	public Vector3 bottomPos;
	public Vector3 middlePos;
	public Vector3 fanstopPos;
	public Vector3 fansbotPos;
	public Vector3 sinetopPos;
	public Vector3 sinebotPos;
	public Vector3 DeePos;
	public Vector3 DeedownPos;
	public Vector3 JumperPos;
	public Vector3 TopDuckerPos;
	public Vector3 BottomDuckerPos;
	public Vector3 BackgroundPos;
	public Vector3 topRoadPos;
	public float respawn;
	public float spawnTime;
	public float slowrespawnTime = 0.5f;
	public float sinespawnTime = 0.2f;
	float camH, camW;
	enum duckerappear {left,right};
	public int addedenemy = 0;
	public GameObject hat;
	public WaveofEnemy currentenemy;
	float nextwavetime;
	int count;
	public GameObject cave;
	public Vector3 obstcalePos;
	public float respawnforobstacle;
	public int countNum;
	Text tutorial;
	string[] msg = new string[50];
	int fansIndex;
	public bool ifinstruction = false;

	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		topPos.x = bottomPos.x = middlePos.x =  sinebotPos.x = sinetopPos.x= camW/2 +1f;
		middlePos.z = 0f;
		topPos.y = camH/2-1f;
		topPos.z = 0f;
		cavePos = topPos;
		cavePos.y += 0.5f;
		bottomPos.y= -topPos.y+1f;
		bottomPos.z = 0f;
		BackgroundPos.x = topPos.x + 80f;
		BackgroundPos.y = middlePos.y = (topPos.y + bottomPos.y) / 2 -0.5f;
		BackgroundPos.z = 0;
		fanstopPos.x = fansbotPos.x = topPos.x;
		fanstopPos.y = topPos.y - 0.8f;
		fansbotPos.y = bottomPos.y + 0.8f;
		fanstopPos.z = fansbotPos.z = topPos.z;
		sinetopPos.y = (topPos.y + bottomPos.y) / 2 + 2.5f;
		sinebotPos.y = -sinebotPos.y;
		sinetopPos.y -= 1.5f;
		sinebotPos.y -= 2.5f;
		sinetopPos.z = 0f;
		sinebotPos.z = 0f;
		
		DeePos.x = topPos.x;
		DeePos.y = topPos.y;
		DeePos.z = 0;
		hat = null;
		DeedownPos.x = bottomPos.x;
		DeedownPos.y = bottomPos.y;
		DeedownPos.z = 0;
		JumperPos.y = bottomPos.y+camH/6;
		JumperPos.x = camW / 2;
		JumperPos.z = 0;
		
		BottomDuckerPos.x = TopDuckerPos.x = -camW / 2;
		TopDuckerPos.y = topPos.y;
		BottomDuckerPos.y = bottomPos.y;
		BottomDuckerPos.z = TopDuckerPos.z = 0;

		topRoadPos = topPos;
		topRoadPos.y += 2f;

		obstcalePos.x = camW / 2 + 2.5f;
		obstcalePos.y = 0f;
		obstcalePos.z = 0f;
		countNum = 0;
		respawnforobstacle = 0f;

		respawn = 0.14f;
		addedenemy = 0;
		spot = 0;
		currentenemy = WaveInfo[spot];
		count = 0;
		size = (float)WaveInfo.Count;
		GameObject.Find ("Slider").GetComponent<Slider> ().value = (spot+1) / size;
		
		tutorial = GameObject.Find ("Tutorial").GetComponent<Text>();
		msg [0] = "Press Z to rotate counter-clockwise, X to rotate clockwise. (Z = K, X = L)";

		msg [1] = "Destroy the entire formation of Stars to get a a power-up capsule.";
		msg [2] = "Time-varying Force field will change the trajactory.";
		msg [3] = "Stay away from the tubes.";
		msg [9] = "Mario: Get Me!";
		msg [10] = "Mario: Do Not Shoot at Me!";
		msg [11] = "Mario: Do Not Shoot at Me!";
		msg [12] = "Mario: Do Not Shoot at Me!";
		fansIndex = 0;
		print (middlePos);
	}
	
	// Update is called once per frame
	void Update () {
		if (BackgroundScroll.speed == 0)
			return;
		if (Time.time < nextwavetime) {
			return;
		}
		if (spot >= WaveInfo.Count) 
			return;
		

		//tutorial
		tutorial.text = msg[spot];
		if (spot == 0) {
			tutorial.text = msg[0];
			if (Input.GetKey (KeyCode.Z)||Input.GetKey (KeyCode.K)||Input.GetKey (KeyCode.X)||Input.GetKey (KeyCode.L)) {
				spot = 1;
			}
			return;
		} else if (spot == 1) {
			if (!ifinstruction){
				ifinstruction = true;
			}

			CoreForce.dir = Vector3.down;
			CoreForce.dirop = Vector3.up;
			if (PlayerController.pow != powerLevel.Double) {
				respawn -= Time.deltaTime;
				if (respawn > 0)
					return;
				respawn = spawnTime;
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, bottomPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys> ().index = fansIndex;
				addedenemy++;
				if (addedenemy < currentenemy.count){
					return;
				}

				fansIndex += 1;
				addedenemy = 0;
				spot--;
				tutorial.text = msg[1];
				nextwavetime = Time.time+currentenemy.delaytime;
			}
			spot++;
			GameObject.Find ("Slider").GetComponent<Slider> ().value = spot / size;
			currentenemy = WaveInfo [spot];
			return;
		} else if (spot == 2) {

			if (PlayerController.pow != powerLevel.Shield) {
				respawn -= Time.deltaTime;
				if (respawn > 0)
					return;
				respawn = spawnTime;
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, topPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				GameObject fans2 = (GameObject)Instantiate (currentenemy.enemyPrefab2, bottomPos, currentenemy.enemyPrefab2.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys> ().index = fansIndex;
				fans2.GetComponent<FansEnemys> ().index = fansIndex+1;
				addedenemy++;
				if (addedenemy < currentenemy.count){
					return;
				}
				fansIndex += 2;
				addedenemy = 0;
				spot--;
				nextwavetime = Time.time+currentenemy.delaytime;
			}
			spot++;
			GameObject.Find ("Slider").GetComponent<Slider> ().value = spot / size;
			currentenemy = WaveInfo [spot];
			return;
		}
		if (spot >= 8) {
			CoreForce.dir = Vector3.down;
			CoreForce.dirop = Vector3.up;
		}
		switch(currentenemy.enemytype){
		case typeifenemies.road:
			Instantiate (currentenemy.enemyPrefab, topRoadPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		case typeifenemies.fan:
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			
			respawn = spawnTime;

			if (side == true) {
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, bottomPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys>().index = fansIndex;
			} else {
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, topPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys>().index = fansIndex;
			}
			addedenemy++;
			if (addedenemy < currentenemy.count){
				return;
			}
			fansIndex += 1;
			side = !side;
			break;
		case typeifenemies.garun:
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = 1.5f;
			Instantiate (currentenemy.enemyPrefab, fansbotPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			Instantiate (currentenemy.enemyPrefab2, middlePos, currentenemy.enemyPrefab2.gameObject.transform.rotation);
			Instantiate (currentenemy.enemyPrefab3, fanstopPos, currentenemy.enemyPrefab3.gameObject.transform.rotation);
			
			addedenemy++;
			if (addedenemy < currentenemy.count){
				return;
			}
			break;
		case typeifenemies.sine:
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = 1f;
			if ( addedenemy==1 && currentenemy.index==1) {
				GameObject sine1 = (GameObject)Instantiate (currentenemy.enemyPrefab3, sinetopPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				GameObject sine2 = (GameObject)Instantiate (currentenemy.enemyPrefab4, sinebotPos, currentenemy.enemyPrefab2.gameObject.transform.rotation);
				sine1.GetComponent<SineE>().isRed = true;
				sine2.GetComponent<SineE>().isRed = true;
			}
			else {
				Instantiate (currentenemy.enemyPrefab, sinetopPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				Instantiate (currentenemy.enemyPrefab2, sinebotPos, currentenemy.enemyPrefab2.gameObject.transform.rotation);
			}
			addedenemy++;
			if (addedenemy<currentenemy.count)
				return;
			break;
		case typeifenemies.cave:
			count ++;
			if (count == 200) {
				if (caveside == true) {
					Instantiate (cave, bottomPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				}
				else {
					Instantiate (cave, cavePos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				}
				caveside = !caveside;
				count = 0;
				addedenemy++;
			}
			if (addedenemy < currentenemy.count){
				return;
			}
			break;
		case typeifenemies.obstcale:
			respawnforobstacle -=Time.deltaTime;
			if (respawnforobstacle > 0)
				return;
			respawnforobstacle = 3.5f;
			Instantiate (currentenemy.enemyPrefab, obstcalePos,currentenemy.enemyPrefab.gameObject.transform.rotation);
			countNum++;
			if (countNum < currentenemy.count)
				return;
			break;
		case typeifenemies.mario:
			Instantiate (currentenemy.enemyPrefab, middlePos,currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		default:
			break;
		}
		spot++;
		if (spot >= WaveInfo.Count) 
			return;
		GameObject.Find ("Slider").GetComponent<Slider> ().value = spot / size;
		currentenemy = WaveInfo [spot];
		if (currentenemy.enemytype == typeifenemies.fan) {
			respawn = 0;
		}
		nextwavetime = Time.time+currentenemy.delaytime;
		respawn = 0;
		addedenemy = 0;

	}

	IEnumerator Example() {
		yield return new WaitForSeconds(10f);
	}

	IEnumerator deedelay() {
		yield return new WaitForSeconds(1.2f);
	}
}
