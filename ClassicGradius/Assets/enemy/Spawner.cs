using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public List <WaveofEnemy> WaveInfo; 
	static public int spot = 0;
	bool side = false;
	bool deeside = false;
	public Vector3 topPos;
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
	public int hatchercount;
	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		topPos.x = bottomPos.x = middlePos.x =  sinebotPos.x = sinetopPos.x= camW/2 +1f;
		middlePos.z = 0f;
		topPos.y = camH/2-1f;
		topPos.z = 0f;
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

		respawn = 0.14f;
		addedenemy = 0;
		spot = 0;
		currentenemy = WaveInfo[spot];

		hatchercount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (spot);
		if (Time.time < nextwavetime) {
			return;
		}
		if (spot >= WaveInfo.Count) 
			return;
		switch(currentenemy.enemytype){
		case typeifenemies.background:
			Instantiate (currentenemy.enemyPrefab, BackgroundPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		case typeifenemies.fan:
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			
			respawn = spawnTime;

			if (side == true) {
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, fansbotPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys>().index = currentenemy.index;
			} else {
				GameObject fans = (GameObject)Instantiate (currentenemy.enemyPrefab, fanstopPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				fans.GetComponent<FansEnemys>().index = currentenemy.index;
			}
			addedenemy++;
			if (addedenemy < currentenemy.count){
				return;
			}
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
		case typeifenemies.ducker:
			Instantiate (currentenemy.enemyPrefab, TopDuckerPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			Instantiate (currentenemy.enemyPrefab2, BottomDuckerPos, currentenemy.enemyPrefab2.gameObject.transform.rotation);
			break;
		case typeifenemies.topducker:
			if (currentenemy.index == 1) {
				GameObject ducker1 = (GameObject)Instantiate (currentenemy.enemyPrefab, TopDuckerPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				ducker1.GetComponent<DuckerE>().isRed = true;
			}
			else {
				Instantiate (currentenemy.enemyPrefab, TopDuckerPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			}
			break;
		case typeifenemies.bottomducker:
			if (currentenemy.index == 1) {
				GameObject ducker2 = (GameObject)Instantiate (currentenemy.enemyPrefab, BottomDuckerPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				ducker2.GetComponent<DuckerE>().isRed = true;
			}
			else {
				Instantiate (currentenemy.enemyPrefab, BottomDuckerPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			}
			break;
		case typeifenemies.dee:
			if (hatchercount%3 ==0 || spot == 12){
				Instantiate (currentenemy.enemyPrefab, DeePos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				StartCoroutine (deedelay ());
				Instantiate (currentenemy.enemyPrefab2, DeePos + new Vector3(1f,0f,0f), currentenemy.enemyPrefab2.gameObject.transform.rotation);
				if (currentenemy.count == 3) {
					StartCoroutine (deedelay ());
					Instantiate (currentenemy.enemyPrefab3, DeePos+ new Vector3(2f,0f,0f), currentenemy.enemyPrefab3.gameObject.transform.rotation);
				}
			}
			else{
				Instantiate (currentenemy.enemyPrefab, DeedownPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				StartCoroutine (deedelay ());
				Instantiate (currentenemy.enemyPrefab2, DeedownPos + new Vector3(1f,0f,0f), currentenemy.enemyPrefab2.gameObject.transform.rotation);
				if (currentenemy.count == 3) {
					StartCoroutine (deedelay ());
					Instantiate (currentenemy.enemyPrefab3, DeedownPos+ new Vector3(2f,0f,0f), currentenemy.enemyPrefab3.gameObject.transform.rotation);
				}
			}
			break;
		case typeifenemies.boss:
			Instantiate (currentenemy.enemyPrefab, middlePos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		case typeifenemies.jumper:
			Instantiate (currentenemy.enemyPrefab, JumperPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		case typeifenemies.hatcher:
			//if (!ifneednext){
				if (hatchercount%3!=0) {
					Instantiate (currentenemy.enemyPrefab, bottomPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
					
				} else {
					Instantiate (currentenemy.enemyPrefab2, topPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
				}
			hatchercount++;
				
				//ifneednext = true;
				//return;
			//}
			//fneednext = false;
			break;
		default:
			break;
		}
		spot++;
		if (spot >= WaveInfo.Count) 
			return;
		currentenemy = WaveInfo [spot];
		if (currentenemy.enemytype == typeifenemies.fan) {
			respawn = 0;
		}
		nextwavetime = Time.time+currentenemy.delaytime;
		respawn = 0;
		addedenemy = 0;

	}

	IEnumerator Example() {
		yield return new WaitForSeconds(2.5f);
	}

	IEnumerator deedelay() {
		yield return new WaitForSeconds(1.2f);
	}
}
