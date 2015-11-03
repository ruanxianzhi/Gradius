using UnityEngine;
using System.Collections;

public class ShieldBehaviour : MonoBehaviour {
	public int count = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = GameObject.Find ("Player").gameObject.transform.position;
		pos.x += 1f;
		this.transform.position = pos;
	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.tag == "Basic" || coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "EnemyWeapon") {
			count -= 1;
			this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.lossyScale.x-0.15f+count*0.015f, this.gameObject.transform.lossyScale.y-0.15f+count*0.015f, 0.01f);
		}
		if (count == 0) {
			Destroy(this.gameObject);
			GameObject.Find ("Player").GetComponent<PlayerController>().shieldEnable = false;
		}
	}
}
