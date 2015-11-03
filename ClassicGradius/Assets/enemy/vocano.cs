using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class vocano : MonoBehaviour {
	public GameObject vocanoshots;
	public GameObject vocanoshots2;
	Rigidbody enemyBod;
	Text score;
	public int points = 300;
	float camH,camW;
	public float spawnTime=0.2f;
	float respawn;
	float totaltime;
	static bool movestate;
	bool eject;
	bool side;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = new Vector3[] {new Vector3(-1, -1, 0), new Vector3(1, -1, 0), new Vector3(0, 0.8f, 0)};
		mesh.uv = new Vector2[] {new Vector2(1, 1), new Vector2(0, 0), new Vector2(0, 1)};
		mesh.triangles = new int[] {2, 1, 0};

		totaltime = 15f;
		enemyBod = this.GetComponent<Rigidbody> ();
		score = GameObject.Find ("Score").GetComponent<Text>();
		camH = PlayerController.camH;
		camW = PlayerController.camW;
		respawn = 0.1f;
		movestate = true;
		eject = false;
		side = false;
		//false->left
		//true->right
	}
	
	// Update is called once per frame
	void Update () {
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		if (enemyBod.transform.position.x - enemyBod.transform.lossyScale.x / 2 <= -camW / 2) {
			Destroy(this.gameObject);
			return;
		}
		if (movestate && enemyBod.transform.position.x < -camW / 6) {
			BackgroundScroll.speed = 0;
			eject = true;
			movestate = false;
		}

		if (totaltime < 0) {
			eject = false;
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			BackgroundScroll.speed = 2f;
		}
		if (eject) {
			totaltime -= Time.deltaTime;
			this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = 0.1f;
			GameObject obj =Instantiate (vocanoshots) as GameObject;
			GameObject obj2 =Instantiate (vocanoshots2) as GameObject;
			Vector3 pos = this.gameObject.transform.position;
			pos.z = 0f;
			Vector3 pos2 = pos;
			pos2.x += 8f;
			obj.transform.position =  pos;
			obj2.transform.position =  pos2;
			float xspeed = Random.value* 16f;
			float yspeed = Random.value* 25f;
			while (xspeed < 3f){
				xspeed+=1f;
			}
			if (!side){
				xspeed*=-1;
			}
			obj.GetComponent<Rigidbody>().velocity = new Vector3(xspeed,yspeed,0);
			obj2.GetComponent<Rigidbody>().velocity = new Vector3(xspeed,yspeed,0);
			side = !side;

		}


	}
}
