using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class spawn_level2 : MonoBehaviour {
	public List <WaveofEnemy> WaveInfo; 
	public static int index_enemy;
	public WaveofEnemy currentenemy;
	float nextwavetime;
	public Vector3 bottomPos;
	float camH, camW;

	// Use this for initialization
	void Start () {
		index_enemy = 0;
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camH = cam.orthographicSize * 2f;
		camW = camH * cam.aspect;
		currentenemy = WaveInfo[index_enemy];
		bottomPos.x= camW/2 +1f;
		bottomPos.y = -camH / 2 + 0.2f;
		bottomPos.z = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < nextwavetime) {
			return;
		}
		if (index_enemy >= WaveInfo.Count) 
			return;
		switch (currentenemy.enemytype) {
			case typeifenemies.cave:
			Instantiate (currentenemy.enemyPrefab, bottomPos, currentenemy.enemyPrefab.gameObject.transform.rotation);
			break;
		default:
				break;
		}
		index_enemy++;
	}
}
