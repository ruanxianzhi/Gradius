using UnityEngine;
using System.Collections;
using System;

public  enum typeifenemies{fan,garun,sine,dee,ducker,topducker,bottomducker,jumper,hatcher,vocano,boss,background};
[Serializable]
public class WaveofEnemy {

	// Use this for initialization
	public float delaytime; // the location of the wave
	// alternatively you could do time if the waves are based on time instead of distance
	public GameObject enemyPrefab;
	public GameObject enemyPrefab2;
	public GameObject enemyPrefab3;
	public GameObject enemyPrefab4;
	public int count;
	public typeifenemies enemytype;
	public int index;

	public WaveofEnemy (float t, GameObject Enemy,GameObject enemyPrefab2,GameObject enemyPrefab3,GameObject enemyPrefab4, int EnemyCount, int index=0)
	{

		this.enemyPrefab = Enemy;
		this.enemyPrefab2 = enemyPrefab2;
		this.enemyPrefab3 = enemyPrefab3;
		this.enemyPrefab4 = enemyPrefab4;
		this.delaytime = t;
		this.count = EnemyCount;
		this.index = index;

	}
}
