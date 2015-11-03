using UnityEngine;
using System.Collections;

public class FireBullets : MonoBehaviour {
	public GameObject	bulletPrefab;
	public float		bulletSpeed = 20f;
	private Transform	bulletAnchor;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("BulletAnchor");
		bulletAnchor = go.transform;
		InvokeRepeating("Fire", 0.1f, 0.1f);
	}

	void Fire () {
		if (!CameraMover.S.running) return;

		GameObject go = Instantiate(bulletPrefab) as GameObject;
		go.transform.parent = bulletAnchor;
		go.transform.position = transform.position;
		Quaternion rand = Quaternion.Euler(randInVariance(10), randInVariance(10), 0);
		go.transform.rotation = transform.rotation * rand;
		go.rigidbody.velocity = go.transform.forward * bulletSpeed;
	}

	float randInVariance(float variance) {
		float f = Random.value;
		f *= variance * 2;
		f -= variance;
		return f;
	}
}
