using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HatcherE : MonoBehaviour {
	public GameObject Babies;
	Rigidbody enemyBod;
	Text score;
	public int points = 300;
	public float spawnTime=0.5f;
	public int health;
	public static int counter;
	float respawn;
	// Use this for initialization
	void Start () {
		enemyBod = this.GetComponent<Rigidbody> ();
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		score = GameObject.Find ("Score").GetComponent<Text>();
		respawn = 0f;
		counter = 0;
		health = 20;
	}
	
	// Update is called once per frame
	void Update () {
		enemyBod.velocity = new Vector3 (-BackgroundScroll.speed, 0f, 0f);
		if (health <= 5)
			this.GetComponent<Renderer>().sharedMaterial.color = Color.red;
		if (this.gameObject.transform.position.x - this.gameObject.transform.lossyScale.x / 2 <= -PlayerController.camW / 2) {
			this.GetComponent<Renderer>().sharedMaterial.color = Color.white;
			Destroy(this.gameObject);
			return;
		}
		respawn -= Time.deltaTime;
		if (respawn > 0)
			return;
		respawn = 0.3f;
		Instantiate (Babies, this.gameObject.transform.position, this.gameObject.transform.rotation);
	}

	public void Scored(){
		score.text = (int.Parse (score.text) + points).ToString();
		/*if (Random.value < dropChance) {
			GameObject power = Instantiate(PowerUp) as GameObject;
			power.GetComponent<Rigidbody>().transform.position = this.gameObject.transform.position;
		}
		*/
		if (transform.position.x < -10) {
			this.GetComponent<Renderer>().sharedMaterial.color = Color.white;
			Destroy(this.gameObject);
		}
	}
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "PlayerWeapon")
			health--;
		if (health > 0)
			return;
		if (coll.gameObject.tag == "Player") {
			if (Babies.gameObject.transform.position == this.gameObject.transform.position){
				Destroy(Babies);
			}
			this.GetComponent<Renderer>().sharedMaterial.color = Color.white;
			Destroy(this.gameObject);
		}
		else if (coll.gameObject.tag == "PlayerWeapon") {

			if (Babies!=null){
				
				if (Babies.gameObject.transform.position == this.gameObject.transform.position){
					
					Destroy(Babies);
				}
			}
			Scored();
			this.GetComponent<Renderer>().sharedMaterial.color = Color.white;
			//Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
	void OnDestroy(){
		
	}
}
