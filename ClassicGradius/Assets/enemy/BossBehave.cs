using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossBehave : MonoBehaviour {
	public GameObject BWeapon1;
	public GameObject BWeapon2;
	public GameObject BWeapon3;
	public GameObject BWeapon4;

	Rigidbody enemyBod;
	public int points = 400;
	bool moveforward = true;
	Text score;
	public bool first = true;
	public bool dirup = true;
	public int healthofboss = 30;
	public float bound ;
	float camH;
	public float spawnTime = 2f;
	public float respawn ;
	//public float shotdelay;
	//public int shotcount;
	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		camH = PlayerController.camH;
		bound = camH / 2 ;
		respawn = 0;
		//shotcount = 0;
		//shotdelay = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		/*if (shotcount > 0) {
			shotdelay-=Time.deltaTime;
			if (shotdelay > 0){
				return;
			}
			shotdelay = 0.1f;
			spawnshotting();
			shotcount++;
			if (shotcount >=4){
				shotcount = 0;
			}
		}*/
		if (moveforward) {
			if (enemyBod.gameObject.transform.position.x < 3f){
				moveforward = false;
				BackgroundScroll.speed = 0;
			}
		}
		else {
			if (dirup)
				transform.Translate (Vector3.up * 1.8f * Time.deltaTime);
			else
				transform.Translate (-Vector3.up * 1.8f * Time.deltaTime);
			if (first) {
				if (transform.position.y >= bound-1.2f ) {
					dirup = false;
				}
				if (transform.position.y <= -bound +1.2f) {
					dirup = true;	
					first = false;
				}
			}
			if (transform.position.y >= bound - 2.4f) {
				dirup = false;	
			}
			if (transform.position.y <= -bound +2.4f) {
				dirup = true;
				first = false;
			}
			respawn -= Time.deltaTime;
			if (respawn > 0)
				return;
			respawn = spawnTime;
			spawnshotting();
			//shotcount ++;
	

		}

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
			Application.LoadLevel("StartScene");
			StartController.health = 3;
		}
		else if (coll.gameObject.tag == "PlayerWeapon") {
			healthofboss--;
			if (healthofboss<=0){
				Scored();
				//Destroy(coll.gameObject);
				Destroy(this.gameObject);
				Application.LoadLevel("StartScene");
				StartController.health = 3;
			}
		}
	}
	void OnDestroy(){
		
	}

	void spawnshotting(){
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
		Instantiate (BWeapon1, this.gameObject.transform.position + new Vector3 (0f, 0.5f, 0f), this.gameObject.transform.rotation);
		Instantiate (BWeapon2, this.gameObject.transform.position + new Vector3 (1.5f, 1.5f, 0f), this.gameObject.transform.rotation);
		Instantiate (BWeapon3, this.gameObject.transform.position - new Vector3 (0f, 0.5f, 0f), this.gameObject.transform.rotation);
		Instantiate (BWeapon4, this.gameObject.transform.position - new Vector3 (-1.5f, 1.5f, 0f), this.gameObject.transform.rotation);

	}
	IEnumerator Example() {
		yield return new WaitForSeconds(0.1f);
	}
}

