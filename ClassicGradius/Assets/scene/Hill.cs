using UnityEngine;
using System.Collections;

public class Hill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = new Vector3[] {new Vector3(-1, -1, 0), new Vector3(1, -1, 0), new Vector3(0, 0.8f, 0)};
		mesh.uv = new Vector2[] {new Vector2(1, 1), new Vector2(0, 0), new Vector2(0, 1)};
		mesh.triangles = new int[] {0, 1, 2};
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody> ().velocity = new Vector3 (-BackgroundScroll.speed, 0, 0);
	}
}
